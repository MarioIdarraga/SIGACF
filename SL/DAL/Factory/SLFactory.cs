using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using SL.DAL.Contracts;
using SL.DAL.Repositories.File;
using SL.DAL.Repositories.SqlServer;

namespace SL.Factory
{
    public sealed class SLFactory
    {
        private static readonly SLFactory _instance = new SLFactory();

        private readonly string _backendSL;

        public static SLFactory Current => _instance;

        private SLFactory()
        {
            _backendSL = ConfigurationManager.AppSettings["backendSL"];
        }

        public ILogger GetLoggerRepository()
        {
            if (_backendSL == "SqlServer")
            {
                return new DAL.Repositories.SqlServer.LoggerRepository();
            }

            if (_backendSL == "File")
            {
                return new DAL.Repositories.File.LoggerRepository();
            }

            throw new NotSupportedException($"El backendSL '{_backendSL}' no es soportado.");
        }

        public IPermissionRepository GetPermissionRepository()
        {
            if (_backendSL == "SqlServer")
            {
                return new SL.DAL.Repositories.SqlServer.PermissionRepository();
            }

            if (_backendSL == "File")
            {
                return new SL.DAL.Repositories.File.PermissionRepository();
            }

            throw new NotSupportedException($"El backendSL '{_backendSL}' no es soportado.");
        }

        public IVerificadorVerticalRepository GetVerificadorVerticalRepository()
        {
            if (_backendSL == "SqlServer")
            {
                return new SL.DAL.Repositories.SqlServer.VerificadorVerticalRepository();
            }

            if (_backendSL == "File")
            {
                return new SL.DAL.Repositories.File.VerificadorVerticalRepository();
            }

            throw new NotSupportedException($"El backendSL '{_backendSL}' no es soportado.");
        }
    }
}