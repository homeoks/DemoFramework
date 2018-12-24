using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class ApplicationSetting
    {
        private static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot;
        }

        public static IConfigurationRoot Get()
        {
            var applicationExeDirectory = ApplicationExeDirectory();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
#if DEBUG

            env = env == "Development" ? "appsettings.json" : $"appsettings.{env}.json";
#else
            env="appsettings.production.json";
#endif
            var builder = new ConfigurationBuilder()
                .SetBasePath(applicationExeDirectory)
                .AddJsonFile(env);

            return builder.Build();
        }
    }
}
