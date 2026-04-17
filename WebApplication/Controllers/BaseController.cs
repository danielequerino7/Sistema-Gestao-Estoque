using System.Net.Http;
using System.Web.Mvc;

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
    }
}