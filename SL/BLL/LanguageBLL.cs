using System;
using SL.DAL.Repositories.File;
using SL.Domain.BusinessException;

namespace SL.BLL
{
    /// <summary>
    /// Proporciona funcionalidades de negocio relacionadas con la traducción
    /// de claves de idioma dentro del sistema SIGACF.
    /// Implementa un patrón Singleton para acceso global controlado.
    /// </summary>
    public sealed class LanguageBLL
    {
        #region Singleton

        /// <summary>
        /// Instancia única de la clase <see cref="LanguageBLL"/>.
        /// </summary>
        private readonly static LanguageBLL _instance = new LanguageBLL();

        /// <summary>
        /// Obtiene la instancia única de <see cref="LanguageBLL"/>.
        /// </summary>
        public static LanguageBLL Current => _instance;

        /// <summary>
        /// Constructor privado utilizado para implementar el patrón Singleton.
        /// </summary>
        private LanguageBLL()
        {
            // Init
        }

        #endregion

        /// <summary>
        /// Traduce una clave de idioma utilizando el repositorio configurado.
        /// </summary>
        /// <param name="key">Clave asociada a un texto a traducir.</param>
        /// <returns>Cadena traducida correspondiente a la clave ingresada.</returns>
        /// <exception cref="NoSeEncontroLaPalabraException">
        /// Se lanza cuando la palabra no existe en el archivo de idioma.
        /// </exception>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error inesperado durante el proceso de traducción.
        /// </exception>
        public string Traductor(string key)
        {
            try
            {
                var repo = new LanguageRepository();
                return repo.Traductor(key);
            }
            catch (NoSeEncontroLaPalabraException)
            {
                // Se relanza la excepción específica sin perder contexto
                throw;
            }
            catch (Exception ex)
            {
                // Se encapsula la excepción inesperada para claridad en la capa superior
                throw new Exception("Error en la traducción", ex);
            }
        }
    }
}


