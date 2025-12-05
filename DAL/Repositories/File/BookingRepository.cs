using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para Booking.
    /// Gestiona las operaciones CRUD utilizando almacenamiento JSON.
    /// </summary>
    internal class BookingRepository : IGenericRepository<Booking>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio asegurando la existencia del archivo.
        /// </summary>
        public BookingRepository()
        {
            try
            {
                string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(dataDir))
                    Directory.CreateDirectory(dataDir);

                _filePath = Path.Combine(dataDir, "Bookings.json");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "[]");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de Bookings.", ex);
            }
        }

        #region Private Helpers

        /// <summary>
        /// Carga todos los registros desde el archivo JSON.
        /// </summary>
        private List<Booking> LoadAll()
        {
            try
            {
                string json = System.IO.File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos de reserva desde el archivo.", ex);
            }
        }

        /// <summary>
        /// Guarda la lista completa de reservas en el archivo JSON.
        /// </summary>
        private void SaveAll(List<Booking> bookings)
        {
            try
            {
                string json = JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los datos de reserva en el archivo.", ex);
            }
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Inserta una nueva reserva.
        /// </summary>
        public void Insert(Booking Object)
        {
            try
            {
                var bookings = LoadAll();
                bookings.Add(Object);
                SaveAll(bookings);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la reserva.", ex);
            }
        }

        /// <summary>
        /// Actualiza una reserva existente según su ID.
        /// </summary>
        public void Update(Guid Id, Booking Object)
        {
            try
            {
                var bookings = LoadAll();
                var existing = bookings.FirstOrDefault(x => x.IdBooking == Id);

                if (existing == null)
                    throw new Exception("No existe la reserva a actualizar.");

                bookings.Remove(existing);
                bookings.Add(Object);
                SaveAll(bookings);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva.", ex);
            }
        }

        /// <summary>
        /// Elimina una reserva según su ID.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var bookings = LoadAll();
                var item = bookings.FirstOrDefault(x => x.IdBooking == Id);

                if (item == null)
                    throw new Exception("No existe la reserva a eliminar.");

                bookings.Remove(item);
                SaveAll(bookings);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva.", ex);
            }
        }

        /// <summary>
        /// Obtiene una reserva por su identificador.
        /// </summary>
        public Booking GetOne(Guid Id)
        {
            try
            {
                var bookings = LoadAll();
                return bookings.FirstOrDefault(x => x.IdBooking == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la reserva.", ex);
            }
        }

        #endregion

        #region GET ALL – Filtros

        /// <summary>
        /// Obtiene todas las reservas sin filtros.
        /// </summary>
        public IEnumerable<Booking> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las reservas.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por documento y fechas.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            try
            {
                var list = LoadAll();

                if (nroDocument.HasValue)
                    list = list.Where(x => int.Parse(x.NroDocument) == nroDocument.Value).ToList();

                if (registrationBooking.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date == registrationBooking.Value.Date).ToList();

                if (registrationDate.HasValue)
                    list = list.Where(x => x.RegistrationDate.Date == registrationDate.Value.Date).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reservas filtradas por documento y fechas.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas por estado y rango de fechas.
        /// </summary>
        public IEnumerable<Booking> GetAll(DateTime? from, DateTime? to, int state)
        {
            try
            {
                var list = LoadAll().Where(x => x.State == state).ToList();

                if (from.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date >= from.Value.Date).ToList();

                if (to.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date <= to.Value.Date).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas por fecha y estado.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por datos del cliente.
        /// Solo aplica filtro por documento, ya que FILE no posee Customers.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = LoadAll();

                if (nroDocument.HasValue)
                    list = list.Where(x => int.Parse(x.NroDocument) == nroDocument.Value).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas por datos del cliente.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por documento y estado.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            try
            {
                var list = LoadAll();

                if (nroDocument.HasValue)
                    list = list.Where(x => int.Parse(x.NroDocument) == nroDocument.Value).ToList();

                list = list.Where(x => x.State == state).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas por documento y estado.", ex);
            }
        }

        #endregion
    }
}

