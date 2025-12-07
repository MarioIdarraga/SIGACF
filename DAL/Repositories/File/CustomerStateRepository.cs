using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para CustomerState.
    /// Gestiona la carga de estados de cliente desde archivo JSON.
    /// Ideal para modo FILE cuando no se usa SQL Server.
    /// </summary>
    internal class CustomerStateRepository : ICustomerStateRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio asegurando la existencia del archivo JSON.
        /// </summary>
        public CustomerStateRepository()
        {
            try
            {
                _filePath = ConfigurationManager.AppSettings["PathFile"]
                            ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "CustomerStates.json");

                string folder = Path.GetDirectoryName(_filePath);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                // Si no existe, crear archivo vacío con lista JSON
                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "[]");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de CustomerState.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todos los estados de cliente desde el archivo JSON.
        /// </summary>
        private List<CustomerState> LoadStates()
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                    return new List<CustomerState>();

                string json = System.IO.File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<CustomerState>>(json) ?? new List<CustomerState>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los estados de cliente del archivo.", ex);
            }
        }

        /// <summary>
        /// Guarda toda la lista de estados en el archivo JSON.
        /// </summary>
        private void SaveStates(List<CustomerState> list)
        {
            try
            {
                string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los estados de cliente en el archivo.", ex);
            }
        }

        #endregion

        #region Methods GetAll & GetById

        /// <summary>
        /// Obtiene todos los estados de cliente almacenados en el archivo.
        /// </summary>
        public List<CustomerState> GetAll()
        {
            try
            {
                return LoadStates();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los estados de cliente.", ex);
            }
        }

        /// <summary>
        /// Obtiene un estado de cliente por su identificador.
        /// </summary>
        public CustomerState GetById(int id)
        {
            try
            {
                var list = LoadStates();
                return list.FirstOrDefault(s => s.IdCustomerState == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cliente por ID.", ex);
            }
        }

        #endregion
    }
}


