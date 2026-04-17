using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTOs;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
        public class ProdutoService : IProdutoService
        {
            private readonly IProdutoRepository _produtoRepository;
            private readonly IEstoqueRepository _estoqueRepository;
            private readonly IMapper _mapper;
            private static readonly Random _random = new Random();

            public ProdutoService(
                IProdutoRepository produtoRepository,
                IEstoqueRepository estoqueRepository,
                IMapper mapper)
            {
                _produtoRepository = produtoRepository;
                _estoqueRepository = estoqueRepository;
                _mapper = mapper;
            }

        public ProdutoDTO Criar(ProdutoDTO dto)
        {

            if (dto.QuantidadeEmEstoque < 0)
                throw new RegraNegocioException("Quantidade em estoque não pode ser negativa");

            if (dto.Preco < 0)
                throw new RegraNegocioException("Preço não pode ser negativo");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new RegraNegocioException("Nome do produto é obrigatório");

            var sku = GerarSkuUnico();
            dto.Sku = sku;

            var produto = new Produto
            {
                Sku = dto.Sku,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco
            };

            _produtoRepository.Adicionar(produto);

            var estoque = new Estoque
            {
                ProdutoId = produto.Id,
                SetorId = dto.SetorId,
                Quantidade = dto.QuantidadeEmEstoque
            };

            _estoqueRepository.Adicionar(estoque);

            return _mapper.Map<ProdutoDTO>(produto);
        }

        public ProdutoDTO Atualizar(int id, ProdutoDTO dto)
        {

            if (dto.Preco < 0)
                throw new RegraNegocioException("Preço não pode ser negativo");

            if (dto.QuantidadeEmEstoque < 0)
                throw new RegraNegocioException("Quantidade não pode ser negativa");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new RegraNegocioException("Nome é obrigatório");

            var produto = _produtoRepository.ObterPorId(id);

            if (produto == null)
                throw new Exception("Produto não encontrado");


            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.Preco = dto.Preco;

            _produtoRepository.Atualizar(produto);

            var estoque = _estoqueRepository.ObterPorProdutoId(produto.Id);

            if (estoque == null)
                throw new Exception("Estoque não encontrado");

            estoque.Quantidade = dto.QuantidadeEmEstoque;

            _estoqueRepository.Atualizar(estoque);

            return _mapper.Map<ProdutoDTO>(produto);
        }

        public ProdutoDTO ObterPorId(int id)
        {
            var produto = _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public IEnumerable<ProdutoDTO> Listar()
        {
            var produtos = _produtoRepository.Listar();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        }


        public void Excluir(int id)
        {
            var produto = _produtoRepository.ObterPorId(id);

            if (produto == null)
                throw new Exception("Produto não encontrado");

            _produtoRepository.Remover(produto);
        }

        private string GerarSku()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string GerarSkuUnico()
        {
            string sku;
            do
            {
                sku = GerarSku();
            }
            while (_produtoRepository.ExisteSku(sku));
            return sku;
        }
   
    }
}
