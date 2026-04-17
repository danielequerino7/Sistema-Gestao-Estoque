using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Application.Services;
using Domain.Interfaces;
using Persistence.Context;
using Persistence.Repositories;
using SimpleInjector.Lifestyles;
using SimpleInjector;
using WebApplication.App_Start;
using SimpleInjector.Integration.WebApi;
using AutoMapper;
using Application.Mappings;


namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SwaggerConfig.Register();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ConfigureSimpleInjector();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void ConfigureSimpleInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<LigaDbContext>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IEstoqueRepository, EstoqueRepository>(Lifestyle.Scoped);
           // container.Register<IMovimentacaoRepository, MovimentacaoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProdutoProfile>();               
            });
            container.RegisterInstance(config);
            container.Register<IMapper>(() =>
                config.CreateMapper(container.GetInstance), Lifestyle.Scoped);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
