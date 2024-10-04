using ManageGologin.ManagePhysicalPath;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using ManageGologin.Services;
using ManageGologin.Manager;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ManageGologin.Configuration
{
    public static class HostBuilder
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IHostBuilder CreateHostBuilder()
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();
            logger.Debug("NLog configuration loaded.");
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IProfileManager, ProfileManager>();
                    services.AddTransient<IWebDriver, ChromeDriver>();
                    services.AddScoped<GeolocationService>();
                    services.AddTransient<IProxyManager, ProxyManager>();
                    services.AddSingleton<Form1>();
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders(); // Clear default logging providers
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                        loggingBuilder.AddNLog(); // Add NLog as logging provider
                    });

                })
                .UseNLog();
        }
    }
}
