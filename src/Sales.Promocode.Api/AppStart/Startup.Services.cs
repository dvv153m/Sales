using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Services;
using Sales.Infrastructure.Data.Dapper.Repositories;
using Sales.Infrastructure.Services;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {            
            builder.Services.AddSingleton<IPromocodeGenerator>(x =>
            {
                PromocodeApiConfig appConf = builder.Configuration.GetSection(PromocodeApiConfig.SectionName).Get<PromocodeApiConfig>();
                return new PromocodeGenerator(appConf.PromoocodeLenght);
            });

            builder.Services.AddScoped<IPromocodeRepository, PromocodeRepository>();
            builder.Services.AddScoped<IPromocodeService, PromocodeService>();
        }
    }
}
