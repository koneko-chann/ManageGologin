using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManageGologin.Models
{
    public class CustomProxy
    {
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }

        public CustomProxy() { }

        public CustomProxy(string proxy)
        {
            // Proxy must follow this format: {address}:{port}:{username}:{password}
            var proxyParts = proxy.Split(':');
            if (proxyParts.Length != 4)
            {
                throw new ArgumentException("Proxy must be in the format {address}:{port}:{username}:{password}");
            }

            ProxyAddress = proxyParts[0];
            if (!int.TryParse(proxyParts[1], out int port))
            {
                throw new ArgumentException("Port must be a valid integer");
            }
            ProxyPort = port;
            ProxyUsername = proxyParts[2];
            ProxyPassword = proxyParts[3];
        }

        public async Task<bool> IsProxyAliveAsync()
        {
            var proxy = new WebProxy(ProxyAddress, ProxyPort);

            if (!string.IsNullOrEmpty(ProxyUsername) && !string.IsNullOrEmpty(ProxyPassword))
            {
                proxy.Credentials = new NetworkCredential(ProxyUsername, ProxyPassword);
            }

            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true,
            };

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.Timeout = TimeSpan.FromSeconds(5); // Set timeout to 5 seconds
                try
                {
                    var response = await httpClient.GetAsync("https://www.microsoft.com");
                    return response.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
