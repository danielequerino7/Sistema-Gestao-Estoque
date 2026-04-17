using System.Collections.Generic;

namespace WebApplication.ViewModel
{
    public class PaginacaoProdutoViewModel
    {
        public List<ExibirProdutoViewModel> Produtos { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
    }
}