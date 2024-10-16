using ManageGologin.Executor.InterfaceExecutor;
using ManageGologin.Models;
using OpenQA.Selenium.Chrome;

namespace ManageGologin.Executor
{
    public class PlayGame : IExecutor
    {
        private ChromeDriver _chromeDriver;
        private Profiles _profiles;
        public PlayGame(ChromeDriver chromeDriver,Profiles profiles)
        {
            _chromeDriver = chromeDriver;
            _profiles = profiles;
        }
        public async Task Execute()
        {
            Console.WriteLine("Play game");
        }
    }
 
}
