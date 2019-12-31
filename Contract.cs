using System;
using System.Collections.Generic;

namespace ASPNetCoreAPI
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public DateTime Date { get; set; }
        public int DateDepositPayed { get; set; }
        public int OrderId { get; set; }
        public int ContractTotalPrice { get; set; }
        public int ContractTotalPriceIncVat { get; set; }
        public int ProductionProcess { get; set; }
    }
}
