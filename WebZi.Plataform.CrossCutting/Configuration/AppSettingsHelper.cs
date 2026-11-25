using Microsoft.Extensions.Configuration;

namespace WebZi.Plataform.CrossCutting.Configuration
{
    public static class AppSettingsHelper
    {
        private static readonly Lazy<IConfigurationRoot> _configuration = new(BuildConfiguration);
        private static IConfigurationRoot BuildConfiguration()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.SetBasePath(AppContext.BaseDirectory);
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!string.IsNullOrWhiteSpace(environment))
            {
                configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            }

            return configurationBuilder.Build();
        }

        public static string GetConnectionString()
        {
            return _configuration.Value.GetConnectionString("DefaultConnection");
        }

        public static string GetConnectionString(string connectionStringName)
        {
            return _configuration.Value.GetConnectionString(connectionStringName);
        }


        public static string GetValue(string section, string element)
        {
            IConfigurationSection configurationSection = _configuration.Value
                .GetSection(section)
                .GetSection(element);

            return configurationSection.Value;
        }

        public static string GetValue(string section)
        {
            IConfigurationSection configurationSection = _configuration.Value
                .GetSection(section);

            return configurationSection.Value;
        }
    }
}