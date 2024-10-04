using ManageGologin.Executor;
using ManageGologin.Helper;
using ManageGologin.Manager;
using ManageGologin.Models;
using ManageGologin.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ManageGologin.ManagePhysicalPath
{
    public class ProfileManager : IProfileManager
    {
        // private IServiceProvider _serviceProvider;
        public List<Profiles>? Profiles { get; set; }
        private IProxyManager _proxyManager;
        private ILogger<ProfileManager> _logger;
        private IServiceProvider _serviceProvider;
        public ProfileManager(IServiceProvider serviceProvider, IProxyManager proxyManager, ILogger<ProfileManager> logger)
        {
            //this._serviceProvider = serviceProvider;
            this._proxyManager = proxyManager;
            Profiles = ProfileHelper.GetProfiles(_proxyManager);
            _logger = logger;
            _serviceProvider = serviceProvider;
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
            if (_serviceProvider.GetService<Form1>().installRabbyBtn.Checked == true)
            {
                options.AddExtension(Resources.RabbyWalletExtension);
            }
            await profiles.SetPreferenceGeo(startWithProxy);
            var driver = new ChromeDriver(service, options);
            var RabbyInstall = new InstallRabby(ref driver, profiles);
            await RabbyInstall.Execute();

            CloseProfile(ref driver);
            if (driver == null || driver.SessionId == null)
            {
                return null;
            }
            return driver;
        }
        public async Task<IWebDriver> OpenProfileWithScript(Profiles profiles, Dictionary<string, string> webAndJs, bool? startWithProxy = false)
        {
            var webDriver = await OpenProfile(profiles, startWithProxy);
            IJavaScriptExecutor jsEx = (IJavaScriptExecutor)webDriver;
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(3));

            foreach (var key in webAndJs.Keys)
            {
                var web = key;
                var js = webAndJs[key];
                // Điều hướng đến URL
                await webDriver.Navigate().GoToUrlAsync(web);

                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                jsEx.ExecuteAsyncScript(js);
                Console.WriteLine("Test");
            }

            return webDriver;
        }
        public void CloseProfile(ref ChromeDriver driver)
        {
            try
            {
                if (driver != null)
                {
                    // Attempt to close the browser window
                    driver.Close();
                    Console.WriteLine("Browser window closed successfully.");
                }
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                Console.WriteLine($"Error during closing browser window: {ex.Message}");
            }

            try
            {
                if (driver != null)
                {
                    // Attempt to quit the driver and close all associated windows
                    driver.Quit();
                    Console.WriteLine("Driver quit successfully.");
                }
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                Console.WriteLine($"Error during quitting the driver: {ex.Message}");
            }
            finally
            {
                // Set driver to null to indicate it has been closed or quit
                driver = null;
            }
        }



    }
}
