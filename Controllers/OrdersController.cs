using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASPNetCoreAPI.Controllers
{
    public class OrdersController : Controller
    {
        private xismhdqwContext _context;

        public OrdersController(xismhdqwContext context)
        {
            _context = context;
        }

        public JsonResult Get(int customerId)
        {
            var result = _context.Orders.Where(m => m.CustomerId == customerId);
            return Json(result);
        }

        public string Set(string json)
        {
            try
            {
                var inputJObject = JObject.Parse(json);
                var ordersJToken = inputJObject["orders"];
                var contractJToken = inputJObject["contract"];
                var detailsJToken = inputJObject["details"];

                Orders order = new Orders
                {
                    CustomerId = Convert.ToInt32(ordersJToken["customerId"]),
                    Date = DateTime.Now,
                    City = ordersJToken["city"].ToString(),
                    DeliveryAddress = ordersJToken["deliveryAddress"].ToString(),
                    BoatId = Convert.ToInt32(ordersJToken["boatId"]),
                    SelesPersonId = 0,
                    OrderId = _context.Orders.Max(m => m.OrderId) + 1
                };

                var details = detailsJToken.First;
                var index = _context.Details.Max(m => m.DetailId) + 1;

                while (details != null)
                {
                    var detail = new Details
                    {
                        AccessoryId = Convert.ToInt32(details["accessoryId"]),
                        DetailId = index++,
                        OrderId = order.OrderId
                    };
                    details = details.Next;
                    _context.Details.Add(detail);
                }

                Contract contract = new Contract()
                {
                    OrderId = order.OrderId,
                    Date = DateTime.Now,
                    ContractId = _context.Contract.Max(m => m.ContractId) + 1,
                    ContractTotalPrice = Convert.ToInt32(contractJToken["contractTotalPrice"]),
                    ContractTotalPriceIncVat = Convert.ToInt32(contractJToken["contractTotalPriceIncVat"]),
                    DateDepositPayed = 1,
                    ProductionProcess = 1
                };

                _context.Contract.Add(contract);
                _context.Orders.Add(order);
                _context.SaveChanges();

                return "Order is accepted";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
