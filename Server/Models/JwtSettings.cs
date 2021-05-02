﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public int ExpireHours { get; set; }
    }
}
