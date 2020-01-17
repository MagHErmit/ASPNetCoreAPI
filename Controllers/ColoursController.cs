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
    public class ColoursController : Controller
    {
        private readonly xismhdqwContext _context;

        public ColoursController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Colours
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colours.ToListAsync());
        }

        // GET: Colours/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colours = await _context.Colours
                .FirstOrDefaultAsync(m => m.ColourId == id);
            if (colours == null)
            {
                return NotFound();
            }

            return View(colours);
        }

        // GET: Colours/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColourId,Colour")] Colours colours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colours);
        }

        // GET: Colours/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colours = await _context.Colours.FindAsync(id);
            if (colours == null)
            {
                return NotFound();
            }
            return View(colours);
        }

        // POST: Colours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColourId,Colour")] Colours colours)
        {
            if (id != colours.ColourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColoursExists(colours.ColourId))
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
            return View(colours);
        }

        // GET: Colours/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colours = await _context.Colours
                .FirstOrDefaultAsync(m => m.ColourId == id);
            if (colours == null)
            {
                return NotFound();
            }

            return View(colours);
        }

        // POST: Colours/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colours = await _context.Colours.FindAsync(id);
            _context.Colours.Remove(colours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColoursExists(int id)
        {
            return _context.Colours.Any(e => e.ColourId == id);
        }
    }
}
