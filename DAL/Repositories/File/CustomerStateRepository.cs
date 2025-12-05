using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para gestionar los estados de cliente (CustomerState).
    /// Utiliza almacenamiento en archivo de texto delimitado por '|'.
    /// </summary>
    internal class CustomerStateRepository : IGenericRepository<CustomerState>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio asegurando la existencia del archivo.
        /// </summary>
        public CustomerStateRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "CustomerStates.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de estados de cliente.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todos los estados desde el archivo.
        /// </summary>
        private List<CustomerState> LoadAll()
        {
            try
            {
                var list = new List<CustomerState>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    list.Add(new CustomerState
                    {
                        IdCustomerState = int.Parse(parts[0]),
                        Description = parts[1]
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los estados de cliente.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los estados en el archivo.
        /// </summary>
        private void SaveAll(List<CustomerState> list)
        {
            try
            {
                var lines = list.Select(x => $"{x.IdCustomerState}|{x.Description}");
                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los estados de cliente.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta un nuevo estado de cliente.
        /// </summary>
        public void Insert(CustomerState Object)
        {
            try
            {
                var states = LoadAll();

                if (states.Any(x => x.IdCustomerState == Object.IdCustomerState))
                    throw new Exception("El estado de cliente ya existe.");

                states.Add(Object);
                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el estado de cliente.", ex);
            }
        }

        /// <summary>
        /// Actualiza un estado de cliente existente.
        /// </summary>
        public void Update(Guid Id, CustomerState Object)
        {
            try
            {
                var states = LoadAll();

                var existing = states.FirstOrDefault(x => x.IdCustomerState == Object.IdCustomerState);

                if (existing == null)
                    throw new Exception("El estado de cliente no existe.");

                states.Remove(existing);
                states.Add(Object);
                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de cliente.", ex);
            }
        }

        /// <summary>
        /// Elimina un estado de cliente.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);

                var states = LoadAll();
                var item = states.FirstOrDefault(x => x.IdCustomerState == numericId);

                if (item == null)
                    throw new Exception("El estado de cliente no existe.");

                states.Remove(item);
                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el estado de cliente.", ex);
            }
        }

        /// <summary>
        /// Obtiene un estado de cliente por su identificador.
        /// </summary>
        public CustomerState GetOne(Guid Id)
        {
            try
            {
                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);

                var states = LoadAll();
                return states.FirstOrDefault(x => x.IdCustomerState == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cliente.", ex);
            }
        }

        #endregion


        #region GetAll

        /// <summary>
        /// Obtiene todos los estados de cliente.
        /// </summary>
        public IEnumerable<CustomerState> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los estados de cliente.", ex);
            }
        }

        /// <summary>
        /// No aplican filtros en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<CustomerState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplican filtros en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<CustomerState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplican filtros en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<CustomerState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplican filtros en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<CustomerState> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}

