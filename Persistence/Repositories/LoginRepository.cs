using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly LigaDbContext _context;

        public LoginRepository(LigaDbContext context)
        {
            _context = context;
        }

        public Usuario BuscarPorCPF(string cpf)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Cpf == cpf);
        }
    }
}
