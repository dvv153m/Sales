using FluentMigrator.Runner;
using Sales.Contracts.Configuration;
using Sales.Infrastructure.Data.Context;
using Sales.Infrastructure.Data.Migration;
using Sales.Infrastructure.Product.Data.Dapper;

namespace Sales.Product.Api.AppStart
{
    public partial class Startup
    {
        void DbInitialize(WebApplicationBuilder builder)
        {
            ProductApiSettings promocodeApiOptions = builder.Configuration.GetSection(ProductApiSettings.SectionName).Get<ProductApiSettings>();

            builder.Services.AddSingleton<DapperContext>(x =>
            {
                return new DapperContext(promocodeApiOptions.SqlConnectionString, promocodeApiOptions.MasterConnectionString);
            });

            builder.Services.AddSingleton<Database>();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(promocodeApiOptions.SqlConnectionString)
                    .ScanIn(typeof(ProductDataMigrationEntrypoint).Assembly).For.Migrations())
                .AddLogging(config => config.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
