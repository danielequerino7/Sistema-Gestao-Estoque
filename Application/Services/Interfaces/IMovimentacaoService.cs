using System.Collections.Generic;
using Application.DTOs;

namespace Application.Services.Interfaces
{
    public interface IMovimentacaoService
    {
        MovimentacaoDTO Movimentar(MovimentacaoDTO dto);
    }
}
