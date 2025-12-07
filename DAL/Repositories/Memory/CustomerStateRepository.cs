using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio MEMORY para CustomerState.
    /// Mantiene los estados del cliente en memoria,
    /// simulando el comportamiento de un repositorio SQL o FILE.
    /// </summary>
    internal class CustomerStateRepository : ICustomerStateRepository
    {
        /// <summary>
        /// Lista interna de estados cargados en memoria.
        /// </summary>
        private readonly List<CustomerState> _states;

        /// <summary>
        /// Inicializa el repositorio cargando los estados predeterminados.
        /// </summary>
        public CustomerStateRepository()
        {
            try
            {
                _states = new List<CustomerState>
                {
                    new CustomerState { IdCustomerState = 1, Description = "Activo" },
                    new CustomerState { IdCustomerState = 2, Description = "Inactivo" },
                    new CustomerState { IdCustomerState = 3, Description = "Suspendido" },
                    new CustomerState { IdCustomerState = 4, Description = "Bloqueado" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar los estados de cliente en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los estados de cliente disponibles en memoria.
        /// </summary>
        public List<CustomerState> GetAll()
        {
            try
            {
                // Retornamos una copia para evitar modificaciones externas.
                return new List<CustomerState>(_states);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados de cliente en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene un estado de cliente por su identificador.
        /// </summary>
        public CustomerState GetById(int id)
        {
            try
            {
                return _states.FirstOrDefault(s => s.IdCustomerState == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el estado de cliente por ID en memoria.", ex);
            }
        }
    }
}

