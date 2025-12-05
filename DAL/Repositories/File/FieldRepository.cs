using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para gestionar canchas (Field).
    /// Utiliza archivo TXT con formato delimitado por '|'.
    /// </summary>
    internal class FieldRepository : IGenericRepository<Field>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public FieldRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "Fields.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de Fields.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Carga todas las canchas desde el archivo.
        /// </summary>
        private List<Field> LoadAll()
        {
            try
            {
                var list = new List<Field>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 7) continue;

                    list.Add(new Field
                    {
                        IdField = Guid.Parse(parts[0]),
                        Name = parts[1],
                        Capacity = int.Parse(parts[2]),
                        FieldType = int.Parse(parts[3]),
                        HourlyCost = decimal.Parse(parts[4]),
                        IdFieldState = int.Parse(parts[5]),
                        DVH = parts[6]
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las canchas.", ex);
            }
        }

        /// <summary>
        /// Guarda todas las canchas en el archivo.
        /// </summary>
        private void SaveAll(List<Field> list)
        {
            try
            {
                var lines = list.Select(x =>
                    $"{x.IdField}|{x.Name}|{x.Capacity}|{x.FieldType}|{x.HourlyCost}|{x.IdFieldState}|{x.DVH}");

                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar las canchas.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta una nueva cancha.
        /// </summary>
        public void Insert(Field Object)
        {
            try
            {
                var fields = LoadAll();

                fields.Add(Object);
                SaveAll(fields);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la cancha.", ex);
            }
        }

        /// <summary>
        /// Actualiza una cancha existente.
        /// </summary>
        public void Update(Guid Id, Field Object)
        {
            try
            {
                var fields = LoadAll();

                var existing = fields.FirstOrDefault(f => f.IdField == Id);

                if (existing == null)
                    throw new Exception("La cancha no existe.");

                fields.Remove(existing);
                fields.Add(Object);
                SaveAll(fields);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cancha.", ex);
            }
        }

        /// <summary>
        /// Elimina una cancha.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var fields = LoadAll();
                var item = fields.FirstOrDefault(f => f.IdField == Id);

                if (item == null)
                    throw new Exception("La cancha no existe.");

                fields.Remove(item);
                SaveAll(fields);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la cancha.", ex);
            }
        }

        /// <summary>
        /// Obtiene una cancha por ID.
        /// </summary>
        public Field GetOne(Guid Id)
        {
            try
            {
                var fields = LoadAll();
                return fields.FirstOrDefault(f => f.IdField == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cancha.", ex);
            }
        }

        #endregion



        #region GetAll

        /// <summary>
        /// Obtiene todas las canchas.
        /// </summary>
        public IEnumerable<Field> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las canchas.", ex);
            }
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Devuelve todas.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Devuelve todas.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Devuelve todas.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            return GetAll();
        }

        /// <summary>
        /// Los filtros no aplican en FILE. Devuelve todas.
        /// </summary>
        public IEnumerable<Field> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}
