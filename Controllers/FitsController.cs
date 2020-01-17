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
    public class FitsController : Controller
    {
        private readonly xismhdqwContext _context;

        public FitsController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Fits
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fit.ToListAsync());
        }

        // GET: Fits/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fit = await _context.Fit
                .FirstOrDefaultAsync(m => m.FitId == id);
            if (fit == null)
            {
                return NotFound();
            }

            return View(fit);
        }

        // GET: Fits/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FitId,AccessoryId,BoatId")] Fit fit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fit);
        }

        // GET: Fits/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fit = await _context.Fit.FindAsync(id);
            if (fit == null)
            {
                return NotFound();
            }
            return View(fit);
        }

        // POST: Fits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FitId,AccessoryId,BoatId")] Fit fit)
        {
            if (id != fit.FitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitExists(fit.FitId))
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
            return View(fit);
        }

        // GET: Fits/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fit = await _context.Fit
                .FirstOrDefaultAsync(m => m.FitId == id);
            if (fit == null)
            {
                return NotFound();
            }

            return View(fit);
        }

        // POST: Fits/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fit = await _context.Fit.FindAsync(id);
            _context.Fit.Remove(fit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitExists(int id)
        {
            return _context.Fit.Any(e => e.FitId == id);
        }
    }
}
