using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}