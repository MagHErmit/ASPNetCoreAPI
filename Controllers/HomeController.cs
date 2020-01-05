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
using Microsoft.AspNetCore.Authorization;

namespace ASPNetCoreAPI.Controllers
{
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
            try
            {
                if (id == "all") return Json(Getobject(name));

                Type tp = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == name);
                if (tp == null) throw new Exception();

                return Json(db.Find(tp, Convert.ToInt32(id)));
            }
            catch
            {
                return Json(null);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private object Getobject(string name)
        {
            PropertyInfo[] props = Assembly.Load("ASPNetCoreAPI").GetType("ASPNetCoreAPI.xismhdqwContext").GetProperties();  // get all Properties object
            object obj = Assembly.Load("ASPNetCoreAPI").GetType("ASPNetCoreAPI.xismhdqwContext").GetConstructor(new Type[0]).Invoke(new object[0]);
            foreach (PropertyInfo a in props)
            {
                if (a.Name.Equals(name))
                {
                    return a.GetValue(obj);
                }
            }
            return null;
        }
    }
}
