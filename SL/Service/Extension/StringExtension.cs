using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.BLL;

namespace SL.Service.Extension
{
    /// <summary>
    /// Métodos de extensión para la clase <see cref="string"/> que permiten obtener
    /// traducciones desde el motor de idiomas del sistema.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Traduce la clave actual utilizando el repositorio de lenguaje configurado.
        /// Se utiliza en UI mediante: "Clave".Traductor().
        /// </summary>
        /// <param name="key">Clave que se desea traducir.</param>
        /// <returns>Texto traducido correspondiente a la clave.</returns>
        /// <exception cref="Domain.BusinessException.NoSeEncontroLaPalabraException">
        /// Se lanza cuando la clave no está definida en el archivo o tabla de idioma.
        /// </exception>
        public static string Traductor(this string key)
        {
            try
            {
                return LanguageBLL.Current.Traductor(key);
            }
            catch (Domain.BusinessException.NoSeEncontroLaPalabraException ex)
            {
                throw;
            }
        }
    }
}
