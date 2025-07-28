using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO
{
  
        public static class Session
        {
            public static string Email { get; set; }
            public static string Password { get; set; } // optional: avoid storing raw password
        }

}
