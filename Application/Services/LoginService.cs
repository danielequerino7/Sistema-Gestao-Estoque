using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        private readonly IMapper _mapper;

        public LoginService(ILoginRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UsuarioDTO Autenticar(LoginDTO dto)
        {
            var usuario = _repository.BuscarPorCPF(dto.CPF);

            if (usuario == null)
            {
                return null;
            }

            if (usuario.SenhaHash != dto.SenhaHash)
                return null;

            return _mapper.Map<UsuarioDTO>(usuario);

        }
    }
}
