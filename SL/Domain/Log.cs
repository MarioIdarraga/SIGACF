using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Domain
{
    public class Log
    {
        public string Message { get; set; }

        public Severity severity { get; set; }

        public enum Severity
        {
            Warning,
            Error,
            CriticalError,
            FatalError
        }
    }
}
