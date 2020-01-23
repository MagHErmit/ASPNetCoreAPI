using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreAPI;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace ASPNetCoreAPI.Controllers
{
    public class ComentsController : Controller
    {
        private readonly xismhdqwContext _context;

        public ComentsController(xismhdqwContext context)
        {
            _context = context;
        }

        public string AppCreate(string json)
        {
            try
            {
                var inputJObject = JObject.Parse(json);
                Coments coments = new Coments()
                {
                    CustomerId = Convert.ToInt32(inputJObject["customerId"]),
                    BoatId = Convert.ToInt32(inputJObject["boatId"]),
                    Coment = inputJObject["coment"].ToString(),
                    ComentId = _context.Coments.Max(m => m.ComentId) + 1
                };

                _context.Coments.Add(coments);
                _context.SaveChanges(); 

                return "ComentAdded";
            }
            catch
            {
                return "Error";
            }
        }

        public string AppDelete(int comentId)
        {
            try
            {
                Coments coments = _context.Coments.Find(comentId);
                _context.Coments.Remove(coments);
                _context.SaveChanges();
                return "Deleted";
            }
            catch
            {
                return "Error";
            }
        }

        // GET: Coments
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coments.ToListAsync());
        }

        // GET: Coments/Details/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coments = await _context.Coments
                .FirstOrDefaultAsync(m => m.ComentId == id);
            if (coments == null)
            {
                return NotFound();
            }

            return View(coments);
        }

        // GET: Coments/Create
        [Authorize(Roles = "3")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComentId,BoatId,CustomerId,Coment")] Coments coments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coments);
        }

        // GET: Coments/Edit/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coments = await _context.Coments.FindAsync(id);
            if (coments == null)
            {
                return NotFound();
            }
            return View(coments);
        }

        // POST: Coments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComentId,BoatId,CustomerId,Coment")] Coments coments)
        {
            if (id != coments.ComentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentsExists(coments.ComentId))
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
            return View(coments);
        }

        // GET: Coments/Delete/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coments = await _context.Coments
                .FirstOrDefaultAsync(m => m.ComentId == id);
            if (coments == null)
            {
                return NotFound();
            }

            return View(coments);
        }

        // POST: Coments/Delete/5
        [Authorize(Roles = "3")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coments = await _context.Coments.FindAsync(id);
            _context.Coments.Remove(coments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentsExists(int id)
        {
            return _context.Coments.Any(e => e.ComentId == id);
        }
    }
}
