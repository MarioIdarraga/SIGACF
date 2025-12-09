using SL.Domain.BusinessException;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SL.DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server para traducciones.
    /// Obtiene textos desde la tabla LanguageText usando clave + cultura actual.
    /// Equivalente funcional al repositorio basado en archivos.
    /// </summary>
    public sealed class LanguageRepository
    {
        #region Singleton

        private readonly static LanguageRepository _instance = new LanguageRepository();

        /// <summary>
        /// Instancia única del repositorio (patrón Singleton).
        /// </summary>
        public static LanguageRepository Current => _instance;

        /// <summary>
        /// Constructor privado para evitar instancias externas.
        /// </summary>
        private LanguageRepository()
        {
        }

        #endregion

        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        /// <summary>
        /// Traduce una clave según el idioma actual del sistema (CultureInfo).
        /// Busca en la tabla LanguageText (LangCode, Key, Value).
        /// </summary>
        /// <param name="key">Clave a traducir.</param>
        /// <returns>Cadena traducida.</returns>
        /// <exception cref="NoSeEncontroLaPalabraException">
        /// Si no existe la clave para el idioma actual.
        /// </exception>
        public string Traductor(string key)
        {
            string lang = Thread.CurrentThread.CurrentUICulture.Name; // ej: es-AR

            const string sql = @"
                SELECT [Value]
                FROM LanguageText
                WHERE LangCode = @lang AND [Key] = @key";

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@lang", lang);
                cmd.Parameters.AddWithValue("@key", key);

                cn.Open();
                var result = cmd.ExecuteScalar();

                if (result != null)
                    return result.ToString();
            }

            throw new NoSeEncontroLaPalabraException();
        }

        /// <summary>
        /// Obtiene todos los idiomas disponibles según los registros
        /// presentes en la tabla LanguageText.
        /// </summary>
        /// <returns>Lista de códigos de idioma (es-AR, en-US, etc).</returns>
        public List<string> GetAllLanguages()
        {
            var list = new List<string>();

            const string sql = @"
                SELECT DISTINCT LangCode
                FROM LanguageText
                ORDER BY LangCode";

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(rd.GetString(0));
                }
            }

            return list;
        }
    }
}