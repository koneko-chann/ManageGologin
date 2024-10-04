using ManageGologin.Helper;
using ManageGologin.Models;
using ManageGologin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Manager
{
    public class ProxyManager: IProxyManager
    {
        private List<CustomProxy> _proxy;
        private readonly GeolocationService geolocationService;
        public ProxyManager(GeolocationService geolocation)
        {
            geolocationService = geolocation;
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
            _proxy = proxies;
        }
        public List<CustomProxy> GetProxy()
        {
           return _proxy.ToList();
        }
        private void SaveProxy()
        {
            var proxyPath = Path.Combine(Resources.ProxyFile);
            var lines = new List<string>();
            var proxyCopy = _proxy.ToList(); // Create a copy of the collection

            foreach (var proxy in proxyCopy)
            {
                lines.Add(proxy.ToFullString());
            }

            bool fileWritten = false;
            int retryCount = 0;
            int maxRetries = 5;
            int delay = 1000; // 1 second

            while (!fileWritten && retryCount < maxRetries)
            {
                try
                {
                    File.WriteAllLines(proxyPath, lines);
                    fileWritten = true; 
                }
                catch (IOException ex)
                {
                    retryCount++;
                    Console.WriteLine($"Failed to write to the file. Attempt {retryCount} of {maxRetries}. Error: {ex.Message}");

                    if (retryCount >= maxRetries)
                    {
                        throw; 
                    }

                    Thread.Sleep(delay); // Wait before retrying
                }
            }
        }


      /// <summary>
      /// Update all proxy
      /// </summary>
      /// <param name="customProxies"></param>
      /// <returns></returns>
        public async Task UpdateAllProxy(List<CustomProxy> customProxies)
        {
            foreach (var customProxy in customProxies)
            {
                var geolocation = await geolocationService.GetGeolocation(customProxy);
                customProxy.Geolocation = geolocation;
            }
            _proxy = customProxies;
            SaveProxy();

        }
        public async Task UpdateProxy(CustomProxy customProxy)
        {
            var index = _proxy.ToList().FindIndex(x => x.ProxyAddress == customProxy.ProxyAddress);
      
            var geolocation =await geolocationService.GetGeolocation(customProxy);
            customProxy.Geolocation = geolocation;
            if (index != -1)
            {
                _proxy[index] = customProxy;
                SaveProxy();
            }
        }
     

    }
   
}
