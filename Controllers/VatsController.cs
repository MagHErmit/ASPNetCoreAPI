using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreAPI;
using Microsoft.AspNetCore.Authorization;

namespace ASPNetCoreAPI.Controllers
{
    public class VatsController : Controller
    {
        private readonly xismhdqwContext _context;

        public VatsController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Vats
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vat.ToListAsync());
        }

        // GET: Vats/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vat = await _context.Vat
                .FirstOrDefaultAsync(m => m.VatId == id);
            if (vat == null)
            {
                return NotFound();
            }

            return View(vat);
        }

        // GET: Vats/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VatId,Vat1")] Vat vat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vat);
        }

        // GET: Vats/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vat = await _context.Vat.FindAsync(id);
            if (vat == null)
            {
                return NotFound();
            }
            return View(vat);
        }

        // POST: Vats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VatId,Vat1")] Vat vat)
        {
            if (id != vat.VatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VatExists(vat.VatId))
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
            return View(vat);
        }

        // GET: Vats/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vat = await _context.Vat
                .FirstOrDefaultAsync(m => m.VatId == id);
            if (vat == null)
            {
                return NotFound();
            }

            return View(vat);
        }

        // POST: Vats/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vat = await _context.Vat.FindAsync(id);
            _context.Vat.Remove(vat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VatExists(int id)
        {
            return _context.Vat.Any(e => e.VatId == id);
        }
    }
}
