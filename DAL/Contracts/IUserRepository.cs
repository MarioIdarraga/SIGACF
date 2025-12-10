using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    /// <summary>
    /// Define las operaciones de acceso a datos para entidades de usuario,
    /// incluyendo creación, actualización, eliminación, consultas y soporte
    /// para recuperación de contraseña.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad de usuario gestionada por el repositorio.</typeparam>
    public interface IUserRepository<T>
    {
        /// <summary>
        /// Inserta un nuevo usuario en el repositorio.
        /// </summary>
        /// <param name="Object">Instancia de la entidad usuario a insertar.</param>
        void Insert(T Object);

        /// <summary>
        /// Actualiza un usuario existente según su identificador.
        /// </summary>
        /// <param name="Id">Identificador único del usuario.</param>
        /// <param name="Object">Entidad usuario con los datos actualizados.</param>
        void Update(Guid Id, T Object);

        /// <summary>
        /// Elimina un usuario según su identificador.
        /// </summary>
        /// <param name="Id">Identificador del usuario a eliminar.</param>
        void Delete(Guid Id);

        /// <summary>
        /// Obtiene todos los usuarios del repositorio.
        /// </summary>
        /// <returns>Colección completa de usuarios.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Obtiene una lista de usuarios filtrada por documento y fechas.
        /// Usado principalmente para operaciones con reservas.
        /// </summary>
        IEnumerable<T> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate);

        /// <summary>
        /// Obtiene un usuario según su identificador único.
        /// </summary>
        /// <param name="Id">Identificador del usuario.</param>
        /// <returns>Instancia del usuario o null si no existe.</returns>
        T GetOne(Guid Id);

        /// <summary>
        /// Obtiene usuarios filtrados por datos personales.
        /// </summary>
        IEnumerable<T> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail);

        /// <summary>
        /// Obtiene un usuario según su nombre de login.
        /// </summary>
        /// <param name="loginName">Nombre de inicio de sesión.</param>
        /// <returns>Instancia de <see cref="User"/> si existe; de lo contrario, null.</returns>
        User GetByLoginName(string loginName);

        /// <summary>
        /// Obtiene un usuario por nombre de usuario o email,
        /// utilizado en procesos de recuperación de contraseña.
        /// </summary>
        /// <param name="userOrMail">Nombre de usuario o dirección de correo.</param>
        /// <returns>Instancia de usuario si existe; null en caso contrario.</returns>
        User GetByUsernameOrEmail(string userOrMail);

        /// <summary>
        /// Guarda el token de recuperación de contraseña para el usuario especificado.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="token">Token generado para recuperación.</param>
        /// <param name="expiration">Fecha y hora de expiración del token.</param>
        void SavePasswordResetToken(Guid userId, string token, DateTime expiration);

        /// <summary>
        /// Obtiene un usuario asociado a un token de recuperación de contraseña.
        /// </summary>
        /// <param name="token">Token único de recuperación.</param>
        /// <returns>Usuario vinculado al token; null si no existe.</returns>
        User GetByPasswordResetToken(string token);

        /// <summary>
        /// Obtiene un usuario según su número de documento.
        /// </summary>
        /// <param name="nroDocument">Número de documento.</param>
        /// <returns>Instancia de usuario si existe; null si no.</returns>
        User GetByDocument(int nroDocument);
    }
}
