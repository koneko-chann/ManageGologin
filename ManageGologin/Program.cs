using Microsoft.Extensions.DependencyInjection;
using static ManageGologin.Configuration.HostBuilder;
namespace ManageGologin
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var host = CreateHostBuilder().Build();
            var ServiceProvider = host.Services;
            ApplicationConfiguration.Initialize();

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
    }
}