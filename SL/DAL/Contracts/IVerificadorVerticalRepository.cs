using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SL.Factory
{
    /// <summary>
    /// Define las operaciones necesarias para la gestión del
    /// Dígito Verificador Vertical (DVV) asociado a las tablas del sistema.
    /// </summary>
    public interface IVerificadorVerticalRepository
    {
        /// <summary>
        /// Actualiza el valor del DVV para una tabla específica.
        /// </summary>
        /// <param name="tabla">Nombre de la tabla.</param>
        /// <param name="dvv">Valor del DVV calculado.</param>
        void SetDVV(string tabla, string dvv);

        /// <summary>
        /// Obtiene el valor actual del DVV asociado a una tabla.
        /// </summary>
        /// <param name="tabla">Nombre de la tabla.</param>
        /// <returns>Valor del DVV almacenado.</returns>
        string GetDVV(string tabla);
    }
}