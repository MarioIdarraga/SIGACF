using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace SL.DAL.Tools
{
    /// <summary>
    /// Helper utilitario para ejecutar comandos SQL contra la base de datos del sistema.
    /// Proporciona métodos para ejecutar sentencias de acción (INSERT, UPDATE, DELETE),
    /// consultas escalares y operaciones que devuelven un SqlDataReader.
    /// Utiliza la cadena de conexión configurada en SqlConnectionString.
    /// </summary>
    internal static class SqlHelper
    {
        /// <summary>
        /// Cadena de conexión utilizada para todas las operaciones SQL.
        /// </summary>
        private static readonly string conString;

        /// <summary>
        /// Constructor estático que carga la cadena de conexión desde el archivo de configuración.
        /// </summary>
        static SqlHelper()
        {
            conString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Ejecuta un comando SQL que no retorna valores (INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="commandText">Texto SQL o nombre del procedimiento almacenado.</param>
        /// <param name="commandType">Tipo de comando: Text, StoredProcedure o TableDirect.</param>
        /// <param name="parameters">Parámetros SQL opcionales.</param>
        /// <returns>Cantidad de filas afectadas.</returns>
        public static int ExecuteNonQuery(
            string commandText,
            CommandType commandType,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL que retorna un único valor (primer fila, primera columna).
        /// Ideal para COUNT, SUM, MAX, SELECT simples o verificaciones.
        /// </summary>
        /// <param name="commandText">Texto SQL o nombre del procedimiento almacenado.</param>
        /// <param name="commandType">Tipo de comando SQL.</param>
        /// <param name="parameters">Parámetros SQL opcionales.</param>
        /// <returns>Objeto con el valor retornado o null si no hay resultados.</returns>
        public static object ExecuteScalar(
            string commandText,
            CommandType commandType,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL que devuelve un conjunto de registros mediante SqlDataReader.
        /// La conexión se cierra automáticamente cuando el reader es cerrado.
        /// </summary>
        /// <param name="commandText">Texto SQL o nombre del procedimiento almacenado.</param>
        /// <param name="commandType">Tipo de comando SQL.</param>
        /// <param name="parameters">Parámetros SQL opcionales.</param>
        /// <returns>
        /// Un <see cref="SqlDataReader"/> con CommandBehavior.CloseConnection,
        /// permitiendo que la conexión se cierre automáticamente cuando se finaliza la lectura.
        /// </returns>
        public static SqlDataReader ExecuteReader(
            string commandText,
            CommandType commandType,
            params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(conString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }
    }
}
