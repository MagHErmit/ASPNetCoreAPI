using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Coments
    {
        public int ComentId { get; set; }
        public int? BoatId { get; set; }
        public int? CustomerId { get; set; }
        public string Coment { get; set; }
    }
}
