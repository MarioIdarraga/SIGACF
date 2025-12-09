using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa un cliente dentro del sistema SIGACF.
    /// Contiene datos personales, información de contacto y estado del cliente.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        public Guid IdCustomer { get; set; }

        /// <summary>
        /// Número de documento del cliente.
        /// </summary>
        public int NroDocument { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Apellido del cliente.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Estado actual del cliente.
        /// Puede representar si está activo, inactivo, bloqueado, etc.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Descripción correspondiente al estado del cliente.
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// Comentario adicional asociado al cliente.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Teléfono de contacto del cliente.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del cliente.
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Dirección física del cliente.
        /// </summary>
        public string Address { get; set; }
    }
}
