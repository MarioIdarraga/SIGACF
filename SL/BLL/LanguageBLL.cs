using System;
using SL.DAL.Repositories.File;
using SL.Domain.BusinessException;

namespace SL.BLL
{
    public sealed class LanguageBLL
    {
        #region Singleton
        private readonly static LanguageBLL _instance = new LanguageBLL();
        public static LanguageBLL Current => _instance;

        private LanguageBLL()
        {
            // Init
        }
        #endregion

        public string Traductor(string key)
        {
            try
            {
                var repo = new LanguageRepository();
                return repo.Traductor(key);
            }
            catch (NoSeEncontroLaPalabraException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la traducción", ex);
            }
        }
    }
}


