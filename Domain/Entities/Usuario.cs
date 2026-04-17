using System.Collections.Generic;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string SenhaHash { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<MovimentacaoEstoque> Movimentacoes { get; set; }
    }
}
