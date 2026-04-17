using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Produto Adicionar(Produto produto);
        Produto ObterPorId(int id);
        Produto Atualizar(Produto produto);
        IEnumerable<Produto> Listar();
        void Remover(Produto produto);
        bool ExisteSku(string sku);
    }
}
