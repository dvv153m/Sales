using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Services;
using Sales.Infrastructure.Promocode.Data.Dapper.Repositories;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {                        
            builder.Services.AddScoped<IPromocodeRepository, PromocodeRepository>();

            builder.Services.AddScoped<IPromocodeService>(x =>
            {
                PromocodeApiOptions appConf = builder.Configuration.GetSection(PromocodeApiOptions.SectionName).Get<PromocodeApiOptions>();
                return new PromocodeService(x.GetRequiredService<IPromocodeRepository>(), appConf.PromoocodeLenght);
            });            
        }
    }
}
