using DAL.Contracts;
using DAL.Repositories.SqlServer;
using SL.DAL.Contracts;
using SL.DAL.Repositories.File;
using SL.DAL.Repositories.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Factory
{
    /// <summary>
    /// Fábrica de repositorios para la capa de servicios lógicos (SL).
    /// Resuelve las implementaciones concretas según la configuración del backend
    /// definido en AppSettings (SqlServer o File).
    /// Implementa el patrón Singleton.
    /// </summary>
    public sealed class SLFactory
    {
        /// <summary>
        /// Instancia única de la fábrica (patrón Singleton).
        /// </summary>
        private static readonly SLFactory _instance = new SLFactory();

        /// <summary>
        /// Nombre del backend configurado para la capa SL.
        /// </summary>
        private readonly string _backendSL;

        /// <summary>
        /// Acceso global a la instancia única de la fábrica.
        /// </summary>
        public static SLFactory Current => _instance;

        /// <summary>
        /// Constructor privado que inicializa el backendSL desde AppSettings.
        /// </summary>
        private SLFactory()
        {
            _backendSL = ConfigurationManager.AppSettings["backendSL"];
        }

        /// <summary>
        /// Obtiene el repositorio responsable de realizar operaciones de backup y restore.
        /// Solo disponible para backend SQL Server.
        /// </summary>
        /// <returns>Instancia de <see cref="IBackupRepository"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Si la cadena de conexión SqlConnectionString no está configurada.
        /// </exception>
        public IBackupRepository GetBackupRepository()
        {
            var cs = ConfigurationManager.ConnectionStrings["SqlConnectionString"]?.ConnectionString
                     ?? throw new InvalidOperationException("SqlConnectionString no configurada.");

            return new SL.DAL.Repositories.SqlServer.BackupRepository(cs);
        }

        /// <summary>
        /// Obtiene la implementación de logger adecuada según el backend configurado.
        /// </summary>
        /// <returns>Instancia de <see cref="ILogger"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// Si se especifica un backendSL no soportado.
        /// </exception>
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

        /// <summary>
        /// Obtiene la implementación del repositorio de permisos (patentes y familias).
        /// </summary>
        /// <returns>Instancia de <see cref="IPermissionRepository"/> según backend.</returns>
        /// <exception cref="NotSupportedException">
        /// Si el backend configurado no está soportado.
        /// </exception>
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

        /// <summary>
        /// Obtiene un repositorio para la gestión del DVV (Dígito Verificador Vertical).
        /// </summary>
        /// <returns>Instancia de <see cref="IVerificadorVerticalRepository"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// Si el backend configurado no está soportado.
        /// </exception>
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
