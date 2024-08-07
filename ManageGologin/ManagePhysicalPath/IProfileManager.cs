using ManageGologin.Models;
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
        List<Profiles> GetProfiles();
       Task<IWebDriver> OpenProfile(string name);
        Task CloseProfile(ChromeDriver driver);
    }
}
