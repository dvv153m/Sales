using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Orders;
using Sales.Core.Rules.Products;
using Sales.Core.Services;
using Sales.Infrastructure.Order.Data.Dapper.Repositories;
using Sales.Infrastructure.Services;

namespace Sales.Order.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            OrderApiOptions appConf = builder.Configuration.GetSection(OrderApiOptions.SectionName).Get<OrderApiOptions>();

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IHttpProxy, HttpProxy>();            
            builder.Services.AddScoped<IProductClient, ProductClient>(x =>
            {                
                return new ProductClient(x.GetRequiredService<IHttpProxy>(), appConf.ProductApiUrl);
            });
            builder.Services.AddScoped<IPromocodeClient, PromocodeClient>(x =>
            {
                return new PromocodeClient(x.GetRequiredService<IHttpProxy>(), appConf.PromocodeApiUrl);
            });

            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<IOrderRepository>(x => new OrderCacheDecoratorRepository(x.GetRequiredService<OrderRepository>()));

            builder.Services.AddScoped<IOrderService, OrderService>();

            //правила при оформлении заказа
            builder.Services.AddScoped<OrderAddRules>(x =>
            {                
                var rule1 = new OneOrderForOnePromocodeRule(x.GetRequiredService<IOrderRepository>());
                var rule2 = new OrderMinPriceRule(appConf.MinimalOrderPrice);

                rule1.SetNextRule(rule2);
                return rule1;
            });

            //правила добавления в корзину
            builder.Services.AddScoped<CartAddProductRules>(x =>
            {
                var rule1 = new ProductAvailabilityRule();
                var rule2 = new ProductUniquenessRule();

                rule1.SetNextRule(rule2);
                return rule1;
            });

            
        }
    }
}
