using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication.Exceptions;

namespace WebApplication.Controllers
{
    public abstract class BaseController : Controller
    {
        protected static readonly HttpClient HttpClient = new HttpClient();

        protected string GetBaseUrl()
        {
            return string.Format("{0}://{1}{2}api/",
                Request.Url.Scheme, Request.Url.Authority, Url.Content("~/"));
        }

        protected void ErroApi(HttpResponseMessage response)
        {
            try
            {
                var erroJson = response.Content.ReadAsStringAsync().Result;

                var erro = JsonConvert.DeserializeObject<ExceptionResponse>(erroJson);

                if (erro != null && !string.IsNullOrWhiteSpace(erro.ExceptionMessage))
                {
                    ModelState.AddModelError("", erro.ExceptionMessage);
                    return;
                }

                ModelState.AddModelError("", "Erro ao processar a requisição.");
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao processar a requisição.");
            }
        }
    }
}