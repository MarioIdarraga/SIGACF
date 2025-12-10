using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio encargado de gestionar los estados de cliente.
    /// Actúa como capa intermedia entre la BLL y los repositorios de CustomerState.
    /// </summary>
    public class CustomerStateService
    {
        private readonly ICustomerStateRepository _repo;

        /// <summary>
        /// Inicializa la instancia del servicio con el repositorio correspondiente.
        /// </summary>
        /// <param name="repo">Repositorio de estados de cliente.</param>
        public CustomerStateService(ICustomerStateRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene la lista completa de estados de cliente existentes.
        /// </summary>
        /// <returns>Lista de objetos <see cref="CustomerState"/>.</returns>
        public List<CustomerState> GetAll() => _repo.GetAll();

        /// <summary>
        /// Obtiene un estado de cliente específico según su identificador.
        /// </summary>
        /// <param name="id">Identificador del estado de cliente.</param>
        /// <returns>
        /// Instancia de <see cref="CustomerState"/> correspondiente al ID,
        /// o null si no existe.
        /// </returns>
        public CustomerState GetById(int id) => _repo.GetById(id);
    }
}
