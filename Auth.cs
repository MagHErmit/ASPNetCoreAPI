using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Auth
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public int CustomerId { get; set; }
    }
}
