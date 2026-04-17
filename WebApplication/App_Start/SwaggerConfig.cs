using System.IO;
using System.Web.Http;
using Swashbuckle.Application;

namespace WebApplication.App_Start
{
    class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "API - Sistema de Gestão de Estoque Hospitalar (Liga)")
                     .Description("API para controle de produtos, estoque, movimentações e setores em um ambiente hospitalar.")
                     .Contact(cc => cc
                         .Name("Desenvolvedora Daniele")
                         .Email("danisq77@gmail.com")
                     );
                    c.DescribeAllEnumsAsStrings();
                    var xmlPath = Path.Combine(
                        System.AppDomain.CurrentDomain.BaseDirectory,
                        "bin",
                        "WebApplication.xml"
                    );

                    if (File.Exists(xmlPath))
                        c.IncludeXmlComments(xmlPath);

                    c.GroupActionsBy(api => api.ActionDescriptor.ControllerDescriptor.ControllerName);
                    
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Swagger - Gestão de Estoque Hospitalar");
                    c.DocExpansion(DocExpansion.List);
                    c.DisableValidator();
                });
        }
    }
}