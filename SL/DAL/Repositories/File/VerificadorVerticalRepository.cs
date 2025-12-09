using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SL.DAL.Contracts;
using SL.Factory;

namespace SL.DAL.Repositories.File
{
    /// <summary>
    /// Implementación File del repositorio de DVV (Dígito Verificador Vertical).
    /// Guarda los DVV en un archivo JSON ubicado en /DVV/DVV.json.
    /// </summary>
    internal class VerificadorVerticalRepository : IVerificadorVerticalRepository
    {
        private readonly string basePath;
        private readonly string dvvFile;

        /// <summary>
        /// Constructor. Prepara carpeta y archivo DVV.json.
        /// </summary>
        public VerificadorVerticalRepository()
        {
            basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DVV");
            dvvFile = Path.Combine(basePath, "DVV.json");

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            if (!System.IO.File.Exists(dvvFile))
                System.IO.File.WriteAllText(dvvFile, "{}"); // JSON vacío inicial
        }

        /// <summary>
        /// Lee el archivo DVV.json y lo deserializa.
        /// Si no existe, devuelve un diccionario vacío.
        /// </summary>
        private Dictionary<string, string> LoadDVV()
        {
            var json = System.IO.File.ReadAllText(dvvFile);

            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Guarda el diccionario DVV en el archivo DVV.json.
        /// </summary>
        private void SaveDVV(Dictionary<string, string> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            System.IO.File.WriteAllText(dvvFile, json);
        }

        /// <summary>
        /// Obtiene el DVV almacenado para una tabla.
        /// Devuelve string.Empty si no existe.
        /// </summary>
        public string GetDVV(string tabla)
        {
            var data = LoadDVV();

            return data.TryGetValue(tabla, out var dvv)
                ? dvv
                : string.Empty;
        }

        /// <summary>
        /// Establece o actualiza el DVV para una tabla.
        /// </summary>
        public void SetDVV(string tabla, string dvv)
        {
            var data = LoadDVV();

            data[tabla] = dvv;

            SaveDVV(data);
        }
    }
}