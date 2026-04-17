using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Application.Services.Interfaces;

namespace Application.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IMapper _mapper;

        public MovimentacaoService(
            IProdutoRepository produtoRepository,
            IEstoqueRepository estoqueRepository,
            IMovimentacaoRepository movimentacaoRepository,
            IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _estoqueRepository = estoqueRepository;
            _movimentacaoRepository = movimentacaoRepository;
            _mapper = mapper;
        }

        public MovimentacaoDTO Movimentar(MovimentacaoDTO dto)
        {
            if (dto == null)
                throw new Exception("Dados inválidos");

            if (dto.Quantidade <= 0)
                throw new Exception("Quantidade deve ser maior que zero");

            var produto = _produtoRepository.ObterPorId(dto.ProdutoId);
            if (produto == null)
                throw new Exception("Produto não encontrado");

            switch (dto.Tipo)
            {
                case "ENTRADA":
                    Entrada(dto);
                    break;

                case "CONSUMO":
                    Consumo(dto);
                    break;

                case "TRANSFERENCIA":
                    Transferencia(dto);
                    break;

                default:
                    throw new Exception("Tipo inválido");
            }

            var movimentacao = new MovimentacaoEstoque
            {
                Tipo = dto.Tipo,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade,
                UsuarioId = dto.UsuarioId,
                SetorOrigemId = dto.SetorOrigemId,
                SetorDestinoId = dto.SetorDestinoId,
                DataMovimentacao = DateTime.Now
            };

            _movimentacaoRepository.Adicionar(movimentacao);

            return dto;
        }


        private void Entrada(MovimentacaoDTO dto)
        {
            if (dto.SetorDestinoId == null)
                throw new Exception("Setor destino obrigatório");

            var estoque = _estoqueRepository.Obter(dto.ProdutoId, dto.SetorDestinoId.Value);

            if (estoque == null)
            {
                estoque = new Estoque
                {
                    ProdutoId = dto.ProdutoId,
                    SetorId = dto.SetorDestinoId.Value,
                    Quantidade = dto.Quantidade
                };

                _estoqueRepository.Adicionar(estoque);
            }
            else
            {
                estoque.Quantidade += dto.Quantidade;
                _estoqueRepository.Atualizar(estoque);
            }
        }

        private void Consumo(MovimentacaoDTO dto)
        {
            if (dto.SetorOrigemId == null)
                throw new Exception("Setor origem obrigatório");

            var estoque = _estoqueRepository.Obter(dto.ProdutoId, dto.SetorOrigemId.Value);

            if (estoque == null || estoque.Quantidade < dto.Quantidade)
                throw new Exception("Estoque insuficiente para consumo");

            estoque.Quantidade -= dto.Quantidade;

            _estoqueRepository.Atualizar(estoque);
        }

        private void Transferencia(MovimentacaoDTO dto)
        {
            if (dto.SetorOrigemId == null || dto.SetorDestinoId == null)
                throw new Exception("Origem e destino obrigatórios");

            if (dto.SetorOrigemId == dto.SetorDestinoId)
                throw new Exception("Setores não podem ser iguais");

            var estoqueOrigem = _estoqueRepository.Obter(dto.ProdutoId, dto.SetorOrigemId.Value);

            if (estoqueOrigem == null || estoqueOrigem.Quantidade < dto.Quantidade)
                throw new Exception("Estoque insuficiente no setor de origem para transferência");

            estoqueOrigem.Quantidade -= dto.Quantidade;
            _estoqueRepository.Atualizar(estoqueOrigem);

            var estoqueDestino = _estoqueRepository.Obter(dto.ProdutoId, dto.SetorDestinoId.Value);

            if (estoqueDestino == null)
            {
                estoqueDestino = new Estoque
                {
                    ProdutoId = dto.ProdutoId,
                    SetorId = dto.SetorDestinoId.Value,
                    Quantidade = dto.Quantidade
                };

                _estoqueRepository.Adicionar(estoqueDestino);
            }
            else
            {
                estoqueDestino.Quantidade += dto.Quantidade;
                _estoqueRepository.Atualizar(estoqueDestino);
            }
        }      
    }
}