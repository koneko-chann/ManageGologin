using ManageGologin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Helper
{
    public static class ProfileHelper
    {
        public static List<Profiles> GetProfiles()
        {
            var browserDirectory = Path.Combine(Resources.ProfilePath, "browser");
            var profiles = new List<Profiles>();
            if (Directory.Exists(browserDirectory))
            {
                var directories = Directory.GetDirectories(browserDirectory);
                uint stt = 1;

                //Read proxy file
                var proxyPath = Path.Combine(Resources.ProxyFile);
                var proxies = new List<CustomProxy>();
                if (File.Exists(proxyPath))
                {
                    var lines = File.ReadAllLines(proxyPath);
                    foreach (var line in lines)
                    {
                        var proxy = new CustomProxy(line);
                        proxies.Add(proxy);
                    }
                }
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
    }
}
