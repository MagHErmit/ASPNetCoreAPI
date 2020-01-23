using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreAPI;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace ASPNetCoreAPI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly xismhdqwContext _context;

        public CustomersController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Customers
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,SecondName,DateOfBirth,Address,City,Email,PhoneNumber,IdNumber,IdDocumentName")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        public string AppEdit(string json)
        {
            try
            {
                var customer = JObject.Parse(json);
                Customers customers = new Customers()
                {
                    CustomerId = Convert.ToInt32(customer["customerId"]),
                    IdNumber = customer["idNumber"].ToString(),
                    IdDocumentName = Convert.ToInt32(customer["idDocumentName"]),
                    City = customer["city"].ToString(),
                    PhoneNumber = customer["phoneNumber"].ToString(),
                    Email = customer["email"].ToString(),
                    Address = customer["address"].ToString(),
                    DateOfBirth = DateTime.Parse(customer["dateOfBirth"].ToString()),
                    FirstName = customer["firstName"].ToString(),
                    SecondName = customer["secondName"].ToString()
                };

                _context.Update(customers);
                _context.SaveChangesAsync();
            }
            catch
            {

            }

            return "Edited";
        }

        public string AppDelete(int customerId)
        {
            Customers customers = _context.Customers.Find(customerId);
            Auth auth = _context.Auth.FirstOrDefault(m => m.CustomerId == customerId);

            if (customers != null)
                _context.Customers.Remove(customers);
            if (auth != null)
                _context.Auth.Remove(auth);
            _context.SaveChanges();

            return "Deleted";
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,SecondName,DateOfBirth,Address,City,Email,PhoneNumber,IdNumber,IdDocumentName")] Customers customers)
        {
            //if (id != customers.CustomerId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
