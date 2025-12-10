using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    /// <summary>
    /// Define operaciones genéricas de acceso a datos para entidades identificadas por un <see cref="Guid"/>.
    /// Permite insertar, actualizar, eliminar y obtener registros con distintos criterios de filtrado.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad gestionada por el repositorio.</typeparam>
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Inserta un nuevo registro en el repositorio.
        /// </summary>
        /// <param name="Object">Instancia de la entidad a insertar.</param>
        void Insert(T Object);

        /// <summary>
        /// Actualiza un registro existente según su identificador.
        /// </summary>
        /// <param name="Id">Identificador único de la entidad.</param>
        /// <param name="Object">Entidad con los datos actualizados.</param>
        void Update(Guid Id, T Object);

        /// <summary>
        /// Elimina un registro del repositorio según su identificador.
        /// </summary>
        /// <param name="Id">Identificador único del registro a eliminar.</param>
        void Delete(Guid Id);

        /// <summary>
        /// Obtiene todos los registros del repositorio.
        /// </summary>
        /// <returns>Colección completa de entidades <typeparamref name="T"/>.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Obtiene registros filtrados por número de documento y fechas.
        /// Usado típicamente en consultas de reservas u operaciones con dos fechas.
        /// </summary>
        /// <param name="nroDocument">Número de documento opcional para filtrar.</param>
        /// <param name="registrationBooking">Fecha de reserva.</param>
        /// <param name="registrationDate">Fecha de registro.</param>
        /// <returns>Conjunto filtrado de entidades.</returns>
        IEnumerable<T> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate);

        /// <summary>
        /// Obtiene un registro específico según su identificador único.
        /// </summary>
        /// <param name="Id">Identificador de la entidad.</param>
        /// <returns>Instancia de <typeparamref name="T"/> si existe; de lo contrario, null.</returns>
        T GetOne(Guid Id);

        /// <summary>
        /// Obtiene registros filtrados por datos personales del cliente.
        /// Usado principalmente para entidades relacionadas con usuarios o clientes.
        /// </summary>
        /// <param name="nroDocument">Documento del cliente.</param>
        /// <param name="firstName">Nombre.</param>
        /// <param name="lastName">Apellido.</param>
        /// <param name="telephone">Teléfono.</param>
        /// <param name="mail">Correo electrónico.</param>
        /// <param name="state">Estado.</param>
        /// <returns>Colección filtrada de entidades.</returns>
        IEnumerable<T> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state);

        /// <summary>
        /// Obtiene registros filtrados por fecha y estado.
        /// Usado principalmente en consultas de pagos o reservas por rango de fechas.
        /// </summary>
        /// <param name="from">Fecha inicial del rango.</param>
        /// <param name="to">Fecha final del rango.</param>
        /// <param name="state">Estado aplicable al filtro.</param>
        /// <returns>Colección filtrada de entidades.</returns>
        IEnumerable<T> GetAll(DateTime? from, DateTime? to, int state);
    }
}

