using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int SelesPersonId { get; set; }
        public int CustomerId { get; set; }
        public int BoatId { get; set; }
        public string DeliveryAddress { get; set; }
        public string City { get; set; }
    }
}
