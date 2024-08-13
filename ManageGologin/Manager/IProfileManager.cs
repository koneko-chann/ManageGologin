using ManageGologin.Models;
using ManageGologin.Pagination;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.ManagePhysicalPath
{
    public interface IProfileManager
    {
        Task<IWebDriver> OpenProfile(Profiles profiles,bool? startWithProxy=false);
        Task CloseProfile(ChromeDriver driver);
        List<Profiles> GetProfiles(PagingParameters pagingParameters);
        List<Profiles> GetProfiles();
        void SetProfiles(List<Profiles> profiles);
    }
}
