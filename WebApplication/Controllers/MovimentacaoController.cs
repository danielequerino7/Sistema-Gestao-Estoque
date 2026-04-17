using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Application.DTOs;
using System.Collections.Generic;
using WebApplication.Controllers;
using Domain.Entities;
using WebApplication.ViewModel;

public class MovimentacaoController : BaseController
{
    private MovimentacaoViewModel CarregarCombos()
    {
        var vm = new MovimentacaoViewModel();

        var apiUrl = GetBaseUrl() + "produtos";

        var produtosResponse = HttpClient.GetAsync(apiUrl).Result;
        if (produtosResponse.IsSuccessStatusCode)
        {
            var json = produtosResponse.Content.ReadAsStringAsync().Result;
            var produtos = JsonConvert.DeserializeObject<List<ProdutoDTO>>(json);

            vm.Produtos = produtos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nome
            }).ToList();
        }

        var setoresResponse = HttpClient.GetAsync(GetBaseUrl() + "setores").Result;
        if (setoresResponse.IsSuccessStatusCode)
        {
            var json = setoresResponse.Content.ReadAsStringAsync().Result;
            var setores = JsonConvert.DeserializeObject<List<Setor>>(json);

            vm.Setores = setores.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Nome
            }).ToList();
        }

        return vm;
    }

    public ActionResult Entrada()
    {
        return View(CarregarCombos());
    }

    [HttpPost]
    public ActionResult Entrada(MovimentacaoViewModel vm)
    {
        try
        {
            var usuario = Session["Usuario"] as UsuarioDTO;

            var dto = new MovimentacaoDTO
            {
                Tipo = "ENTRADA",
                ProdutoId = vm.ProdutoId,
                Quantidade = vm.Quantidade,
                SetorDestinoId = vm.SetorDestinoId,
                UsuarioId = usuario.Id
            };

            var apiUrl = GetBaseUrl() + "movimentacoes";

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = HttpClient.PostAsync(apiUrl, content).Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Entrada");

            ErroApi(response);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var combos = CarregarCombos();
        vm.Produtos = combos.Produtos;
        vm.Setores = combos.Setores;

        return View(vm);
    }

    public ActionResult Consumo()
    {
        return View(CarregarCombos());
    }

    [HttpPost]
    public ActionResult Consumo(MovimentacaoViewModel vm)
    {
        try
        {
            var dto = new MovimentacaoDTO
            {
                Tipo = "CONSUMO",
                ProdutoId = vm.ProdutoId,
                Quantidade = vm.Quantidade,
                SetorOrigemId = vm.SetorOrigemId,
                UsuarioId = Convert.ToInt32(Session["Id"])
            };

            var apiUrl = GetBaseUrl() + "movimentacoes";

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = HttpClient.PostAsync(apiUrl, content).Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Consumo");

            ErroApi(response);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var combos = CarregarCombos();
        vm.Produtos = combos.Produtos;
        vm.Setores = combos.Setores;

        return View(vm);
    }

    public ActionResult Transferencia()
    {
        return View(CarregarCombos());
    }

    [HttpPost]
    public ActionResult Transferencia(MovimentacaoViewModel vm)
    {
        try
        {
            var usuario = Session["Usuario"] as UsuarioDTO;

            if (vm.SetorOrigemId == vm.SetorDestinoId)
            {
                ModelState.AddModelError("", "Origem e destino não podem ser iguais");

                var combosErro = CarregarCombos();
                vm.Produtos = combosErro.Produtos;
                vm.Setores = combosErro.Setores;

                return View(vm);
            }

            var dto = new MovimentacaoDTO
            {
                Tipo = "TRANSFERENCIA",
                ProdutoId = vm.ProdutoId,
                Quantidade = vm.Quantidade,
                SetorOrigemId = vm.SetorOrigemId,
                SetorDestinoId = vm.SetorDestinoId,
                UsuarioId = usuario.Id
            };

            var apiUrl = GetBaseUrl() + "movimentacoes";

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = HttpClient.PostAsync(apiUrl, content).Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Transferencia");

            ErroApi(response);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        var combos = CarregarCombos();
        vm.Produtos = combos.Produtos;
        vm.Setores = combos.Setores;

        return View(vm);
    }
}