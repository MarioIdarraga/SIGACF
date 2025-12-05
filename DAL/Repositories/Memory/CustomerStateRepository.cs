using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para los estados de cliente (CustomerState).
    /// Sin SQL ni FILE, ideal para demo y pruebas.
    /// </summary>
    internal class CustomerStateRepository : IGenericRepository<CustomerState>
    {
        /// <summary>
        /// Estados precargados según la base de datos real.
        /// </summary>
        private static readonly List<CustomerState> _states = new List<CustomerState>
        {
            new CustomerState { IdCustomerState = 1, Description = "Activo" },
            new CustomerState { IdCustomerState = 2, Description = "Inactivo" },
            new CustomerState { IdCustomerState = 3, Description = "Suspendido" },
            new CustomerState { IdCustomerState = 4, Description = "Bloqueado" }
        };

        #region CRUD

        public void Insert(CustomerState Object)
        {
            try
            {
                if (_states.Any(x => x.IdCustomerState == Object.IdCustomerState))
                    throw new Exception("El estado de cliente ya existe.");

                _states.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar estado de cliente en memoria.", ex);
            }
        }

        public void Update(Guid Id, CustomerState Object)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var existing = _states.FirstOrDefault(x => x.IdCustomerState == numericId);

                if (existing == null)
                    throw new Exception("El estado de cliente no existe.");

                existing.Description = Object.Description;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de cliente en memoria.", ex);
            }
        }

        public void Delete(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                var item = _states.FirstOrDefault(x => x.IdCustomerState == numericId);

                if (item == null)
                    throw new Exception("El estado de cliente no existe.");

                _states.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar estado de cliente en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        public CustomerState GetOne(Guid Id)
        {
            try
            {
                int numericId = ConvertGuidToInt(Id);
                return _states.FirstOrDefault(x => x.IdCustomerState == numericId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estado de cliente en memoria.", ex);
            }
        }

        public IEnumerable<CustomerState> GetAll()
        {
            try
            {
                return _states.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los estados de cliente en memoria.", ex);
            }
        }

        // Las sobrecargas no aplican para estados → devuelven todo
        public IEnumerable<CustomerState> GetAll(int? nd, string fn, string ln, string tel, string mail) => GetAll();
        public IEnumerable<CustomerState> GetAll(int? nd, DateTime? rb, DateTime? rd) => GetAll();
        public IEnumerable<CustomerState> GetAll(DateTime? from, DateTime? to, int state) => GetAll();
        public IEnumerable<CustomerState> GetAll(int? nd, string fn, string ln, string tel, string mail, int state) => GetAll();

        #endregion

        #region Helpers

        private int ConvertGuidToInt(Guid id)
        {
            return Convert.ToInt32(id.ToString().Substring(0, 8), 16);
        }

        #endregion
    }
}
