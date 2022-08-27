using Microsoft.Extensions.DependencyInjection;
using Sales.Application.Services;
using Sales.WebUI.Configuration;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPromocodeGenerator>(x =>
            {
                AppConfig appConf = builder.Configuration.GetSection(AppConfig.SectionName).Get<AppConfig>();
                return new PromocodeGenerator(appConf.PromoocodeLenght);
            });
        }
    }
}
