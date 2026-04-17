using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovimentacaoRepository
    {
        void AdicionarMovimentacao(MovimentacaoEstoque mov);
    }
}
