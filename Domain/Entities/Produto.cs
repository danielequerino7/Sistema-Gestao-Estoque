using System.Collections.Generic;

namespace Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public virtual ICollection<Estoque> Estoques { get; set; }
    }
}
