using ManageGologin.ManagePhysicalPath;
using ManageGologin.Manager;
using ManageGologin.Runtime_Processor;
using ManageGologin.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
                    services.AddSingleton<FormRuntimeExecutor>();

                })
                .UseNLog();
        }
    }
}
