using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface ILoginService
    {
        UsuarioDTO Autenticar(LoginDTO dto);
    }
}
