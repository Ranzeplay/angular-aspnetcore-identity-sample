﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Auth
{
    public class SignInViewModel
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
