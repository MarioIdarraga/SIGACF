using System.Diagnostics.Tracing;
using SL.BLL;
using SL.Helpers;
using SL.Logging;

namespace SL.Service
{
    /// <summary>
    /// Servicio transversal encargado de registrar eventos en el sistema.
    /// Encapsula el acceso al LoggerBLL y determina el usuario que ejecuta la acción.
    /// </summary>
    public static class LoggerService
    {
        /// <summary>
        /// Registra un mensaje en el sistema de logging, indicando el nivel de severidad 
        /// y el usuario que realizó la acción. En caso de no estar autenticado,
        /// se registra como "System".
        /// </summary>
        /// <param name="message">Mensaje descriptivo del evento a registrar.</param>
        /// <param name="level">Nivel de severidad del log.</param>
        /// <param name="performedBy">Usuario que ejecutó la acción. Si es null, se toma de la sesión.</param>
        public static void Log(string message, EventLevel level = EventLevel.Informational, string performedBy = null)
        {
            string user = performedBy ?? Session.CurrentUser?.LoginName ?? "System";
            LoggerBLL.Store(message, level, user);
        }
    }
}