using ManageGologin.Executor;
using ManageGologin.Helper;
using ManageGologin.Manager;
using ManageGologin.Models;
using ManageGologin.Pagination;
using ManageGologin.Runtime_Processor;
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
        private List<string> _taskList;
        private FormRuntimeExecutor _formRuntimeExecutor;
        public ProfileManager(IServiceProvider serviceProvider, IProxyManager proxyManager, ILogger<ProfileManager> logger,FormRuntimeExecutor formRuntimeExecutor)
        {
            //this._serviceProvider = serviceProvider;
            this._proxyManager = proxyManager;
            Profiles = ProfileHelper.GetProfiles(_proxyManager);
            _logger = logger;
            _serviceProvider = serviceProvider;
            _formRuntimeExecutor = formRuntimeExecutor;
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
            //service.HideCommandPromptWindow = true; // Ẩn cửa sổ console
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
            List<string> assemblyNames=GetTaskList();
            await profiles.SetPreferenceGeo(startWithProxy);
            var driver = new ChromeDriver(service, options);
            _formRuntimeExecutor.ChoosedClasses = assemblyNames;
            _formRuntimeExecutor.Execute(driver,profiles);
            //CloseProfile(driver);
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
                await webDriver.Navigate().GoToUrlAsync(web);

                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                jsEx.ExecuteAsyncScript(js);
                Console.WriteLine("Test");
            }

            return webDriver;
        }
        public void CloseProfile(ChromeDriver driver)
        {
            try
            {
                if (driver != null)
                {
                    driver.Close();
                    LogInfo("Browser window closed successfully.");
                }
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                LogInfo($"Error during closing browser window: {ex.Message}");
            }

            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    LogInfo("Driver quit successfully.");
                }
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                LogInfo($"Error during quitting the driver: {ex.Message}");
            }
            finally
            {
                // Set driver to null to indicate it has been closed or quit
                driver = null;
            }
        }
        public List<string> GetTaskList()
        {
            var f1 = _serviceProvider.GetService<Form1>();
            var radioButtons = f1.TaskPanel.Controls.OfType<RadioButton>().ToList();
            List<string> strings = new List<string>();
            foreach (var radioButton in radioButtons)
            {
                LogInfo($"RadioButton Name: {radioButton.AccessibleName}, Checked: {radioButton.Checked}");
                if (radioButton.Checked)
                {
                    strings.Add(radioButton.AccessibleName);
                }
            }
            return strings;
        }
    }

}
