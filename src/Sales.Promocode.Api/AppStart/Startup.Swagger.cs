using Microsoft.OpenApi.Models;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options => {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Promocode.Api", Version = "v1" });                
            });
        }
    }
}
