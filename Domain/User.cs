using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain
{
    /// <summary>
    /// Representa un usuario del sistema SIGACF.
    /// Incluye información personal, credenciales de acceso, estado,
    /// y datos adicionales relacionados con seguridad y recuperación de contraseña.
    /// </summary>
    public class User : IDVH
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Nombre de usuario utilizado para iniciar sesión.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// Contraseña cifrada del usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Número de documento del usuario.
        /// </summary>
        public int NroDocument { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Cargo o posición del usuario en la organización.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Domicilio del usuario.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Teléfono de contacto del usuario.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Estado actual del usuario (activo, inactivo, bloqueado, etc.).
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Dígito Verificador Horizontal usado para control de integridad.
        /// </summary>
        public string DVH { get; set; }

        /// <summary>
        /// Indica si el usuario pertenece al personal de la organización.
        /// </summary>
        public bool IsEmployee { get; set; }

        /// <summary>
        /// Token utilizado para el proceso de recuperación de contraseña.
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        /// Fecha y hora de expiración del token de recuperación.
        /// </summary>
        public DateTime? ResetTokenExpiration { get; set; }

        /// <summary>
        /// Cantidad de intentos fallidos de inicio de sesión registrados.
        /// </summary>
        public int FailedAttempts { get; set; }
    }
}
