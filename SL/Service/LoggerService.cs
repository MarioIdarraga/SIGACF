using System.Diagnostics.Tracing;
using SL.BLL;
using SL.Logging;

namespace SL
{
    public static class LoggerService
    {
        public static void Log(string message, EventLevel level = EventLevel.Informational)
        {
            LoggerBLL.Store(message, level);
        }
    }
}