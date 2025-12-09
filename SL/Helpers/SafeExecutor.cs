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
    /// <summary>
    /// Proporciona métodos de ejecución segura que encapsulan el manejo de excepciones
    /// y registran errores de base de datos o fallas inesperadas.
    /// Permite unificar el control de errores en la capa de servicios.
    /// </summary>
    internal static class SafeExecutor
    {
        private const string GenericDbErrorMessage =
            "En este momento no es posible acceder a la base de datos. " +
            "Intente nuevamente más tarde o contacte al administrador del sistema.";

        private const string GenericUnexpectedErrorMessage =
            "Ocurrió un error inesperado. Intente nuevamente o contacte al administrador del sistema.";

        /// <summary>
        /// Ejecuta de manera segura una acción que no devuelve valor,
        /// encapsulando el manejo de excepciones y registrando errores.
        /// </summary>
        /// <param name="action">Acción a ejecutar.</param>
        /// <exception cref="BusinessException">
        /// Se lanza cuando ocurre un error de negocio, error de base de datos o error inesperado.
        /// </exception>
        public static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (BusinessException)
            {
                // Errores de negocio se relanzan tal cual.
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

        /// <summary>
        /// Ejecuta de manera segura una función que devuelve un valor,
        /// encapsulando el manejo de excepciones y registrando errores.
        /// </summary>
        /// <typeparam name="T">Tipo del valor devuelto por la función.</typeparam>
        /// <param name="func">Función a ejecutar.</param>
        /// <returns>Resultado devuelto por la función.</returns>
        /// <exception cref="BusinessException">
        /// Se lanza cuando ocurre un error de negocio, error de base de datos o error inesperado.
        /// </exception>
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