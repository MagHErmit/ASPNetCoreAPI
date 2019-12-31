using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Boat
    {
        public int BoatId { get; set; }
        public string Model { get; set; }
        public int BoatType { get; set; }
        public int NumberOfRowers { get; set; }
        public int Colour { get; set; }
        public int Wood { get; set; }
        public int BasePrice { get; set; }
        public int Vat { get; set; }
    }
}
