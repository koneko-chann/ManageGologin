using ManageGologin.Models;
using OpenQA.Selenium.Chrome;

namespace ManageGologin.Helper
{
    public static class OptionsDefault
    {
        public async static Task GetDefaultSettingsAsync(this ChromeOptions options, string name, string profilePath, CustomProxy proxy)
        {
            // Thiết lập BinaryLocation
            options.BinaryLocation = Path.Combine(profilePath, "orbita-browser", "chrome.exe");

            // Các tùy chọn cần loại trừ
            var excludedArguments = new List<string>
            {
                "--allow-pre-commit-input",
                "--disable-background-networking",
                "--disable-backgrounding-occluded-windows",
                "--disable-client-side-phishing-detection",
                "--disable-default-apps",
                "--disable-hang-monitor",
                "--disable-popup-blocking",
                "--disable-prompt-on-repost",
                "--disable-sync",
                "--enable-automation",
                "--enable-logging",
                "--log-level=0",
                "--no-first-run",
                "--no-service-autorun",
                "--password-store=basic",
                "--remote-debugging-port=0",
                "--test-type=webdriver",
                "--use-mock-keychain"
            };
            options.AddExcludedArguments(excludedArguments);

            // Các tùy chọn cài đặt khác
            options.AddArgument("--lang=en-GB");
            options.AddArgument("--disable-encryption");
            options.AddArgument("--donut-pie=undefined");
            options.AddArgument("--webrtc-ip-handling-policy=default_public_interface_only");
            options.AddArgument("--font-masking-mode=2");
            //set default size as 980 * 845
            options.AddArgument("--window-size=980,845");
            // Thiết lập user-data-dir
            options.AddArgument("--user-data-dir=" + Path.Combine(profilePath, "browser", name));
            options.AddArgument($"--proxy-server=http://{proxy.ToString()}");
            // Thiết lập proxy nếu có
            if (!string.IsNullOrEmpty(proxy?.ProxyAddress) &&
                !string.IsNullOrEmpty(proxy.ProxyUsername) &&
                !string.IsNullOrEmpty(proxy.ProxyPassword))
            {
                var proxyHost = proxy.ProxyAddress.Split(":")[0];
                options.AddArgument($@"--host-resolver-rules=""MAP * 0.0.0.0 , EXCLUDE {proxyHost}""");


                options.AddArgument($"--gologing_proxy_server_username={proxy.ProxyUsername}");
                options.AddArgument($"--gologing_proxy_server_password={proxy.ProxyPassword}");
            }

            // Thiết lập tùy chọn load extension
            options.AddArgument("--load-extension=");

        }
        public async static Task GetDefaultSettingsAsync(this ChromeOptions options, string name, string profilePath)
        {
            // Thiết lập BinaryLocation
            options.BinaryLocation = Path.Combine(profilePath, "orbita-browser", "chrome.exe");

            // Các tùy chọn cần loại trừ
            var excludedArguments = new List<string>
            {
                "--allow-pre-commit-input",
                "--disable-background-networking",
                "--disable-backgrounding-occluded-windows",
                "--disable-client-side-phishing-detection",
                "--disable-default-apps",
                "--disable-hang-monitor",
                "--disable-popup-blocking",
                "--disable-prompt-on-repost",
                "--disable-sync",
                "--enable-automation",
                "--enable-logging",
                "--log-level=0",
                "--no-first-run",
                "--no-service-autorun",
                "--password-store=basic",
                "--remote-debugging-port=0",
                "--test-type=webdriver",
                "--use-mock-keychain"
            };
            options.AddExcludedArguments(excludedArguments);

            // Các tùy chọn cài đặt khác
            options.AddArgument("--lang=en-GB");
            /*options.AddArgument("--disable-encryption");
            options.AddArgument("--donut-pie=undefined");
            options.AddArgument("--webrtc-ip-handling-policy=default_public_interface_only");
            options.AddArgument("--font-masking-mode=2");*/

            // Thiết lập user-data-dir
            options.AddArgument("--user-data-dir=" + Path.Combine(profilePath, "browser", name));

            // Thiết lập tùy chọn load extension
            /* options.AddArgument("--load-extension=");*/
        }
    }
}
