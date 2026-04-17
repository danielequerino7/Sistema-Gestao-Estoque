using System;
using System.Linq;
using System.Web.Http;
using Application.DTOs;
using Application.Services.Interfaces;


namespace WebApplication.API
{
    [RoutePrefix("api/movimentacoes")]
    public class MovimentacaoAPIController : ApiController
    {
        private readonly IMovimentacaoService _service;

        public MovimentacaoAPIController(IMovimentacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Criar([FromBody] MovimentacaoDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Dados inválidos");

                if (dto.Quantidade <= 0)
                    return BadRequest("Quantidade deve ser maior que zero");

                if (string.IsNullOrEmpty(dto.Tipo))
                    return BadRequest("Tipo da movimentação é obrigatório");

                var tiposValidos = new[] { "ENTRADA", "CONSUMO", "TRANSFERENCIA" };

                if (!tiposValidos.Contains(dto.Tipo))
                    return BadRequest("Tipo de movimentação inválido");

                // Regras básicas por tipo
                if (dto.Tipo == "ENTRADA" && dto.SetorDestinoId == null)
                    return BadRequest("Setor destino é obrigatório para entrada");

                if (dto.Tipo == "CONSUMO" && dto.SetorOrigemId == null)
                    return BadRequest("Setor origem é obrigatório para consumo");

                if (dto.Tipo == "TRANSFERENCIA")
                {
                    if (dto.SetorOrigemId == null || dto.SetorDestinoId == null)
                        return BadRequest("Origem e destino são obrigatórios");

                    if (dto.SetorOrigemId == dto.SetorDestinoId)
                        return BadRequest("Origem e destino não podem ser iguais");
                }

                var result = _service.Movimentar(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }       
    }
}