using Sales.AuthService.Api.AppStart;
using Sales.Infrastructure.Data.Dapper.Migration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var startup = new Startup();
startup.Initialize(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
    try
    {
        databaseService.CreateDatabase("memocards3");
    }
    catch
    {
        //log errors or ...
        throw;
    }
    //var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
    //migrator.ListMigrations();
    //migrator.MigrateUp();
}

app.MapControllers();

app.Run();
