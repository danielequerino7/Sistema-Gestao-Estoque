using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly LigaDbContext _context;

    public MovimentacaoRepository(LigaDbContext context)
    {
        _context = context;
    }

    public void AdicionarMovimentacao(MovimentacaoEstoque mov)
    {
        _context.Movimentacoes.Add(mov);
        _context.SaveChanges();
    }
}