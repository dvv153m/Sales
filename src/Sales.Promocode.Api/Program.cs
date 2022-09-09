using Sales.Promocode.Api.AppStart;
using FluentMigrator.Runner;
using Sales.Infrastructure.Data.Migration;
using Sales.Infrastructure.Exception;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var startup = new Startup();
startup.Initialize(builder);

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
logger?.LogInformation("Starting Promocode.Api...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(startup.DebugCorsPolicy);
}
else
{
    app.UseCors(startup.ReleaseCorsPolicy);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseExceptionHandlerMiddleware(logger);

app.UseAuthorization();

#region Creation and migration database

using (var scope = app.Services.CreateScope())
{
    var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
    var migrationService = scope.ServiceProvider.GetService<IMigrationRunner>();
    try
    {
        databaseService.CreateDatabaseIfNotExists();
        migrationService.ListMigrations();
        migrationService.MigrateUp();
    }
    catch(Exception ex)
    {
        logger?.LogError(ex, "An error occurred database creation or migration");
        throw;
    }    
}

#endregion

app.MapControllers();

app.Run();
