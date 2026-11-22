using MO.Store.Api.Catalog;
using MO.Store.Infrastructure;
using MO.Store.Infrastructure.Persistence;
using MO.Store.Infrastructure.Persistence.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("StoreDb")
    ?? throw new InvalidOperationException("Connection string 'StoreDb' is not configured.");

builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await using var scoped = app.Services.CreateAsyncScope();

    var db = scoped.ServiceProvider.GetRequiredService<StoreDbContext>();
    await DevDataSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();

app.MapProductEndpoints();

app.Run();