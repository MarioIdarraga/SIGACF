using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pay
    {
        public int IdPay { get; set; }

        public Guid IdBooking { get; set; }

        public int NroDocument { get; set; }

        public DateTime Date { get; set; }

        public int MethodPay { get; set; }

        public string PaymentMethodDescription { get; set; }

        public decimal Amount { get; set; }

        public int State { get; set; }

        public string StateDescription { get; set; }

    }
}
