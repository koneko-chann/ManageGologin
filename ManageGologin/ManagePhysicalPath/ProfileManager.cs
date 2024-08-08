using KillChromeGarbageProcesses;
using ManageGologin.Attribute;
using ManageGologin.Helper;
using ManageGologin.Models;
using ManageGologin.Pagination;
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
        private IServiceProvider _serviceProvider;
        public List<Profiles>? Profiles { get; set; }
        public ProfileManager(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            Profiles=ProfileHelper.GetProfiles();
        }
        void IProfileManager.SetProfiles(List<Profiles> profiles)
        {
            Profiles = profiles;
        }
    
        public List<Profiles> GetProfiles()
        {
            return Profiles;
        }
        //Get profiles with paging
        public List<Profiles> GetProfiles(PagingParameters pagingParameters)
        {
            var items = Profiles.Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize).ToList();
            return items;
        }
        public async Task<IWebDriver> OpenProfile(Profiles profiles)
        {
            ChromeProcessManager chromeProcessManager = new ChromeProcessManager();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; // Ẩn cửa sổ console
            service.SuppressInitialDiagnosticInformation = true; // Tắt thông tin chuẩn đoán ban đầu
            var options = new ChromeOptions();
           await options.GetDefaultSettingsAsync(profiles.ProfileName, Resources.ProfilePath, profiles.Proxy);
            var driver = new ChromeDriver(service,options);
            driver.Navigate().GoToUrl("https://iphey.com");
            
            return driver;
        }
        public async Task CloseProfile(ChromeDriver driver)
        {
            driver.Close();
            driver.Quit();
        }


    }
}
