using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL.Contracts;
using SL.Factory;

namespace SL.Logging
{
    internal class LoggerBLL
    {
        public static void Store(string message, EventLevel severity, string performedBy)
        {
            var loggerRepository = SLFactory.Current.GetLoggerRepository();

            loggerRepository.Store(message, severity, performedBy);

            switch (severity)
            {
                case EventLevel.Critical:
                case EventLevel.Warning:
                case EventLevel.Error:
                    loggerRepository.Store(message, severity, performedBy);
                    break;
            }
        }
    }
}
