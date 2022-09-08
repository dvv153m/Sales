using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Orders;
using Sales.Core.Rules.Products;
using Sales.Core.Services;
using Sales.Infrastructure.Services;

namespace Sales.Order.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IHttpProxy, HttpProxy>();
            builder.Services.AddScoped<IProductClient, ProductClient>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            //правила добавления в корзину
            builder.Services.AddScoped<OrderAddRules>(x =>
            {
                OrderApiOptions appConf = builder.Configuration.GetSection(OrderApiOptions.SectionName).Get<OrderApiOptions>();

                var rule1 = new OneOrderForOnePromocodeRule(x.GetRequiredService<IOrderRepository>());
                var rule2 = new OrderMinPriceRule(appConf.MinimalOrderPrice);

                rule1.SetNextRule(rule2);
                return rule1;
            });
        }
    }
}
