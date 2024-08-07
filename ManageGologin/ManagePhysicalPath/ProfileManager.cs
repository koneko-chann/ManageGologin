using KillChromeGarbageProcesses;
using ManageGologin.Attribute;
using ManageGologin.Helper;
using ManageGologin.Models;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.WebDriver.UndetectedChromeDriver;
using Sl.Selenium.Extensions.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ManageGologin.ManagePhysicalPath
{
    public class ProfileManager : IProfileManager
    {
        [Path(ErrorMessage = "Path is not valid")]
        public string ProfilePath { get; set; } = "E:\\Gologin";
        private IServiceProvider _serviceProvider;

        public ProfileManager(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        public List<Profiles> GetProfiles()
        {
            var browserDirectory = Path.Combine(this.ProfilePath, "browser");
            var profiles = new List<Profiles>();
            if (Directory.Exists(browserDirectory))
            {
                var directories = Directory.GetDirectories(browserDirectory);
                uint stt = 1;
                foreach (var directory in directories)
                {
                    var profile = Path.GetFileName(directory);
                    profiles.Add(new Profiles
                    {
                        STT = stt,
                        ProfileName = profile,
                        DataPath = Path.Combine(directory),
                    });
                    stt++;
                }
            }
            return profiles;
        }
        public async Task<IWebDriver> OpenProfile(string name)
        {
            ChromeProcessManager chromeProcessManager = new ChromeProcessManager();
            var options = new ChromeOptions();
           await options.GetDefaultSettingsAsync(name, this.ProfilePath, new Models.CustomProxy("45.43.64.130:6388:mwyvhbnr:retc2ujlzvq3"));
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://x.com");
            
            return driver;
        }
        public async Task CloseProfile(ChromeDriver driver)
        {
            driver.Close();
            driver.Quit();
        }


    }
}
