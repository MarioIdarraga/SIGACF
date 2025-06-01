using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.BLL;

namespace SL.Service.Extension
{
    public static class StringExtension
    {
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
