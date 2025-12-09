using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa una reserva de cancha dentro del sistema SIGACF.
    /// Contiene información del cliente, horario, cancha asignada,
    /// promociones aplicadas, estado, importe y datos para control de integridad.
    /// </summary>
    public class Booking : IDVH
    {
        /// <summary>
        /// Identificador único de la reserva.
        /// </summary>
        public Guid IdBooking { get; set; }

        /// <summary>
        /// Identificador del cliente asociado a la reserva.
        /// </summary>
        public Guid IdCustomer { get; set; }

        /// <summary>
        /// Número de documento del cliente asociado a la reserva.
        /// </summary>
        public string NroDocument { get; set; }

        /// <summary>
        /// Fecha en la que se registró la reserva en el sistema.
        /// Este campo no debe mostrarse en la pantalla de carga de reservas.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Fecha en la cual se realizará la reserva de la cancha.
        /// </summary>
        public DateTime RegistrationBooking { get; set; }

        /// <summary>
        /// Hora de inicio de la reserva.
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Hora de fin de la reserva.
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Identificador de la cancha asignada a la reserva.
        /// </summary>
        public Guid Field { get; set; }

        /// <summary>
        /// Descripción adicional asociada a la reserva.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador de la promoción aplicada en la reserva.
        /// </summary>
        public Guid Promotion { get; set; }

        /// <summary>
        /// Descripción de la promoción aplicada.
        /// </summary>
        public string PromotionDescription { get; set; }

        /// <summary>
        /// Estado actual de la reserva.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Descripción del estado de la reserva.
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// Importe total de la reserva.
        /// </summary>
        public decimal ImporteBooking { get; set; }

        /// <summary>
        /// Dígito verificador horizontal utilizado para control de integridad.
        /// </summary>
        public string DVH { get; set; }
    }
}
