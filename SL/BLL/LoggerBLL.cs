using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.BLL
{
    internal class LoggerBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        public static void Store(string message, EventLevel severity)
        {
            DAL.Repositories.File.FileLoggerRepository.Current.Store(message, severity);

            switch (severity)
            {
                case EventLevel.Critical:
                    DAL.Repositories.File.FileLoggerRepository.Current.StoreCritical(message, severity);
                    break;
                case EventLevel.Warning:
                    DAL.Repositories.File.FileLoggerRepository.Current.StoreWarning(message, severity);
                    break;
                case EventLevel.Error:
                    DAL.Repositories.File.FileLoggerRepository.Current.Store(message, severity);
                    break;
            }
        }
    }
}
