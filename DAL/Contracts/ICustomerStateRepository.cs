using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    /// <summary>
    /// Define las operaciones de acceso a datos para los estados de cliente.
    /// Proporciona métodos para obtener todos los estados o uno específico por ID.
    /// </summary>
    public interface ICustomerStateRepository
    {
        /// <summary>
        /// Obtiene la lista completa de estados de cliente.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="CustomerState"/> representando los estados disponibles.
        /// </returns>
        List<CustomerState> GetAll();

        /// <summary>
        /// Obtiene un estado de cliente según su identificador.
        /// </summary>
        /// <param name="id">Identificador numérico del estado.</param>
        /// <returns>
        /// Instancia de <see cref="CustomerState"/> si existe; de lo contrario, null.
        /// </returns>
        CustomerState GetById(int id);
    }
}
