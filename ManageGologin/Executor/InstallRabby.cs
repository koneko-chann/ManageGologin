using ManageGologin.Executor.InterfaceExecutor;
using ManageGologin.Helper;
using ManageGologin.Models;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TextCopy;

namespace ManageGologin.Executor
{
    public class InstallRabby : IExecutor
    {
        private bool _isInstalled;
        private IWebDriver _driver;
        private Profiles _profile;
        public InstallRabby(ref ChromeDriver driver, Profiles profiles)
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
            await Task.Delay(3000);
            LogDebug("Installing Rabby");
            await CreateWallet();
        }
        private async Task<bool> CheckIfInstalled()
        {
            await _driver.Navigate().GoToUrlAsync("chrome-extension://acmacodkjbdgmoleebolmdjonilkdbch/popup.html#/welcome");
            var pageContent = _driver.PageSource;
            if (pageContent.Contains("rabby"))
            {
                return false;
            }
            return false;
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
        [STAThread]
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
                LogInfo(winHandle);
            }
            PerformActionWithWindowCheck(() => _driver.FindElement(By.CssSelector("#root > div > div > div > div.text-center.mt-\\[76px\\] > button")).Click());
            await Task.Delay(1000);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div[2]/div[3]")).Click());

            // Clipboard handling
            var copiedText = await ClipboardService.GetTextAsync();
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
                var jsEx = (IJavaScriptExecutor)_driver;
                jsEx.ExecuteScript("document.querySelector(\"#rc-tabs-0-panel-hd > div > div > div > div > div > div.ant-table-body > table > tbody > tr:nth-child(2) > td.ant-table-cell.cell-add > button\").click()");
            });
            await Task.Delay(500);

            PerformActionWithWindowCheck(() => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/button")).Click());
            SaveWallet(_profile, copiedText);

            foreach (string winHandle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(winHandle);
                LogInfo(winHandle);
            }
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
        private void SaveWallet(Profiles profiles, string mnemonic)
        {
            // Set EPPlus license context to non-commercial (required)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // File path to save the Excel file (modify path as needed)
            var filePath = Resources.RabbyWalletPath;

            // Create or open the Excel file
            FileInfo fileInfo = new FileInfo(filePath);

            using (var package = new ExcelPackage(fileInfo))
            {
                // Add or get an existing worksheet named "Wallets"
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Wallets")
                                ?? package.Workbook.Worksheets.Add("Wallets");

                // Find the next empty row
                int nextRow = worksheet.Dimension == null ? 1 : worksheet.Dimension.End.Row + 1;

                // Write headers if it's the first row
                if (nextRow == 1)
                {
                    worksheet.Cells[nextRow, 1].Value = "Profile Name";
                    worksheet.Cells[nextRow, 2].Value = "Mnemonic";
                    nextRow++; // Move to the next row for data
                }

                // Write profile name and mnemonic to the next row
                worksheet.Cells[nextRow, 1].Value = profiles.ProfileName; // Profile Name in column 1
                worksheet.Cells[nextRow, 2].Value = mnemonic;      // Mnemonic in column 2

                // Save the changes to the file
                package.Save();

                LogInfo("Profile and mnemonic saved successfully!");
            }
        }

    }
}
