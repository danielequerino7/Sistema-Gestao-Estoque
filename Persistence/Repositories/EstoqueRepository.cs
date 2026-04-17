using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;
using System.Data.Entity;

namespace Persistence.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly LigaDbContext _context;

        public EstoqueRepository(LigaDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Estoque estoque)
        {
            _context.Estoques.Add(estoque);
            _context.SaveChanges();
        }

        public void Atualizar(Estoque estoque)
        {
            _context.SaveChanges();
        }

        public IEnumerable<Estoque> Listar()
        {
            return _context.Estoques.ToList();
        }

        public Estoque ObterPorProdutoId(int id)
        {
            return _context.Estoques
                .FirstOrDefault(p => p.ProdutoId == id);
        }
        public Estoque Obter(int produtoId, int setorId)
        {
            return _context.Estoques
                .Include(e => e.Produto)
                .Include(e => e.Setor)
                .FirstOrDefault(e => e.ProdutoId == produtoId && e.SetorId == setorId);
        }
    }
}
