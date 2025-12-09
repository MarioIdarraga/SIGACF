using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Domain.BusinessException
{
    /// <summary>
    /// Excepción que se produce cuando no se encuentra una clave
    /// dentro del archivo de idioma utilizado por el sistema SIGACF.
    /// </summary>
    public class NoSeEncontroLaPalabraException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la excepción
        /// <see cref="NoSeEncontroLaPalabraException"/> con un mensaje predeterminado.
        /// </summary>
        public NoSeEncontroLaPalabraException()
            : base("No se encontró la palabra en el archivo de idioma.")
        {
        }
    }
}
