using System.Diagnostics.Tracing;
using SL.BLL;
using SL.Helpers;
using SL.Logging;

namespace SL
{
    public static class LoggerService
    {
        public static void Log(string message, EventLevel level = EventLevel.Informational, string performedBy = null)
        {
            string user = performedBy ?? Session.CurrentUser?.LoginName ?? "System";
            LoggerBLL.Store(message, level, user);
        }
    }
}