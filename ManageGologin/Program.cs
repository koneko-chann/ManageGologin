using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog.Targets;
using NLog;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
            AllocConsole();//EnableConsole 
            var config = new LoggingConfiguration();

            // Tạo một ColoredConsoleTarget
            var consoleTarget = new ColoredConsoleTarget("console")
            {
                Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=ToString}"
            };

            // Cấu hình màu sắc cho từng mức log
            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Fatal",
                ForegroundColor = ConsoleOutputColor.White,
                BackgroundColor = ConsoleOutputColor.Red
            });

            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Error",
                ForegroundColor = ConsoleOutputColor.Red
            });

            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Warn",
                ForegroundColor = ConsoleOutputColor.Yellow
            });

            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Info",
                ForegroundColor = ConsoleOutputColor.Green
            });

            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Debug",
                ForegroundColor = ConsoleOutputColor.Gray
            });

            consoleTarget.RowHighlightingRules.Add(new ConsoleRowHighlightingRule
            {
                Condition = "level == LogLevel.Trace",
                ForegroundColor = ConsoleOutputColor.DarkGray
            });

            // Thêm target vào cấu hình
            config.AddTarget(consoleTarget);

            // Thêm rule để ghi log từ mức Debug trở lên vào console
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);

            // Áp dụng cấu hình
            LogManager.Configuration = config;
            var host = CreateHostBuilder().Build();
            var ServiceProvider = host.Services;
            ApplicationConfiguration.Initialize();

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();
        
    }
}