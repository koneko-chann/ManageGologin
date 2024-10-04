using ManageGologin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Manager
{
    public interface IProxyManager
    {
        List<CustomProxy> GetProxy();
        Task UpdateProxy(CustomProxy customProxy);
        Task UpdateAllProxy(List<CustomProxy> customProxies);
    }
}
