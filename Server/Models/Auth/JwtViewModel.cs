using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Auth
{
    public class JwtViewModel
    {
        public string Content { get; set; }

        public int ExpireHours { get; set; }

        public bool IsSucceeded { get; set; }
    }
}
