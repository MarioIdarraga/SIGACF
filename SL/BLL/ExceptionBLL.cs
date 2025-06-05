using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.BLL
{
    internal class ExceptionBLL
    {



        private static void DALPolicy(Exception ex)
        {

           //Registra en bitacora
           //Propagar

            throw new Exception(String.Empty, ex);
        }
        private static void BLLPolicy (Exception ex)
        {
            
            if (ex.InnerException != null)
            {
                throw new Exception("Error accediendo a los datos", ex);
            }
            else
            {
                throw new Exception("Error en la capa de negocio", ex);
            }
        }   
        

        static void HandleException(Exception ex)
        {
            // Log the exception or handle it as required
            Console.WriteLine($"An error occurred: {ex.Message}");
            // You can also log to a file, database, etc.


        }


    }
}
