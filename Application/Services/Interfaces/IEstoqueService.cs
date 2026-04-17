using System.Collections.Generic;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEstoqueService
    {
        IEnumerable<Estoque> Listar();
    }
}
