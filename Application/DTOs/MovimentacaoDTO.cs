using System;

namespace Application.DTOs
{
    public class MovimentacaoDTO
    {

        public string Tipo { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public int UsuarioId { get; set; }
        public int? SetorOrigemId { get; set; }
        public int? SetorDestinoId { get; set; }
        public DateTime DataMovimentacao { get; set; }

        /* public string Tipo { get; set; }
         public int Quantidade { get; set; }
         public int ProdutoId { get; set; }
         public int UsuarioId { get; set; }
         public int? SetorOrigemId { get; set; }
         public int? SetorDestinoId { get; set; }
         public DateTime DataMovimentacao { get; set; }
         public virtual Produto Produto { get; set; }
         public virtual Usuario Usuario { get; set; }
         public virtual Setor SetorOrigem { get; set; }
         public virtual Setor SetorDestino { get; set; }*/


    }
}
