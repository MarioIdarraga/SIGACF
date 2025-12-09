using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.DAL.Contracts
{
    /// <summary>
    /// Define operaciones genéricas de negocio para CRUD
    /// aplicables a cualquier entidad del sistema.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad sobre la cual se aplican las operaciones.</typeparam>
    internal interface IGenericBusinessService<T>
    {
        /// <summary>
        /// Inserta una nueva entidad en el repositorio.
        /// </summary>
        /// <param name="Object">Entidad a insertar.</param>
        void Insert(T Object);

        /// <summary>
        /// Actualiza una entidad existente identificada por su Id.
        /// </summary>
        /// <param name="Id">Identificador único de la entidad.</param>
        /// <param name="Object">Entidad con los datos actualizados.</param>
        void Update(Guid Id, T Object);

        /// <summary>
        /// Elimina una entidad según su identificador.
        /// </summary>
        /// <param name="Id">Identificador único de la entidad a eliminar.</param>
        void Delete(Guid Id);

        /// <summary>
        /// Obtiene todas las entidades del repositorio.
        /// </summary>
        /// <returns>Listado de entidades de tipo <typeparamref name="T"/>.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Obtiene una entidad específica mediante su identificador.
        /// </summary>
        /// <param name="Id">Identificador único de la entidad.</param>
        /// <returns>Instancia de <typeparamref name="T"/> correspondiente al Id.</returns>
        T GetOne(Guid Id);
    }
}
