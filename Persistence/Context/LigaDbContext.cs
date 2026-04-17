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

            modelBuilder.Entity<Usuario>()
               .Property(e => e.SenhaHash)
               .HasColumnName("senha_hash");

            modelBuilder.Entity<Estoque>()
                .HasKey(e => new { e.ProdutoId, e.SetorId });

            modelBuilder.Entity<Estoque>()
                .Property(e => e.ProdutoId)
                .HasColumnName("produto_id");

            modelBuilder.Entity<Estoque>()
                .Property(e => e.SetorId)
                .HasColumnName("setor_id");

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

            modelBuilder.Entity<MovimentacaoEstoque>()
               .Property(e => e.ProdutoId)
               .HasColumnName("produto_id");

            modelBuilder.Entity<MovimentacaoEstoque>()
               .Property(e => e.UsuarioId)
               .HasColumnName("usuario_id");

            modelBuilder.Entity<MovimentacaoEstoque>()
               .Property(e => e.SetorOrigemId)
               .HasColumnName("setor_origem_id");

            modelBuilder.Entity<MovimentacaoEstoque>()
               .Property(e => e.SetorDestinoId)
               .HasColumnName("setor_destino_id");

            modelBuilder.Entity<MovimentacaoEstoque>()
               .Property(e => e.DataMovimentacao)
               .HasColumnName("data_movimentacao");


            modelBuilder.Entity<Setor>().ToTable("Setores");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Estoque>().ToTable("Estoque");
            modelBuilder.Entity<MovimentacaoEstoque>().ToTable("MovimentacaoEstoque");
        }
    }

}
