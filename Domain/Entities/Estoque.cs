namespace Domain.Entities
{
    public class Estoque
    {
        public int ProdutoId { get; set; }
        public int SetorId { get; set; }
        public int Quantidade { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
