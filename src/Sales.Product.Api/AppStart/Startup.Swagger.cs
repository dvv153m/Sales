using Microsoft.OpenApi.Models;

namespace Sales.Product.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options => {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.Api", Version = "v1" });
            });
        }
    }
}
