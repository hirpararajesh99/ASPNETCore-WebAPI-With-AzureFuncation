using Microsoft.Extensions.Configuration;
using System.IO;


namespace StockExchangeFunctions
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfig()
        {
            string fileName = "local.settings.json";
            if (!File.Exists(Directory.GetCurrentDirectory() + "/" + fileName))
                fileName = "appsettings.json";


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
        public static IConfiguration GetAppConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
