using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        
        public Guid IdCustomer { get; set; } 
        
        public int NroDocument { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int State { get; set; }

        public string Comment { get; set; }

        public string Telephone { get; set; }

        public string Mail { get; set; }

        public string Address { get; set; }

    }
}
