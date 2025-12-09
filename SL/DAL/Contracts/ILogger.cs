using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.Domain;

namespace SL.DAL.Contracts
{
    /// <summary>
    /// Define las operaciones básicas para el almacenamiento y recuperación
    /// de registros de log dentro del sistema.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Almacena un mensaje de log con su nivel de severidad y el usuario que realizó la acción.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del evento.</param>
        /// <param name="severity">Nivel de severidad del evento.</param>
        /// <param name="performedBy">Usuario que ejecutó la acción registrada.</param>
        void Store(string message, EventLevel severity, string performedBy);

        /// <summary>
        /// Obtiene todos los registros de log almacenados.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Log"/>.</returns>
        List<Log> GetAll();
    }
}
