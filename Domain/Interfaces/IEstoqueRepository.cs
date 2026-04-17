using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEstoqueRepository
    {
        void Adicionar(Estoque estoque);
        Estoque ObterPorProdutoId(int id);
        Estoque Obter(int produtoId, int setorId);
        void Atualizar(Estoque estoque);
        IEnumerable<Estoque> Listar();
    }
}
