using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DAL.Contracts;


namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio encargado de gestionar la obtención de pagos.
    /// Actúa como capa intermedia entre la BLL y el repositorio de pagos.
    /// </summary>
    public class PayService
    {
        /// <summary>
        /// Repositorio utilizado para acceder a los datos de pagos.
        /// </summary>
        private readonly IPayRepository _repo;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de pagos.
        /// </summary>
        /// <param name="repo">Repositorio de pagos que implementa <see cref="IPayRepository"/>.</param>
        public PayService(IPayRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene la lista de pagos filtrada por un rango opcional de fechas.
        /// </summary>
        /// <param name="registrationSince">Fecha inicial del filtro, o null para no aplicar límite inferior.</param>
        /// <param name="registrationUntil">Fecha límite superior del filtro, o null para no aplicar límite superior.</param>
        /// <returns>Colección de objetos <see cref="Pay"/> recuperados desde el repositorio.</returns>
        public IEnumerable<Pay> GetPayments(DateTime? registrationSince, DateTime? registrationUntil)
        {
            return _repo.GetAll(registrationSince, registrationUntil);
        }
    }
}
