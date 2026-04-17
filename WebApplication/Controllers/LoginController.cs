using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Application.DTOs;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string cpf, string senha)
        {
            var cpfSemMascara = Regex.Replace(cpf, @"\D", "");

            try
            {
                var dto = new
                {
                    Cpf = cpfSemMascara,
                    SenhaHash = senha
                };

                var apiUrl = GetBaseUrl() + "login";
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = HttpClient.PostAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(jsonResult);

                    Session["Usuario"] = usuario;
                    Session["Nome"] = usuario.Nome;
                    Session["CPF"] = usuario.CPF;

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "CPF ou senha inválidos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro: " + ex.Message);
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}