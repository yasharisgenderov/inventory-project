using System.Net.Http.Headers;
using Inventory.API.Configs;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Inventory.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var envName = builder.Environment.EnvironmentName;
            // Add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDBConnection")));

            // Register repositories for dependency injection
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<ISaleRepository, SaleRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            // Register services for dependency injection
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ISalesService, SalesService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IStockManagementService, StockManagementService>();

            // OpenAI açarı konfiqurasiyadan oxunur
            var apiKey = builder.Configuration["OpenAI:ApiKey"];

            // HttpClient qeydiyyatı
            builder.Services.AddHttpClient("OpenAI", client =>
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            });
            
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Register Swagger generator, defining the version and title.
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", SwaggerConfig.GetOpenApiInfo(envName));
                c.CustomSchemaIds(type => type.Name);
            });


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
