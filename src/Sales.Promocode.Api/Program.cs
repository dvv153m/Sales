using Sales.Promocode.Api.AppStart;
using Sales.Infrastructure.Data.Dapper.Migration;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}

app.UseHttpsRedirection();

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
