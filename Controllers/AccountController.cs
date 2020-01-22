using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System.Net.Cache;
using System.Text;
using System.Runtime.Serialization.Json;
using ASPNetCoreAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public JsonResult AppRegistration(string json)
        {
            var result = new List<string>();

            try
            {
                var inputJObject = JObject.Parse(json);
                var authJson = inputJObject["auth"];
                var customerJson = inputJObject["customer"];
                if (_db.Auth.FirstOrDefault(m => m.Username == (string) authJson["username"]) != null)
                    result.Add("Данный логин занят");
                if (_db.Customers.FirstOrDefault(m => m.IdNumber == (string) customerJson["idNumber"]) != null)
                    result.Add("По данному паспорту уже зарегестрирован аккаунт");

                if (result.Count == 0)
                {
                    Customers customers = new Customers()
                    {
                        CustomerId = _db.Customers.Max(m => m.CustomerId) + 1,
                        IdNumber = customerJson["idNumber"].ToString(),
                        IdDocumentName = Convert.ToInt32(customerJson["idDocumentName"]),
                        City = customerJson["city"].ToString(),
                        Address = customerJson["address"].ToString(),
                        DateOfBirth = DateTime.Parse(customerJson["dateOfBirth"].ToString()),
                        Email = customerJson["email"].ToString(),
                        FirstName = customerJson["firstName"].ToString(),
                        SecondName = customerJson["secondName"].ToString(),
                        PhoneNumber = customerJson["phoneNumber"].ToString()
                    };

                    Auth auth = new Auth()
                    {
                        CustomerId = customers.CustomerId,
                        Username = authJson["username"].ToString(),
                        Password = authJson["password"].ToString(),
                        UserType = 1
                    };

                    _db.Auth.Add(auth);
                    _db.Customers.Add(customers);

                    _db.SaveChanges();
                }
            }
            catch
            {
                result.Add("Проблема с регистрацией.");
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Auth user = await _db.Auth.FirstOrDefaultAsync(u => u.Username == model.Login && u.Password == model.Password && (u.UserType == 2 || u.UserType == 3)); // Юзеры с типом 2 - админы, 3 - модеры.
                if (user != null)
                {
                    await Authenticate(model.Login, user.UserType); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName, int role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
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