using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {

        public Guid IdBooking { get; set; }

        public Guid IdCustomer { get; set; }

        public string NroDocument { get; set; }

        public DateTime RegistrationDate { get; set; }  ///No quiero que se vea en la carga de ka reserva

        public DateTime RegistrationBooking { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public Guid Field { get; set; }

        public Guid Promotion { get; set; }

        public int State { get; set; } ///No se ve en la carga de la reserva

    }
}
