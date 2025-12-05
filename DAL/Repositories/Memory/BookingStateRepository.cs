using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para BookingState.
    /// Contiene estados precargados desde la configuración SQL real.
    /// </summary>
    internal class BookingStateRepository : IGenericRepository<BookingState>
    {
        private static readonly List<BookingState> _states = new List<BookingState>()
        {
            new BookingState { IdStateBooking = 1, Description = "Pendiente" },
            new BookingState { IdStateBooking = 2, Description = "Confirmada" },
            new BookingState { IdStateBooking = 3, Description = "Cancelada" }
        };

        public void Insert(BookingState Object)
        {
            try
            {
                if (_states.Any(x => x.IdStateBooking == Object.IdStateBooking))
                    throw new Exception("El estado ya existe.");

                _states.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar estado de reserva en memoria.", ex);
            }
        }

        public void Update(Guid Id, BookingState Object)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var existing = _states.FirstOrDefault(x => x.IdStateBooking == numericId);

                if (existing == null)
                    throw new Exception("El estado no existe.");

                _states.Remove(existing);
                _states.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de reserva en memoria.", ex);
            }
        }

        public void Delete(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var item = _states.FirstOrDefault(x => x.IdStateBooking == numericId);

                if (item == null)
                    throw new Exception("El estado no existe.");

                _states.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar estado de reserva en memoria.", ex);
            }
        }

        public BookingState GetOne(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                return _states.FirstOrDefault(x => x.IdStateBooking == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estado de reserva en memoria.", ex);
            }
        }

        public IEnumerable<BookingState> GetAll()
        {
            try
            {
                return _states.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener lista de estados de reserva en memoria.", ex);
            }
        }

        public IEnumerable<BookingState> GetAll(int? nd, string fn, string ln, string tel, string mail) => GetAll();
        public IEnumerable<BookingState> GetAll(int? nd, DateTime? rb, DateTime? rd) => GetAll();
        public IEnumerable<BookingState> GetAll(DateTime? from, DateTime? to, int state) => GetAll();

        private int ConvertGuidToInt(Guid id)
        {
            return Convert.ToInt32(id.ToString().Substring(0, 8), 16);
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }
    }
}

