using Microsoft.Extensions.Configuration;

namespace WebZi.Plataform.CrossCutting.Configuration
{
    public class AppSettingsHelper
    {
        private readonly IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(path: "appsettings.json", optional: false);

        public string GetConnectionString()
        {
            return builder.Build().GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString(string connectionStringName)
        {
            return builder.Build().GetConnectionString(connectionStringName);
        }

        public string GetValue(string section, string element)
        {
            IConfigurationRoot configuration = builder.Build();

            IConfigurationSection configurationSection = configuration.GetSection(section).GetSection(element);

            return configurationSection.Value;
        }

        public string GetValue(string section)
        {
            IConfigurationRoot configuration = builder.Build();

            IConfigurationSection configurationSection = configuration.GetSection(section);

            return configurationSection.Value;
        }
    }
}