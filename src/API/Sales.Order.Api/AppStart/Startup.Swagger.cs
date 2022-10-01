using Microsoft.OpenApi.Models;

namespace Sales.Order.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options => {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.Api", Version = "v1" });
                //options.EnableAnnotations();

                //builder.Services.AddApiVersioning();

                //чтоб авторизаваться через swagger
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Description = "Basic Authenticationusing this Basic Scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "basic"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        { 
                            Reference = new OpenApiReference
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] { }
                    }
                });
            });            
        }
    }
}
