using System.Collections.Generic;
using Application.DTOs;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoService
    {
        ProdutoDTO Criar(ProdutoDTO produtoDTO);
        ProdutoDTO Atualizar(int id, ProdutoDTO produtoDTO);
        ProdutoDTO ObterPorId(int id);
        void Excluir(int id);
        IEnumerable<ProdutoDTO> Listar();
    }
}
