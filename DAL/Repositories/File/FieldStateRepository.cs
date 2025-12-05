using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para estados de cancha (FieldState).
    /// Maneja operaciones CRUD utilizando almacenamiento en archivo de texto delimitado por '|'.
    /// </summary>
    internal class FieldStateRepository : IGenericRepository<FieldState>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public FieldStateRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "FieldStates.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de estados de cancha.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todos los estados desde el archivo.
        /// </summary>
        private List<FieldState> LoadAll()
        {
            try
            {
                var list = new List<FieldState>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    list.Add(new FieldState
                    {
                        IdFieldState = int.Parse(parts[0]),
                        FieldStateDescription = parts[1]
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los estados de cancha.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los estados en el archivo.
        /// </summary>
        private void SaveAll(List<FieldState> list)
        {
            try
            {
                var lines = list.Select(x => $"{x.IdFieldState}|{x.FieldStateDescription}");
                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los estados de cancha.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta un nuevo estado de cancha.
        /// </summary>
        public void Insert(FieldState Object)
        {
            try
            {
                var states = LoadAll();

                if (states.Any(x => x.IdFieldState == Object.IdFieldState))
                    throw new Exception("El estado de cancha ya existe.");

                states.Add(Object);
                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el estado de cancha.", ex);
            }
        }

        /// <summary>
        /// Actualiza un estado de cancha existente.
        /// </summary>
        public void Update(Guid Id, FieldState Object)
        {
            try
            {
                var states = LoadAll();

                var existing = states.FirstOrDefault(x => x.IdFieldState == Object.IdFieldState);

                if (existing == null)
                    throw new Exception("El estado de cancha no existe.");

                states.Remove(existing);
                states.Add(Object);

                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de cancha.", ex);
            }
        }

        /// <summary>
        /// Elimina un estado de cancha según un Guid que se convierte a int.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);

                var states = LoadAll();
                var item = states.FirstOrDefault(x => x.IdFieldState == numericId);

                if (item == null)
                    throw new Exception("El estado de cancha no existe.");

                states.Remove(item);
                SaveAll(states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el estado de cancha.", ex);
            }
        }

        /// <summary>
        /// Obtiene un estado de cancha según ID convertido desde Guid a int.
        /// </summary>
        public FieldState GetOne(Guid Id)
        {
            try
            {
                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);

                var states = LoadAll();
                return states.FirstOrDefault(x => x.IdFieldState == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cancha.", ex);
            }
        }

        #endregion


        #region GetAll

        /// <summary>
        /// Obtiene todos los estados de cancha.
        /// </summary>
        public IEnumerable<FieldState> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los estados de cancha.", ex);
            }
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<FieldState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Retorna todos los estados.
        /// </summary>
        public IEnumerable<FieldState> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}
