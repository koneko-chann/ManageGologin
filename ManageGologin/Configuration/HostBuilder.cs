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

namespace ManageGologin.Configuration
{
    public static class HostBuilder
    {
        public static IServiceProvider ServiceProvider { get; private set; }
     public   static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IProfileManager, ProfileManager>();
                    services.AddTransient<IWebDriver, ChromeDriver>();
                    services.AddScoped<GeolocationService>();
                    services.AddTransient<Form1>();
                }); 
        }
    }
}
