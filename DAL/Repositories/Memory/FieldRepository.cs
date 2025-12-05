using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para gestionar canchas (Field).
    /// </summary>
    internal class FieldRepository : IGenericRepository<Field>
    {
        /// <summary>
        /// Lista estática con canchas precargadas.
        /// </summary>
        private static readonly List<Field> _fields = new List<Field>
        {
            new Field
            {
                IdField = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Cancha 5",
                Capacity = 10,
                FieldType = 1,
                HourlyCost = 5000,
                IdFieldState = 1,
                DVH = ""
            },
            new Field
            {
                IdField = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Cancha 7",
                Capacity = 14,
                FieldType = 1,
                HourlyCost = 7000,
                IdFieldState = 1,
                DVH = ""
            },
            new Field
            {
                IdField = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Cancha Techada",
                Capacity = 12,
                FieldType = 2,
                HourlyCost = 9000,
                IdFieldState = 2,
                DVH = ""
            }
        };

        #region CRUD

        /// <summary>
        /// Inserta una nueva cancha en memoria.
        /// </summary>
        public void Insert(Field Object)
        {
            try
            {
                if (Object.IdField == Guid.Empty)
                    Object.IdField = Guid.NewGuid();

                _fields.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza una cancha existente.
        /// </summary>
        public void Update(Guid Id, Field Object)
        {
            try
            {
                var existing = _fields.FirstOrDefault(f => f.IdField == Id);

                if (existing == null)
                    throw new Exception("La cancha no existe.");

                _fields.Remove(existing);

                Object.IdField = Id;
                _fields.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina una cancha según su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var existing = _fields.FirstOrDefault(f => f.IdField == Id);

                if (existing == null)
                    throw new Exception("La cancha no existe.");

                _fields.Remove(existing);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la cancha en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene una cancha por su ID.
        /// </summary>
        public Field GetOne(Guid Id)
        {
            try
            {
                return _fields.FirstOrDefault(f => f.IdField == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las canchas.
        /// </summary>
        public IEnumerable<Field> GetAll()
        {
            try
            {
                return _fields.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las canchas en memoria.", ex);
            }
        }

        /// <summary>
        /// Filtros no aplican en MEMORIA. Se devuelven todas.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
            => GetAll();

        public IEnumerable<Field> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
            => GetAll();

        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
            => GetAll();

        public IEnumerable<Field> GetAll(DateTime? from, DateTime? to, int state)
            => GetAll();

        #endregion
    }
}

