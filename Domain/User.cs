using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain
{
    public class User : IDVH
    {
        public Guid UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int NroDocument { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public int State { get; set; }
        public string DVH { get; set; }
        public bool IsEmployee { get; set; }

        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
        public int FailedAttempts { get; set; }

    }
}
