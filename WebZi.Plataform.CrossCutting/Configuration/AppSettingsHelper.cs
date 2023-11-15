using Microsoft.Extensions.Configuration;

namespace WebZi.Plataform.CrossCutting.Configuration
{
    public static class AppSettingsHelper
    {
        private static readonly IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(path: "appsettings.json", optional: false);

        public static string GetConnectionString()
        {
            return builder.Build().GetConnectionString("DefaultConnection");
        }

        public static string GetConnectionString(string connectionStringName)
        {
            return builder.Build().GetConnectionString(connectionStringName);
        }

        public static string GetValue(string section, string element)
        {
            IConfigurationRoot configuration = builder.Build();

            IConfigurationSection configurationSection = configuration.GetSection(section).GetSection(element);

            return configurationSection.Value;
        }

        public static string GetValue(string section)
        {
            IConfigurationRoot configuration = builder.Build();

            IConfigurationSection configurationSection = configuration.GetSection(section);

            return configurationSection.Value;
        }
    }
}