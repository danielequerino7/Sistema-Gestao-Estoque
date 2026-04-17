namespace WebApplication.ViewModel
{
    public class ExibirProdutoViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
    }
}