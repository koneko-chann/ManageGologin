using ManageGologin.Models;

namespace ManageGologin.Manager
{
    public interface IProxyManager
    {
        List<CustomProxy> GetProxy();
        Task UpdateProxy(CustomProxy customProxy);
        Task UpdateAllProxy(List<CustomProxy> customProxies);
    }
}
