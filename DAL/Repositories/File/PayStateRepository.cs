using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para la entidad PayState.
    /// Permite obtener los estados de pago desde un archivo JSON.
    /// </summary>
    internal class PayStateRepository : IPayStateRepository
    {
        private readonly string path;
        private readonly JavaScriptSerializer json;

        /// <summary>
        /// Inicializa el repositorio configurando la ruta del archivo JSON.
        /// </summary>
        public PayStateRepository()
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "PayStates.json");
            json = new JavaScriptSerializer();

            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            if (!System.IO.File.Exists(path))
                System.IO.File.WriteAllText(path, "[]"); // Archivo inicial vacío
        }

        /// <summary>
        /// Obtiene todos los estados de pago almacenados en el archivo JSON.
        /// </summary>
        /// <returns>Lista de estados de pago.</returns>
        public IEnumerable<PayState> GetAll()
        {
            try
            {
                var content = System.IO.File.ReadAllText(path);
                var list = json.Deserialize<List<PayState>>(content) ?? new List<PayState>();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados de pago desde archivo.", ex);
            }
        }
    }
}
