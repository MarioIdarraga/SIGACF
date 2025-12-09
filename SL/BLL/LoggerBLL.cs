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
    /// <summary>
    /// Componente de la capa de lógica de negocio responsable de gestionar 
    /// el registro de eventos del sistema. 
    /// Encapsula la lógica de selección del backend de logging (SQL o File)
    /// mediante el uso del <see cref="SLFactory"/>.
    /// </summary>
    internal class LoggerBLL
    {
        /// <summary>
        /// Almacena un registro en el repositorio de logs configurado.
        /// El repositorio puede ser SQL Server o archivo, dependiendo de la 
        /// configuración del backend en la aplicación.
        /// </summary>
        /// <param name="message">Mensaje del evento a registrar.</param>
        /// <param name="severity">Nivel de severidad del evento.</param>
        /// <param name="performedBy">Usuario que realizó la acción.</param>
        public static void Store(string message, EventLevel severity, string performedBy)
        {
            var loggerRepository = SLFactory.Current.GetLoggerRepository();
            loggerRepository.Store(message, severity, performedBy);
        }
    }
}
