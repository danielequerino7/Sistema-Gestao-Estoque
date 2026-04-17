using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoginRepository
    {
        Usuario BuscarPorCPF(string cpf);
    }
}
