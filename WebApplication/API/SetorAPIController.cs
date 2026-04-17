using System.Web.Http;
using Application.Services.Interfaces;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/setores")]
    public class SetorAPIController : ApiController
    {

        private readonly ISetorService _service;

        public SetorAPIController(ISetorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Listar()
        {
            var produtos = _service.Listar();
            return Ok(produtos);
        }
    }
}