using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para los estados de cancha (FieldState).
    /// </summary>
    internal class FieldStateRepository : IGenericRepository<FieldState>
    {
        /// <summary>
        /// Estados precargados desde la base de datos real.
        /// </summary>
        private static readonly List<FieldState> _states = new List<FieldState>
        {
            new FieldState { IdFieldState = 1, FieldStateDescription = "Habilitada" },
            new FieldState { IdFieldState = 2, FieldStateDescription = "Inhabilitado" },
            new FieldState { IdFieldState = 3, FieldStateDescription = "En Mantenimiento" }
        };

        #region CRUD

        /// <summary>
        /// Inserta un nuevo estado de cancha en memoria.
        /// </summary>
        public void Insert(FieldState Object)
        {
            try
            {
                if (_states.Any(x => x.IdFieldState == Object.IdFieldState))
                    throw new Exception("El estado de cancha ya existe.");

                _states.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el estado de cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza un estado de cancha existente.
        /// </summary>
        public void Update(Guid Id, FieldState Object)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var existing = _states.FirstOrDefault(x => x.IdFieldState == numericId);

                if (existing == null)
                    throw new Exception("El estado de cancha no existe.");

                existing.FieldStateDescription = Object.FieldStateDescription;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina un estado de cancha según su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var item = _states.FirstOrDefault(x => x.IdFieldState == numericId);

                if (item == null)
                    throw new Exception("El estado de cancha no existe.");

                _states.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el estado de cancha en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene un estado de cancha por su Id.
        /// </summary>
        public FieldState GetOne(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                return _states.FirstOrDefault(x => x.IdFieldState == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cancha en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los estados de cancha.
        /// </summary>
        public IEnumerable<FieldState> GetAll()
        {
            try
            {
                return _states.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados de cancha en memoria.", ex);
            }
        }

        // Las demás sobrecargas no aplican → devuelven todos

        public IEnumerable<FieldState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
            => GetAll();

        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
            => GetAll();

        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
            => GetAll();

        public IEnumerable<FieldState> GetAll(DateTime? from, DateTime? to, int state)
            => GetAll();

        #endregion

        #region Helpers

        /// <summary>
        /// Convierte un Guid en un entero usando los primeros 8 caracteres hexadecimales.
        /// Se utiliza para mantener compatibilidad con SQL y FILE.
        /// </summary>
        private int ConvertGuidToInt(Guid id)
        {
            return Convert.ToInt32(id.ToString().Substring(0, 8), 16);
        }

        #endregion
    }
}
