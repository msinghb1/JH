using Microsoft.Extensions.Configuration;
using System;


namespace DataValidationFramework.Config
{    
    public static class AppConfig
    {
        public static IConfigurationRoot Settings { get; }

        static AppConfig()
        {
            Settings = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string Get(string key) => Settings[key];
    }

}

