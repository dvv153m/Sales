using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Infrastructure.Services;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();            

            builder.Services.AddScoped<IHttpProxy, HttpProxy>();
            builder.Services.AddScoped<IProductClient, ProductClient>(x=>
            {
                WebUIOptions appConf = builder.Configuration.GetSection(WebUIOptions.SectionName).Get<WebUIOptions>();
                return new ProductClient(x.GetRequiredService<IHttpProxy>(), appConf.ProductApiUrl);
            });
            
        }
    }
}
