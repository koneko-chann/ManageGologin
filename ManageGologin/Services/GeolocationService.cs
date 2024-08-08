using ManageGologin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Services
{
    public class GeolocationService
    {
        private readonly string IPCheckerUrl = "https://api.ipgeolocation.io/"; // Thay bằng URL của dịch vụ API bạn sử dụng
        public readonly string APIKey ; // Thay bằng API key của bạn
        public GeolocationService()
        {
            APIKey = ConfigurationManager.AppSettings["APIKey"];
        }

        public async Task<(double Latitude, double Longitude, string TimeZone)> GetGeolocation(CustomProxy customProxy)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{IPCheckerUrl}ipgeo?apiKey={APIKey}&ip={customProxy.ProxyAddress}");
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(content);

                double latitude = json["latitude"].ToObject<double>();
                double longitude = json["longitude"].ToObject<double>();
                string timeZone = json["time_zone"]["name"].ToString();

                return (latitude, longitude, timeZone);
            }
        }
      
    }
    public static class CustomProxyExtensions
    {
        public static async Task SetGeolocationAsync(this CustomProxy customProxy, GeolocationService geolocationService)
        {
            var (latitude, longitude, timeZone) = await geolocationService.GetGeolocation(customProxy);
            customProxy.Latitude = latitude;
            customProxy.Longitude = longitude;
            customProxy.TimeZone = timeZone;
        }
    }
}
