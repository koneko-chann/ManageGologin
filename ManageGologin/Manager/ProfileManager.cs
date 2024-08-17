using KillChromeGarbageProcesses;
using ManageGologin.Attribute;
using ManageGologin.Helper;
using ManageGologin.Manager;
using ManageGologin.Models;
using ManageGologin.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using Selenium.Extensions;
using Selenium.WebDriver.UndetectedChromeDriver;
using Sl.Selenium.Extensions.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ManageGologin.ManagePhysicalPath
{
    public class ProfileManager : IProfileManager
    {
       // private IServiceProvider _serviceProvider;
        public List<Profiles>? Profiles { get; set; }
        private IProxyManager _proxyManager;
        public ProfileManager(IServiceProvider serviceProvider, IProxyManager proxyManager)
        {
            //this._serviceProvider = serviceProvider;
            this._proxyManager = proxyManager;
            Profiles = ProfileHelper.GetProfiles(_proxyManager);
        }
        public void SetProfiles(List<Profiles> profiles)
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
        public async Task<IWebDriver> OpenProfile(Profiles profiles, bool? startWithProxy = false)
        {
          
            ChromeProcessManager chromeProcessManager = new ChromeProcessManager();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; // Ẩn cửa sổ console
            service.SuppressInitialDiagnosticInformation = true; // Tắt thông tin chuẩn đoán ban đầu
            var options = new ChromeOptions();
            if (startWithProxy == true)
            {
                await options.GetDefaultSettingsAsync(profiles.ProfileName, Resources.ProfilePath, profiles.Proxy);
            }
            else
            {
                await options.GetDefaultSettingsAsync(profiles.ProfileName, Resources.ProfilePath);
            }
            await profiles.SetPreferenceGeo(startWithProxy);
            var driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://iphey.com");

            return driver;
        }
        public async Task<IWebDriver> OpenProfileWithScript(Profiles profiles, Dictionary<string, string> webAndJs, bool? startWithProxy = false )
        {
            var webDriver = await OpenProfile(profiles,startWithProxy);
            IJavaScriptExecutor jsEx = (IJavaScriptExecutor)webDriver;
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(3));

            foreach (var key in webAndJs.Keys)
            {
                var web = key;
                var js = webAndJs[key];

                // Điều hướng đến URL
                await webDriver.Navigate().GoToUrlAsync(web);

                // Chờ cho đến khi trang web tải xong (DOM hoàn tất tải)
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                // Thực thi mã JavaScript sau khi trang đã tải xong
                jsEx.ExecuteAsyncScript(js);
            }

            return webDriver;
        }
        public async Task CloseProfile(ChromeDriver driver)
        {
            driver.Close();
            driver.Quit();
        }


    }
}
