using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    /// <summary>
    /// Define las operaciones de acceso a datos relacionadas con 
    /// los métodos de pago utilizados en el sistema.
    /// </summary>
    public interface IPaymentMethodRepository
    {
        /// <summary>
        /// Obtiene la lista completa de métodos de pago disponibles.
        /// </summary>
        /// <returns>
        /// Colección de objetos <see cref="PaymentMethod"/> que representan
        /// todos los métodos de pago registrados.
        /// </returns>
        IEnumerable<PaymentMethod> GetAll();

        /// <summary>
        /// Obtiene un método de pago específico según su identificador.
        /// </summary>
        /// <param name="id">Identificador numérico del método de pago.</param>
        /// <returns>
        /// Instancia de <see cref="PaymentMethod"/> correspondiente al ID,
        /// o null si no existe.
        /// </returns>
        PaymentMethod GetOne(int id);
    }
}
