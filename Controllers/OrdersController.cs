using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

                JSonHelper helper = new JSonHelper();
                Orders orders = helper.ConvertJSonToObject<Orders>(ordersJToken.ToString());
                Contract contract = helper.ConvertJSonToObject<Contract>(contractJToken.ToString());
                List<Details> details = helper.ConvertJSonToObject<List<Details>>(detailsJToken.ToString());

                // TODO Do adding members into tables.

                return "Order is accepted";
            }
            catch
            {
                return "Error";
            }
        }
    }
}