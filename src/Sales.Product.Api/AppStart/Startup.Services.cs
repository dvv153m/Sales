using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Services;
using Sales.Infrastructure.Product.Data.Dapper.Repositories;

namespace Sales.Product.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
        }
    }
}
