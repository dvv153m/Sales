using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Services;
using Sales.Infrastructure.Data.Dapper.Repositories;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {                        
            builder.Services.AddScoped<IPromocodeRepository, PromocodeRepository>();

            builder.Services.AddScoped<IPromocodeService>(x =>
            {
                PromocodeApiConfig appConf = builder.Configuration.GetSection(PromocodeApiConfig.SectionName).Get<PromocodeApiConfig>();
                return new PromocodeService(x.GetRequiredService<IPromocodeRepository>(), appConf.PromoocodeLenght);
            });            
        }
    }
}
