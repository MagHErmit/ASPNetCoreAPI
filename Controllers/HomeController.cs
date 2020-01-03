using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNetCoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ASPNetCoreAPI.Controllers
{
    public class abc
    {
        public int a { get; set; }
        public int b { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        xismhdqwContext db;
        public HomeController(ILogger<HomeController> logger, xismhdqwContext context)
        {
            _logger = logger;
            db = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public JsonResult Get(string name, string? id)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == name);
            if (type != null)
                DbSet catContext = db.Set(type);
            return Json(db.AccessoryId.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
