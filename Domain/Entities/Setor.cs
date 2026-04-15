using System.Collections.Generic;

namespace Domain.Entities
{
    public class Setor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Estoque> Estoques { get; set; }
    }
}
