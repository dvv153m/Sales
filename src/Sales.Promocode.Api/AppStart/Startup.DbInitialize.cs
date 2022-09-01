using FluentMigrator.Runner;
using Sales.Contracts.Configuration;
using Sales.Infrastructure.Data.Dapper;
using Sales.Infrastructure.Data.Dapper.Context;
using Sales.Infrastructure.Data.Dapper.Migration;

namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void DbInitialize(WebApplicationBuilder builder)
        {
            PromocodeApiOptions promocodeApiConfig = builder.Configuration.GetSection(PromocodeApiOptions.SectionName).Get<PromocodeApiOptions>();

            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddSingleton<Database>();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(promocodeApiConfig.SqlConnectionString)
                    .ScanIn(typeof(DataMigrationEntrypoint).Assembly).For.Migrations())
                .AddLogging(config => config.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
