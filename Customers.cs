using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Customers
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public int IdDocumentName { get; set; }
    }
}
