using System;
using System.Data;
using System.Data.SqlClient;
using SL.DAL.Contracts;
using SL.DAL.Tools;
using SL.Factory;

namespace SL.DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio encargado de almacenar y recuperar el 
    /// Dígito Verificador Vertical (DVV) de cada tabla del sistema.
    /// Utiliza la tabla [TableChecksums] para mantener el valor del DVV.
    /// </summary>
    public class VerificadorVerticalRepository : IVerificadorVerticalRepository
    {
        #region Statements

        /// <summary>
        /// Sentencia SQL para obtener el DVV actual asociado a una tabla.
        /// </summary>
        private string SelectStatement
        {
            get => "SELECT Checksum FROM TableChecksums WHERE TableName = @TableName";
        }

        /// <summary>
        /// Sentencia SQL para insertar o actualizar (MERGE) un DVV en la tabla de checksums.
        /// </summary>
        private string UpsertStatement
        {
            get => @"
                MERGE TableChecksums AS target
                USING (SELECT @TableName AS TableName, @Checksum AS Checksum) AS source
                ON target.TableName = source.TableName
                WHEN MATCHED THEN
                    UPDATE SET Checksum = source.Checksum
                WHEN NOT MATCHED THEN
                    INSERT (TableName, Checksum) VALUES (source.TableName, source.Checksum);";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene el valor del DVV almacenado para una tabla específica.
        /// </summary>
        /// <param name="tabla">Nombre de la tabla para la cual se desea obtener el DVV.</param>
        /// <returns>
        /// DVV almacenado como cadena. Si no existe registro, devuelve string.Empty.
        /// </returns>
        public string GetDVV(string tabla)
        {
            using (var reader = SqlHelper.ExecuteReader(
                SelectStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@TableName", tabla)
                }))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Inserta o actualiza el DVV asociado a una tabla.
        /// Utiliza operación MERGE para evitar duplicados.
        /// </summary>
        /// <param name="tabla">Nombre de la tabla cuyo DVV será actualizado.</param>
        /// <param name="dvv">Valor del DVV calculado.</param>
        public void SetDVV(string tabla, string dvv)
        {
            SqlHelper.ExecuteNonQuery(
                UpsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@TableName", tabla),
                    new SqlParameter("@Checksum", dvv)
                });
        }

        #endregion
    }
}
