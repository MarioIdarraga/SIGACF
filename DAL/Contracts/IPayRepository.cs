using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad <see cref="Pay"/>.
    /// Permite registrar, modificar, eliminar y consultar pagos realizados en el sistema.
    /// </summary>
    public interface IPayRepository
    {
        /// <summary>
        /// Inserta un nuevo registro de pago.
        /// </summary>
        /// <param name="obj">Instancia de <see cref="Pay"/> a insertar.</param>
        void Insert(Pay obj);

        /// <summary>
        /// Actualiza un pago existente según su identificador.
        /// </summary>
        /// <param name="id">Identificador del pago.</param>
        /// <param name="obj">Entidad <see cref="Pay"/> con los datos actualizados.</param>
        void Update(int id, Pay obj);

        /// <summary>
        /// Elimina un registro de pago según su identificador.
        /// </summary>
        /// <param name="id">Identificador del pago a eliminar.</param>
        void Delete(int id);

        /// <summary>
        /// Obtiene todos los pagos registrados.
        /// </summary>
        /// <returns>Colección completa de entidades <see cref="Pay"/>.</returns>
        IEnumerable<Pay> GetAll();

        /// <summary>
        /// Obtiene la lista de pagos filtrada por un rango opcional de fechas.
        /// </summary>
        /// <param name="registrationSincePay">Fecha inicial del filtro, o null para no aplicar límite inferior.</param>
        /// <param name="registrationUntilPay">Fecha final del filtro, o null para no aplicar límite superior.</param>
        /// <returns>Colección filtrada de pagos.</returns>
        IEnumerable<Pay> GetAll(DateTime? registrationSincePay, DateTime? registrationUntilPay);

        /// <summary>
        /// Obtiene un pago específico según su identificador.
        /// </summary>
        /// <param name="id">Identificador del pago.</param>
        /// <returns>Instancia de <see cref="Pay"/> si existe; de lo contrario null.</returns>
        Pay GetOne(int id);
    }
}
