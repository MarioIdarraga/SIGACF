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
        public static void Store(string message, EventLevel severity)
        {
            var loggerRepository = SLFactory.Current.GetLoggerRepository();

            loggerRepository.Store(message, severity);

            switch (severity)
            {
                case EventLevel.Critical:
                    loggerRepository.Store(message, severity); // o StoreCritical
                    break;
                case EventLevel.Warning:
                    loggerRepository.Store(message, severity);
                    break;
                case EventLevel.Error:
                    loggerRepository.Store(message, severity);
                    break;
            }
        }
    }
}
