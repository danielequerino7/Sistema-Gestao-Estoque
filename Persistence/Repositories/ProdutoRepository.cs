using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly LigaDbContext _context;

        public ProdutoRepository(LigaDbContext context)
        {
            _context = context;
        }

        public Produto Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public Produto ObterPorId(int id)
        {
            return _context.Produtos
                .Include("Estoques")
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Produto> Listar()
        {
            return _context.Produtos
                .Include("Estoques")
                .ToList();
        }

        public Produto Atualizar(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return produto;
        }

        public void Remover(Produto produto)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }

        public bool ExisteSku(string sku)
        {
            return _context.Produtos.Any(p => p.Sku == sku);
        }
    }
}

