using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Services;
using Sales.Infrastructure.Services;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            WebUIOptions appConf = builder.Configuration.GetSection(WebUIOptions.SectionName).Get<WebUIOptions>();

            builder.Services.AddHttpClient();            

            builder.Services.AddScoped<IHttpProxy, HttpProxy>();
            
            builder.Services.AddScoped<IProductClient, ProductClient>(x=>
            {                
                return new ProductClient(x.GetRequiredService<IHttpProxy>(), appConf.ProductApiUrl);
            });

            builder.Services.AddScoped<IOrderClient, OrderClient>(x =>
            {
                return new OrderClient(x.GetRequiredService<IHttpProxy>(), appConf.OrderApiUrl);
            });

            builder.Services.AddScoped<IPromocodeClient, PromocodeClient>(x =>
            {                
                return new PromocodeClient(x.GetRequiredService<IHttpProxy>(), appConf.PromocodeApiUrl);
            });
        }
    }
}
