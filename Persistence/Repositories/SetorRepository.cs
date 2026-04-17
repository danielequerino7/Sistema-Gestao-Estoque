using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class SetorRepository : ISetorRepository
    {
        private readonly LigaDbContext _context;

        public SetorRepository(LigaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Setor> Listar()
        {
            return _context.Setores.ToList();
        }
    }
}
