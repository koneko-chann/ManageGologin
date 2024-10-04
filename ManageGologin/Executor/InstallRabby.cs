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

namespace ManageGologin.Executor
{
    public class InstallRabby : IExecutor
    {
        private bool _isInstalled;
        private IWebDriver _driver;
        private Profiles _profile;
        public InstallRabby(IWebDriver driver, Profiles profiles)
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
        await    Task.Delay(3000);   
            LogDebug("Installing Rabby");
            //await InstallRabbyWallet();
            await CreateWallet();

        }
        private async Task<bool> CheckIfInstalled()
        {
            await _driver.Navigate().GoToUrlAsync("chrome-extension://acmacodkjbdgmoleebolmdjonilkdbch/popup.html#/welcome");
            var existingElement = _driver.FindElements(By.ClassName("step-title"));
            if (existingElement.Count == 0)
            {
                return false;
            }
            return true;
        }   
        private async Task InstallRabbyWallet()
        {
            await _driver.Navigate().GoToUrlAsync("https://chromewebstore.google.com/detail/rabby-wallet/acmacodkjbdgmoleebolmdjonilkdbch?pli=1");
            await Task.Delay(3000);
            var installButton = _driver.FindElement(By.XPath("//*[@id=\"yDmH0d\"]/c-wiz/div/div/main/div/section[1]/section/div[2]/div/button"));
            
            /*installButton.Click();
            await Task.Delay(5000);
            var pageContent = _driver.PageSource;
            LogInfo(pageContent);*/
        }
        private async Task CreateWallet()
        {
            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div/div/section/footer/button")).Click());
            await Task.Delay(2000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div/div/section/footer/a/button")).Click());
            await Task.Delay(2000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div/div/div[3]/div[1]/div")).Click());
            await Task.Delay(1000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.Id("password")).SendKeys("12345678"));
            await Task.Delay(1000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.Id("confirmPassword")).SendKeys("12345678"));
            await Task.Delay(1000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div/div/div/div/form/div[3]/button")).Click());
            await Task.Delay(2000);
            foreach (string winHandle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(winHandle);
            }
            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[2]/button")).Click());
            await Task.Delay(1000);
            
            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[2]/div[3]")).Click());

            // Clipboard handling
            var copiedText = Clipboard.GetText();
            LogInfo(copiedText);

            await Task.Delay(3000);
            string winHandleBefore = _driver.CurrentWindowHandle;
            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[3]/button")).Click());
            await Task.Delay(2000);
            foreach (string winHandle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(winHandle);
            }


         /*   _driver.Navigate().GoToUrl("https://chrome-extension://acmacodkjbdgmoleebolmdjonilkdbch/index.html#/import/select-address?hd=HD%20Key%20Tree&keyringId=1");
           LogInfo(_driver.Url);*/
          //  Thread.Sleep(1000000000);
            await Task.Delay(2000);

            PerformActionWithWindowCheck(() =>
            {
                LogInfo(_driver.Url);
                var jsEx= (IJavaScriptExecutor)_driver;
                jsEx.ExecuteScript("document.querySelector(\"#rc-tabs-0-panel-hd > div > div > div > div > div > div.ant-table-body > table > tbody > tr:nth-child(2) > td.ant-table-cell.cell-add > button\").click()");
            });
            await Task.Delay(500);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/button")).Click());
        }

        private void PerformActionWithWindowCheck(Action action)
        {
            const int maxRetryAttempts = 3; // Maximum number of retries
            int attempt = 0;
            bool actionCompleted = false;

            while (!actionCompleted && attempt < maxRetryAttempts)
            {
                try
                {
                    action(); // Try executing the action
                    actionCompleted = true; // If no exception, mark as completed
                }
                catch (NoSuchWindowException)
                {
                    attempt++;
                    LogInfo($"Attempt {attempt}: Window not found. Switching to the first window.");

                    // If the window is not found, switch to the first window
                    var firstWindow = _driver.WindowHandles.First();
                    _driver.SwitchTo().Window(firstWindow);

                    if (attempt >= maxRetryAttempts)
                    {
                        throw new Exception("Failed after multiple attempts to retry the action.");
                    }

                    // Retry the action after switching windows
                }
            }
        }

    }
}
