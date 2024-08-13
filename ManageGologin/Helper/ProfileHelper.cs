using ManageGologin.Manager;
using ManageGologin.Models;
using ManageGologin.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Helper
{
    public static class ProfileHelper
    {
        public static List<Profiles> GetProfiles(IProxyManager proxyManager)
        {
            var browserDirectory = Path.Combine(Resources.ProfilePath, "browser");
            var profiles = new List<Profiles>();
            if (Directory.Exists(browserDirectory))
            {
                var directories = Directory.GetDirectories(browserDirectory);
                uint stt = 1;

                //Read proxy file
                var proxies = proxyManager.GetProxy();
                //Read profile
                foreach (var directory in directories)
                {
                    var profile = Path.GetFileName(directory);
                    profiles.Add(new Profiles
                    {
                        STT = stt,
                        ProfileName = profile,
                        DataPath = Path.Combine(directory),
                        Proxy = proxies[(int)stt - 1]
                    });
                    stt++;
                }
            }
            return profiles;
        }
        public static async Task SetPreferenceGeo(this Profiles profiles, bool? startWithProxy = false)
        {
            var preferencePath = Path.Combine(profiles.DataPath, "Default", "Preferences");

            // Đọc nội dung JSON từ tệp Preferences
            var preferenceContent = File.ReadAllText(preferencePath);
            var json = JObject.Parse(preferenceContent);

            Geolocation geolocation;
            var geolocationService = new GeolocationService();

            if (startWithProxy == true && profiles.Proxy != null)
            {
                geolocation = await geolocationService.GetGeolocation(profiles.Proxy);
            }
            else
            {
                geolocation = await geolocationService.GetMyIpGeolocation();
            }

            // Kiểm tra và cập nhật geolocation trong JSON
            if (json["gologin"] == null)
            {
                json["gologin"] = new JObject();
            }

            if (json["gologin"]["geoLocation"] != null)
            {
                json["gologin"]["geoLocation"]["latitude"] = geolocation.Latitude;
                json["gologin"]["geoLocation"]["longitude"] = geolocation.Longitude;
            }
            else
            {
                // Tạo mới nếu không tồn tại
                json["gologin"]["geoLocation"] = new JObject
                {
                    ["latitude"] = geolocation.Latitude,
                    ["longitude"] = geolocation.Longitude
                };
            }

            if (json["gologin"]["timezone"] != null)
            {
                json["gologin"]["timezone"]["id"] = geolocation.TimeZone;
            }
            else
            {
                // Tạo mới nếu không tồn tại
                json["gologin"]["timezone"] = new JObject
                {
                    ["id"] = geolocation.TimeZone
                };
            }

            // Lưu lại tệp JSON với nội dung đã chỉnh sửa
            File.WriteAllText(preferencePath, json.ToString());
        }

    }
}
