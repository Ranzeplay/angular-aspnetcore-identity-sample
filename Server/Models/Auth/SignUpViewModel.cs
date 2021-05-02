using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Auth
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
