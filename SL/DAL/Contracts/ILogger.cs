using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Domain;

namespace SL.DAL.Contracts
{
    public interface ILogger
    {
        void Store(string message, EventLevel severity);
        List<Log> GetAll();
    }
}
