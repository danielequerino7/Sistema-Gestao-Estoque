using System;
using Application.DTOs;
using Domain.Interfaces;
using System.Web.Http;

namespace WebApplication.API
{
    [RoutePrefix("api/produtos")]
    public class ProdutoAPIController : ApiController
    {
        private readonly IProdutoService _service;

        public ProdutoAPIController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Criar([FromBody] ProdutoDTO dto)
        {
            var produto = _service.Criar(dto);
            return Ok(produto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult ObterPorId(int id)
        {
            var produto = _service.ObterPorId(id);
            if (produto == null)
                return NotFound();
            return Ok(produto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Atualizar(int id, [FromBody] ProdutoDTO dto)
        {
            if (dto == null)
                return BadRequest("Dados inválidos");

            _service.Atualizar(id, dto);

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Listar()
        {
            var produtos = _service.Listar();
            return Ok(produtos);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Excluir(int id)
        {
            try
            {
                _service.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}