namespace Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int SetorId { get; set; }
    }
}
