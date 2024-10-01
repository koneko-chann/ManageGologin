using ManageGologin.Executor.InterfaceExecutor;
using ManageGologin.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManageGologin.Helper.LoggerHelper;

namespace ManageGologin.Executor
{
    public class InstallRabby : IExecutor
    {
        private bool _isInstalled;
        private IWebDriver _driver;
        private Profiles _profile;
        public InstallRabby(IWebDriver driver,Profiles profiles)
        {
            _isInstalled = false;
            _driver = driver;
            _profile = profiles;
        }
        public async Task Execute()
        {
            if (await CheckIfInstalled() == true)
            {
                LogDebug("Rabby is already installed");
                return;
            }
            LogDebug("Installing Rabby");
            await InstallRabbyWallet();
        }
        private async Task<bool> CheckIfInstalled()
        {
          await _driver.Navigate().GoToUrlAsync("chrome-extension://acmacodkjbdgmoleebolmdjonilkdbch/popup.html#/welcome");
          var existingElement=_driver.FindElements(By.ClassName("step-title"));
            if(existingElement.Count==0)
            {
                return false;
            }
            return true;
        }
        private async Task InstallRabbyWallet()
        {
            await _driver.Navigate().GoToUrlAsync("https://chromewebstore.google.com/detail/rabby-wallet/acmacodkjbdgmoleebolmdjonilkdbch?pli=1");
            await Task.Delay(3000);
            var installButton= _driver.FindElement(By.XPath("//*[@id=\"yDmH0d\"]/c-wiz/div/div/main/div/section[1]/section/div[2]/div/button"));
            installButton.Click();
            await Task.Delay(5000);
          var pageContent = _driver.PageSource;
            LogInfo(pageContent);
        }
    }
}
