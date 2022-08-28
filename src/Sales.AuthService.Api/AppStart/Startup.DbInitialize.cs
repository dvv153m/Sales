using Sales.Infrastructure.Data.Dapper.Context;
using Sales.Infrastructure.Data.Dapper.Migration;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void DbInitialize(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddSingleton<Database>();
        }
    }
}
