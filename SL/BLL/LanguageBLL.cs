using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL;
using SL.Domain.BusinessException;

namespace SL.BLL
{
    public sealed class LanguageBLL
    {
        #region Singleton
        private readonly static LanguageBLL _instance = new LanguageBLL();
            public static LanguageBLL Current
            {
                get
                {
                        return _instance;
                }
            }
        private LanguageBLL()
        {
                    // Implement here the initialization of your singleton
        }
                
        #endregion

        public string Traductor(string key)
        {
            try
            {
                return LanguageDAL.Current.Traductor(key);
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

