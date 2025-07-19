using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SL.Helpers
{
    public static class Session
    {
        public static User CurrentUser { get; set; }
    }
}
