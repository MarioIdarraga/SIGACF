using System;

namespace Domain
{
    public class User
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
        public bool IsEmployee { get; set; }

    }
}