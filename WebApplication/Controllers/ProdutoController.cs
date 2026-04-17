using System.Net.Http;
using System;
using System.Web.Mvc;
using Application.DTOs;
using WebApplication.ViewModel;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using Domain.Entities;

namespace WebApplication.Controllers
{
    public class ProdutoController : BaseController
    {
        [HttpPost]
        public ActionResult Criar(CriarProdutoViewModel vm)
        {
            try
            {
                var dto = new ProdutoDTO
                {
                    Nome = vm.Nome,
                    Descricao = vm.Descricao,
                    Preco = vm.Preco,
                    QuantidadeEmEstoque = vm.QuantidadeEmEstoque,
                    SetorId = vm.SetorId
                };

                var apiUrl = GetBaseUrl() + "produtos";

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = HttpClient.PostAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Criar");
                }

                ErroApi(response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar produto: " + ex.Message);
            }

            vm.Setores = CarregarSetores();
            return View(vm);
        }

        public ActionResult Criar()
        {
            var vm = new CriarProdutoViewModel();

            try
            {
                vm.Setores = CarregarSetores();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao carregar dados: " + ex.Message);
                vm.Setores = new List<SelectListItem>();
            }

            return View(vm);
        }

        public ActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            IEnumerable<ProdutoDTO> produtos = new List<ProdutoDTO>();

            try
            {
                var apiUrl = GetBaseUrl() + "produtos";
                var response = HttpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<IEnumerable<ProdutoDTO>>(json);
                }
                else
                {
                    ErroApi(response);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao listar produtos: " + ex.Message);
            }

            var totalItens = produtos.Count();
            var totalPaginas = (int)Math.Ceiling((double)totalItens / pageSize);

            var produtosPaginados = produtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new PaginacaoProdutoViewModel
            {
                Produtos = produtosPaginados.Select(p => new ExibirProdutoViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Sku = p.Sku,
                    Preco = p.Preco,
                    QuantidadeEmEstoque = p.QuantidadeEmEstoque
                }).ToList(),
                PaginaAtual = page,
                TotalPaginas = totalPaginas
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Editar(EditarProdutoViewModel vm)
        {
            try
            {
                var dto = new ProdutoDTO
                {
                    Nome = vm.Nome,
                    Descricao = vm.Descricao,
                    Preco = vm.Preco,
                    QuantidadeEmEstoque = vm.QuantidadeEmEstoque
                };

                var apiUrl = GetBaseUrl() + "produtos/" + vm.Id;

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = HttpClient.PutAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ErroApi(response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao editar produto: " + ex.Message);
            }

            return View(vm);
        }

        public ActionResult Editar(int id)
        {
            ProdutoDTO produto = null;

            try
            {
                var apiUrl = GetBaseUrl() + "produtos/" + id;
                var response = HttpClient.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoDTO>(json);
                }
                else
                {
                    ErroApi(response);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao buscar produto: " + ex.Message);
            }

            if (produto == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new EditarProdutoViewModel
            {
                Id = produto.Id,
                Sku = produto.Sku,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                var apiUrl = GetBaseUrl() + "produtos/" + id;
                var response = HttpClient.DeleteAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ErroApi(response);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao excluir produto: " + ex.Message);
            }

            return RedirectToAction("Index");
        }

        private List<SelectListItem> CarregarSetores()
        {
            try
            {
                var apiUrl = GetBaseUrl() + "setores";
                var response = HttpClient.GetAsync(apiUrl).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new List<SelectListItem>();
                }

                var json = response.Content.ReadAsStringAsync().Result;
                var setores = JsonConvert.DeserializeObject<List<Setor>>(json);

                return setores.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }).ToList();
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }
    }
}