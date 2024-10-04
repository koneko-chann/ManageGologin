using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Helper
{
    public static class LoggerHelper
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public static void LogError(Exception ex, string message)
        {
            Logger.Error(ex, message);
        }

        public static void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public static void LogWarn(string message)
        {
            Logger.Warn(message);
        }
    }

}
