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
    public class WoodsController : Controller
    {
        private readonly xismhdqwContext _context;

        public WoodsController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Woods
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wood.ToListAsync());
        }

        // GET: Woods/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wood = await _context.Wood
                .FirstOrDefaultAsync(m => m.WoodId == id);
            if (wood == null)
            {
                return NotFound();
            }

            return View(wood);
        }

        // GET: Woods/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Woods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WoodId,Wood1")] Wood wood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wood);
        }

        // GET: Woods/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wood = await _context.Wood.FindAsync(id);
            if (wood == null)
            {
                return NotFound();
            }
            return View(wood);
        }

        // POST: Woods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WoodId,Wood1")] Wood wood)
        {
            if (id != wood.WoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WoodExists(wood.WoodId))
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
            return View(wood);
        }

        // GET: Woods/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wood = await _context.Wood
                .FirstOrDefaultAsync(m => m.WoodId == id);
            if (wood == null)
            {
                return NotFound();
            }

            return View(wood);
        }

        // POST: Woods/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wood = await _context.Wood.FindAsync(id);
            _context.Wood.Remove(wood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WoodExists(int id)
        {
            return _context.Wood.Any(e => e.WoodId == id);
        }
    }
}
