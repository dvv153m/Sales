using FluentMigrator.Runner;
using Sales.Infrastructure.Data.Migration;
using Sales.Infrastructure.Exception;
using Sales.Order.Api.AppStart;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup();
startup.Initialize(builder);

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
logger?.LogInformation("Starting Order.Api...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    /*app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });*/
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandlerMiddleware(logger);

app.UseAuthentication();
app.UseAuthorization();

//todo ������� � infrastructure
#region Creation and migration database

using (var scope = app.Services.CreateScope())
{
    var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
    var migrationService = scope.ServiceProvider.GetService<IMigrationRunner>();
    try
    {
        databaseService.CreateDatabaseIfNotExists();
        //migrationService.ListMigrations();
        migrationService.MigrateUp();
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "An error occurred database creation or migration");
        throw;
    }
}

#endregion

app.MapControllers();

app.Run();
