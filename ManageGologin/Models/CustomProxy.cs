using System.Net;

namespace ManageGologin.Models
{
    public class CustomProxy
    {
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
        public Geolocation? Geolocation { get; set; }
        public string ProxyStatus { get; set; } = "dead";


        public CustomProxy() { }

        public CustomProxy(string proxy)
        {
            // Proxy must follow this format: {address}:{port}:{username}:{password}|{status}|{latitude}|{longitude}|{timezone}
            string[] statusSplit;
            if (proxy.Contains('|'))
            {
                statusSplit = proxy.Split('|');
            }
            else
            {
                statusSplit = [proxy, "dead", "0", "0", "Asia/Bangkok"];
            }

            var proxyParts = statusSplit[0].Split(':');
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
            ProxyStatus = statusSplit.Length > 1 ? statusSplit[1] : "dead";

            Geolocation = new Geolocation()
            {
                Latitude = statusSplit.Length > 2 && double.TryParse(statusSplit[2], out double lat) ? lat : 0,
                Longitude = statusSplit.Length > 3 && double.TryParse(statusSplit[3], out double lon) ? lon : 0,
                TimeZone = statusSplit.Length > 4 ? statusSplit[4] : "Asia/Bangkok"
            };
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

        public static bool CheckFormat(string proxy)
        {
            var parts = proxy.Split('|');
            if (parts.Length < 1)
            {
                return false;
            }

            var proxyParts = parts[0].Split(':');
            if (proxyParts.Length != 4)
            {
                return false;
            }

            if (!int.TryParse(proxyParts[1], out int port))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"{ProxyAddress}:{ProxyPort}";
        }
        public string ToFullString()
        {
            return $"{ProxyAddress}:{ProxyPort}:{ProxyUsername}:{ProxyPassword}|{ProxyStatus}|{Geolocation?.Latitude}|{Geolocation?.Longitude}|{Geolocation?.TimeZone}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is CustomProxy proxy)
            {
                return ProxyAddress == proxy.ProxyAddress && ProxyPort == proxy.ProxyPort;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ProxyAddress.GetHashCode() ^ ProxyPort.GetHashCode();
        }
    }
}
