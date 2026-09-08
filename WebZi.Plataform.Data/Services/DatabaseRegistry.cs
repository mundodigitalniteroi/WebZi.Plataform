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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(120))
                    .LogTo(Console.WriteLine, LogLevel.Information) // Exibe as queries executadas no BD pelo EF
            );
        }
    }
}