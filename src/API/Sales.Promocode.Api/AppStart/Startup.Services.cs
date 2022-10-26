using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<PromocodeRepository>();
            //builder.Services.AddScoped<IPromocodeRepository, PromocodeCacheDecoratorRepository>();
            //builder.Services.AddScoped<IPromocodeRepository>(x => new PromocodeCacheDecoratorRepository(x.GetRequiredService<PromocodeRepository>()));
            builder.Services.AddScoped<IPromocodeRepository>(provider =>
            {
                PromocodeApiSettings promocodeApiOptions = builder.Configuration.GetSection(PromocodeApiSettings.SectionName).Get<PromocodeApiSettings>();
                var promoRepository = provider.GetService<PromocodeRepository>();
                return new PromocodeCacheDecoratorRepository(promoRepository,
                                                             provider.GetService<IMemoryCache>(),
                                                             promocodeApiOptions.CacheOptions.AbsoluteExpirationRelativeToNow);
            });

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
