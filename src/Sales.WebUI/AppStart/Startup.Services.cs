using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Products;
using Sales.Core.Services;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();            

            //правила добавления в корзину
            builder.Services.AddScoped<CartRuleHandler>(x =>
            {
                var rule1 = new AvailableProductHandler();
                var rule2 = new UniqueProductHandler();

                rule1.SetNextHandler(rule2);
                return rule1;
            });
        }
    }
}
