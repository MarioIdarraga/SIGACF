using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BusinessException;

namespace SL.Helpers
{
    internal static class SafeExecutor
    {
        private const string GenericDbErrorMessage =
            "En este momento no es posible acceder a la base de datos. " +
            "Intente nuevamente más tarde o contacte al administrador del sistema.";

        private const string GenericUnexpectedErrorMessage =
            "Ocurrió un error inesperado. Intente nuevamente o contacte al administrador del sistema.";

        // Para métodos que NO devuelven nada (void)
        public static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (BusinessException)
            {
                // Ya es un error "de negocio", lo dejamos pasar hacia la UI
                throw;
            }
            catch (SqlException ex)
            {
                // Log técnico detallado
                LoggerService.Log(
                    $"Error de base de datos: {ex.Message}",
                    EventLevel.Error);

                // Mensaje genérico para el usuario
                throw new BusinessException(GenericDbErrorMessage, ex);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado: {ex}",
                    EventLevel.Critical);

                throw new BusinessException(GenericUnexpectedErrorMessage, ex);
            }
        }

        // Para métodos que devuelven algo (T)
        public static T Run<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                LoggerService.Log(
                    $"Error de base de datos: {ex.Message}",
                    EventLevel.Error);

                throw new BusinessException(GenericDbErrorMessage, ex);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado: {ex}",
                    EventLevel.Critical);

                throw new BusinessException(GenericUnexpectedErrorMessage, ex);
            }
        }
    }
}