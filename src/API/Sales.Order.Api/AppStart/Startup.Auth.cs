using Microsoft.AspNetCore.Authentication;
using Sales.Infrastructure.Authentication;

namespace Sales.Order.Api.AppStart
{
    public partial class Startup
    {
        void ConfigureAuth(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("BasicAuthentication")
                   .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            
            builder.Services.AddAuthorization();
        }
    }
}
