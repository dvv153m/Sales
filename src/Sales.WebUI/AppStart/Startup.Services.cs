using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Services;


namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();   
        }
    }
}
