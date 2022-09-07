using Sales.Core.Rules.Products;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();            

            //правила добавления в корзину
            builder.Services.AddScoped<CartAddRules>(x =>
            {
                var rule1 = new ProductAvailabilityRule();
                var rule2 = new ProductUniquenessRule();

                rule1.SetNextRule(rule2);
                return rule1;
            });
        }
    }
}
