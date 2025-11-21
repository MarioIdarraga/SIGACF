using System;
using System.Collections.Generic;
using System.Linq;
using BLL.BusinessException;
using DAL.Contracts;
using Domain;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio para gestionar los estados de reserva (BookingsStates).
    /// Encapsula la lógica de acceso al repositorio de <see cref="BookingState"/>.
    /// </summary>
    public class BookingStateService
    {
        private readonly IGenericRepository<BookingState> _repository;

        /// <summary>
        /// Crea una nueva instancia del servicio de estados de reserva.
        /// </summary>
        /// <param name="repository">
        /// Repositorio genérico de <see cref="BookingState"/> utilizado para el acceso a datos.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si el repositorio recibido es <c>null</c>.
        /// </exception>
        public BookingStateService(IGenericRepository<BookingState> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Obtiene todos los estados de reserva disponibles.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="BookingState"/> con todos los estados.
        /// </returns>
        /// <exception cref="BusinessException">
        /// Se lanza cuando ocurre un error al consultar los estados en la capa de datos.
        /// </exception>
        public List<BookingState> GetAll()
        {
            try
            {
                return _repository.GetAll().ToList();
            }
            catch (BLL.BusinessException.BusinessException) 
            {
                // Si en alguna capa inferior ya se lanzó BusinessException, se propaga tal cual.
                throw;
            }
            catch (Exception ex)
            {
                // Envolvemos cualquier error técnico en una BusinessException de negocio.
                throw new BLL.BusinessException.BusinessException("Ocurrió un error al obtener los estados de reserva.", ex);
            }
        }
    }
}

