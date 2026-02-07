using Microsoft.EntityFrameworkCore;
using StoreZoneV2API.Application.Services; // Add this using directive
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using StoreZoneV2API.Infrastructure.Data;
using StoreZoneV2API.Domain.Interfaces.Data;
using StoreZoneV2API.Infrastructure.Repositories;
using StoreZoneV2API.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
string? vaultUri = builder.Configuration["KeyVaultConfig:Endpoint"];

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
       options => options.MigrationsAssembly("StoreZoneV2API.Infrastructure"))
       );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.MapOpenApi();
}
else
{
    if (app.Environment.IsProduction())
    {
        if (!string.IsNullOrEmpty(vaultUri))
        {
            var secretClint = new SecretClient(new Uri(vaultUri), new DefaultAzureCredential());
            var secrit = secretClint.GetSecret("storezoneSQLConn");
            builder.Configuration["ConnectionStrings:DefaultConnection"] = secrit.Value.Value;
        }
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            options => options.MigrationsAssembly("StoreZoneV2API."))

            );
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ProductService>();
        app.UseHsts();
    }
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
