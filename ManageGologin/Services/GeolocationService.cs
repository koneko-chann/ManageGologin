using ManageGologin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Services
{
    public class GeolocationService
    {
        private readonly string IPCheckerUrl = "https://api.ipgeolocation.io/"; // Thay bằng URL của dịch vụ API bạn sử dụng
        public readonly string APIKey; // Thay bằng API key của bạn
        public GeolocationService()
        {
            APIKey = ConfigurationManager.AppSettings["APIKey"];
        }

        public async Task<Geolocation> GetGeolocation(CustomProxy customProxy)
        {
            string publicIp;
            try
            {
                publicIp = await GetPublicIp(customProxy);

            }
            catch
            {
                publicIp = customProxy.ProxyAddress;
            }
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{IPCheckerUrl}ipgeo?apiKey={APIKey}&ip={publicIp}");
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(content);

                double latitude = json["latitude"].ToObject<double>();
                double longitude = json["longitude"].ToObject<double>();
                string timeZone = json["time_zone"]["name"].ToString();

                return new Geolocation()
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    TimeZone = timeZone
                };
            }
        }
        public async Task<Geolocation> GetMyIpGeolocation()
        {
            string ip;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://api.ipify.org");
                ip = await response.Content.ReadAsStringAsync();
            }
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{IPCheckerUrl}ipgeo?apiKey={APIKey}&ip={ip}");
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(content);

                double latitude = json["latitude"].ToObject<double>();
                double longitude = json["longitude"].ToObject<double>();
                string timeZone = json["time_zone"]["name"].ToString();

                return new Geolocation()
                {
                    Latitude = latitude,
                    Longitude = longitude,
                    TimeZone = timeZone
                };
            }
        }
        public async Task<string> GetPublicIp(CustomProxy customProxy)
        {
            // Lấy IP public của proxy
            // Sử dụng proxy
            var proxy = new WebProxy(customProxy.ProxyAddress, customProxy.ProxyPort);
            if (!string.IsNullOrEmpty(customProxy.ProxyUsername) && !string.IsNullOrEmpty(customProxy.ProxyPassword))
            {
                proxy.Credentials = new NetworkCredential(customProxy.ProxyUsername, customProxy.ProxyPassword);
            }
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true,
            };
            using (var httpClient = new HttpClient(httpClientHandler))
            {
                var response = await httpClient.GetAsync("https://api.ipify.org");
                return await response.Content.ReadAsStringAsync();
            }
            
        }

    }
    public static class CustomProxyExtensions
    {
        public static async Task SetGeolocationAsync(this CustomProxy customProxy, GeolocationService geolocationService)
        {
            var geolocation = await geolocationService.GetGeolocation(customProxy);
            customProxy.Geolocation = geolocation;
        }
    }

}
