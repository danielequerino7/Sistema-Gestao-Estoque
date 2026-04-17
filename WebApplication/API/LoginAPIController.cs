using System.Web.Http;
using Application.DTOs;
using Application.Services.Interfaces;


namespace WebApplication.API
{
    [RoutePrefix("api/login")]
    public class LoginAPIController : ApiController
    {
        private readonly ILoginService _service;

        public LoginAPIController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Login(LoginDTO dto)
        {
            var usuario = _service.Autenticar(dto);

            if (usuario == null)
                return Unauthorized();

            return Ok(usuario);
        }
    }
}