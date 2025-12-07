using System;
using System.Collections.Generic;
using System.Linq;
using BLL.BusinessException;
using DAL.Contracts;
using Domain;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio para gestionar las promociones.
    /// </summary>
    public class PromotionService
    {
        private readonly IGenericRepository<Promotion> _repository;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de promociones.
        /// </summary>
        /// <param name="repository">Repositorio genérico de <see cref="Promotion"/>.</param>
        public PromotionService(IGenericRepository<Promotion> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Obtiene todas las promociones disponibles.
        /// </summary>
        /// <returns>Lista de promociones.</returns>
        public List<Promotion> GetAll()
        {
            try
            {
                return _repository.GetAll().ToList();
            }
            catch (BLL.BusinessException.BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BLL.BusinessException.BusinessException("Ocurrió un error al obtener las promociones.", ex);
            }
        }
    }
}
