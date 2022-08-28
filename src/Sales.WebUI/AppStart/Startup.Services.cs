using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Services;
using Sales.Infrastructure.Services;


namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPromocodeGenerator>(x =>
            {
                WebUIConfig appConf = builder.Configuration.GetSection(WebUIConfig.SectionName).Get<WebUIConfig>();
                return new PromocodeGenerator(appConf.PromoocodeLenght);
            });
        }
    }
}
