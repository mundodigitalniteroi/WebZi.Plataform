using Microsoft.Extensions.Configuration;

namespace WebZi.Plataform.CrossCutting.Configuration
{
    public static class AppSettingsHelper
    {
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
            return BuildConfiguration().GetConnectionString("DefaultConnection");
        }

        public static string GetConnectionString(string connectionStringName)
        {
            return BuildConfiguration().GetConnectionString(connectionStringName);
        }

        public static string GetValue(string section, string element)
        {
            IConfigurationRoot configuration = BuildConfiguration();

            IConfigurationSection configurationSection = configuration.GetSection(section).GetSection(element);

            return configurationSection.Value;
        }

        public static string GetValue(string section)
        {
            IConfigurationRoot configuration = BuildConfiguration();

            IConfigurationSection configurationSection = configuration.GetSection(section);

            return configurationSection.Value;
        }
    }
}