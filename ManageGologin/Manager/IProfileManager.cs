using ManageGologin.Models;
using ManageGologin.Pagination;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ManageGologin.ManagePhysicalPath
{
    public interface IProfileManager
    {
        Task<IWebDriver> OpenProfile(Profiles profiles, bool? startWithProxy = false);
        void CloseProfile(ref ChromeDriver driver);
        List<Profiles> GetProfiles(PagingParameters pagingParameters);
        List<Profiles> GetProfiles();
        void SetProfiles(List<Profiles> profiles);
        Task<IWebDriver> OpenProfileWithScript(Profiles profiles, Dictionary<string, string> webAndJs, bool? startWithProxy = false);
    }
}
