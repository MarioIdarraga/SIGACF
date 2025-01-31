using System.Diagnostics.Tracing;

namespace SL
{
    public class LoggerService
    {
        public static void Store(string message, EventLevel severity)
        {
            BLL.LoggerBLL.Store(message, severity);
        }
        public static void StoreCritical(string message, EventLevel severity)
        {
            BLL.LoggerBLL.Store(message, severity);
        }

        public static void StoreWarning(string log, EventLevel severity)
        {
            BLL.LoggerBLL.Store(log, severity);
        }
    }
}