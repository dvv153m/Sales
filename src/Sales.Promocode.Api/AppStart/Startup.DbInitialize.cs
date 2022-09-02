using FluentMigrator.Runner;
using Sales.Contracts.Configuration;
using Sales.Infrastructure.Data.Context;
using Sales.Infrastructure.Data.Migration;
using Sales.Infrastructure.Promocode.Data.Dapper;


namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        void DbInitialize(WebApplicationBuilder builder)
        {
            PromocodeApiOptions promocodeApiOptions = builder.Configuration.GetSection(PromocodeApiOptions.SectionName).Get<PromocodeApiOptions>();
            
            builder.Services.AddSingleton<DapperContext>(x =>
            {
                return new DapperContext(promocodeApiOptions.SqlConnectionString, promocodeApiOptions.MasterConnectionString);
            });
            
            builder.Services.AddSingleton<Database>();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(promocodeApiOptions.SqlConnectionString)
                    .ScanIn(typeof(PromocodeDataMigrationEntrypoint).Assembly).For.Migrations())
                .AddLogging(config => config.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
