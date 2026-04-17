using System.Web.Mvc;
using System.Collections.Generic;

namespace WebApplication.ViewModel
{
    public class MovimentacaoViewModel
    {
        public int ProdutoId { get; set; }
        public int? SetorOrigemId { get; set; }
        public int? SetorDestinoId { get; set; }
        public int Quantidade { get; set; }

        public List<SelectListItem> Produtos { get; set; }
        public List<SelectListItem> Setores { get; set; }
    }
}