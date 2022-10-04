using FluentMigrator.Runner;
using Sales.Contracts.Configuration;
using Sales.Infrastructure.Data.Context;
using Sales.Infrastructure.Data.Migration;
using Sales.Infrastructure.Order.Data.Dapper;

namespace Sales.Order.Api.AppStart
{
    public partial class Startup
    {
        void DbInitialize(WebApplicationBuilder builder)
        {
            OrderApiSettings orderApiOptions = builder.Configuration.GetSection(OrderApiSettings.SectionName).Get<OrderApiSettings>();

            builder.Services.AddSingleton<DapperContext>(x =>
            {
                return new DapperContext(orderApiOptions.SqlConnectionString, orderApiOptions.MasterConnectionString);
            });

            builder.Services.AddSingleton<Database>();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(orderApiOptions.SqlConnectionString)
                    .ScanIn(typeof(OrderDataMigrationEntrypoint).Assembly).For.Migrations())
                .AddLogging(config => config.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
