using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ASPNetCoreAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ASPNetCoreAPI.Controllers
{
    public class AccountController : Controller
    {
        private xismhdqwContext _db;

        public AccountController(xismhdqwContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public JsonResult AppLogin(string json)
        {
            try
            {
                JObject auth = JObject.Parse(json);
                Auth user = _db.Auth.FirstOrDefault(u =>
                    u.Username == (string) auth["username"] && u.Password == (string) auth["password"]);
                if (user != null)
                    return Json(user.CustomerId);
                else
                    return Json(-1);
            }
            catch
            {
                return Json(-1);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Auth user = await _db.Auth.FirstOrDefaultAsync(u => u.Username == model.Login && u.Password == model.Password && u.UserType == 2); // Юзеры с типом 2 - админы.
                if (user != null)
                {
                    await Authenticate(model.Login); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}