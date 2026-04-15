using System.Data.Entity;
using Domain.Entities;

namespace Persistence.Context
{
    public class LigaDbContext : DbContext
    {
        public LigaDbContext() : base("name=LigaDbConnection")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<MovimentacaoEstoque> Movimentacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estoque>()
                .HasKey(e => new { e.ProdutoId, e.SetorId });

            modelBuilder.Entity<Estoque>()
                .HasRequired(e => e.Produto)
                .WithMany(p => p.Estoques)
                .HasForeignKey(e => e.ProdutoId);

            modelBuilder.Entity<Estoque>()
                .HasRequired(e => e.Setor)
                .WithMany(s => s.Estoques)
                .HasForeignKey(e => e.SetorId);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasRequired(m => m.Produto)
                .WithMany()
                .HasForeignKey(m => m.ProdutoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasRequired(m => m.Usuario)
                .WithMany(u => u.Movimentacoes)
                .HasForeignKey(m => m.UsuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOptional(m => m.SetorOrigem)
                .WithMany()
                .HasForeignKey(m => m.SetorOrigemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOptional(m => m.SetorDestino)
                .WithMany()
                .HasForeignKey(m => m.SetorDestinoId)
                .WillCascadeOnDelete(false);
        }
    }

}
