using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebZi.Plataform.Data.Database;

namespace WebZi.Plataform.Data.Services
{
    internal static class DatabaseRegistry
    {
        public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? "Production";

            bool isDevelopment = string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(120));

                if (isDevelopment)
                {
                    options.LogTo(Console.WriteLine, LogLevel.Information)
                           .EnableSensitiveDataLogging();
                }
            });
        }
    }
}