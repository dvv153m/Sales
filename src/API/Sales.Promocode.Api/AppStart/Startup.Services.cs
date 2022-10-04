using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Authentication;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Services;
using Sales.Infrastructure.Authentication;
using Sales.Infrastructure.Promocode.Data.Dapper.Repositories;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {            
            builder.Services.AddScoped<PromocodeRepository>();
            builder.Services.AddScoped<IPromocodeRepository>(x => new PromocodeCacheDecoratorRepository(x.GetRequiredService<PromocodeRepository>()));

            builder.Services.AddScoped<IPromocodeGenerator, PromocodeGenerator>();

            builder.Services.AddScoped<IPromocodeService>(x =>
            {
                PromocodeApiSettings appConf = builder.Configuration.GetSection(PromocodeApiSettings.SectionName).Get<PromocodeApiSettings>();
                return new PromocodeService(x.GetRequiredService<IPromocodeRepository>(),
                                            x.GetRequiredService<IPromocodeGenerator>(),
                                            appConf.PromocodeLenght);
            });            
        }
    }
}
