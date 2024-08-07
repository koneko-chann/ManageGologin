using ManageGologin.Models;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V125.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ManageGologin.Helper
{
    public static class OptionsDefault
    {
        public async static Task<ChromeOptions> GetDefaultSettingsAsync(this ChromeOptions options, string name, string profilePath, CustomProxy proxy)
        {
            string proxyAddress = proxy.ProxyAddress;
            string proxyUsername = proxy.ProxyUsername;
            string proxyPassword = proxy.ProxyPassword;
            if (proxyAddress != null && proxyUsername != null && proxyPassword != null && await proxy.IsProxyAliveAsync())
            {
                options.AddArgument($"--host-resolver-rules=\"MAP * 0.0.0.0 , EXCLUDE " + proxyAddress.Split(":")[0] + "\"");
                options.AddArgument($"--gologin_proxy_server_username={proxyUsername}");
                options.AddArgument($"--gologin_proxy_server_password={proxyPassword}");
            }
            options.AddArgument("--user-data-dir=" + Path.Combine(profilePath, "browser", name));
            options.BinaryLocation = Path.Combine(profilePath, "orbita-browser", "chrome.exe");
            options.AddExcludedArguments(new List<string> { "--allow-pre-commit-input", "--disable-background-networking", "--disable-backgrounding-occluded-windows", "--disable-client-side-phishing-detection", "--disable-default-apps", "--disable-hang-monitor", "--disable-popup-blocking", "--disable-prompt-on-repost", "--disable-sync", "--enable-automation", "--enable-logging", "--log-level=0", "--no-first-run", "--no-service-autorun", "--password-store=basic", "--remote-debugging-port=0", "--test-type=webdriver", "--use-mock-keychain" });
            options.AddArgument($"--lang=en-GB");
            options.AddArgument($"--disable-encryption");
            options.AddArgument($"--donut-pie=undefined");
            options.AddArgument($"--webrtc-ip-handling-policy=default_public_interface_only");
            options.AddArgument($"--font-masking-mode=2");
            options.AddArgument($"--load-extension=");
          
            return options;
        }
    }
}
