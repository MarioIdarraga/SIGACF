using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para gestionar los estados de reserva (BookingState).
    /// Almacena los datos en un archivo de texto delimitado por '|'.
    /// </summary>
    internal class BookingStateRepository : IGenericRepository<BookingState>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public BookingStateRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "BookingStates.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de BookingState.", ex);
            }
        }

        /// <summary>
        /// Carga todos los registros desde el archivo.
        /// </summary>
        private List<BookingState> LoadAll()
        {
            try
            {
                var list = new List<BookingState>();

                foreach (var line in System.IO.File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    list.Add(new BookingState
                    {
                        IdStateBooking = int.Parse(parts[0]),
                        Description = parts[1]
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los estados de reserva.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los registros en el archivo.
        /// </summary>
        private void SaveAll(List<BookingState> list)
        {
            try
            {
                var lines = list.Select(x => $"{x.IdStateBooking}|{x.Description}");
                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los estados de reserva.", ex);
            }
        }

        /// <summary>
        /// Inserta un nuevo estado de reserva.
        /// </summary>
        public void Insert(BookingState Object)
        {
            try
            {
                var list = LoadAll();

                if (list.Any(x => x.IdStateBooking == Object.IdStateBooking))
                    throw new Exception("El estado ya existe.");

                list.Add(Object);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el estado de reserva.", ex);
            }
        }

        /// <summary>
        /// Actualiza un estado de reserva existente.
        /// </summary>
        public void Update(Guid Id, BookingState Object)
        {
            try
            {
                var list = LoadAll();
                var existing = list.FirstOrDefault(x => x.IdStateBooking == Object.IdStateBooking);

                if (existing == null)
                    throw new Exception("El estado no existe.");

                list.Remove(existing);
                list.Add(Object);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de reserva.", ex);
            }
        }

        /// <summary>
        /// Elimina un estado de reserva.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var list = LoadAll();

                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);
                var item = list.FirstOrDefault(x => x.IdStateBooking == numericId);

                if (item == null)
                    throw new Exception("El estado no existe.");

                list.Remove(item);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el estado de reserva.", ex);
            }
        }

        /// <summary>
        /// Obtiene un estado de reserva por su identificador.
        /// </summary>
        public BookingState GetOne(Guid Id)
        {
            try
            {
                var list = LoadAll();
                int numericId = Convert.ToInt32(Id.ToString().Substring(0, 8), 16);

                return list.FirstOrDefault(x => x.IdStateBooking == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de reserva.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los estados de reserva.
        /// </summary>
        public IEnumerable<BookingState> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los estados de reserva.", ex);
            }
        }

        /// <summary>
        /// No aplica filtros para estados de reserva. Devuelve todos los registros.
        /// </summary>
        public IEnumerable<BookingState> GetAll(int? nd, string fn, string ln, string tel, string mail)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplica filtros para estados de reserva. Devuelve todos los registros.
        /// </summary>
        public IEnumerable<BookingState> GetAll(int? nd, DateTime? rb, DateTime? rd)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplica filtros para estados de reserva. Devuelve todos los registros.
        /// </summary>
        public IEnumerable<BookingState> GetAll(int? nd, string fn, string ln, string tel, string mail, int state)
        {
            return GetAll();
        }

        /// <summary>
        /// No aplica filtros para estados de reserva. Devuelve todos los registros.
        /// </summary>
        public IEnumerable<BookingState> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }
    }
}

