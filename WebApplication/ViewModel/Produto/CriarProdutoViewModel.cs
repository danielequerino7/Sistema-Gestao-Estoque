using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication.ViewModel
{
    
    public class CriarProdutoViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int SetorId { get; set; }
        public List<SelectListItem> Setores { get; set; }
    }
}