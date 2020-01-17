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
    public class DetailsController : Controller
    {
        private readonly xismhdqwContext _context;

        public DetailsController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: Details
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Details.ToListAsync());
        }

        // GET: Details/Details/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Details/Create
        [Authorize(Roles = "3")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,AccessoryId,OrderId")] Details details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(details);
        }

        // GET: Details/Edit/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,AccessoryId,OrderId")] Details details)
        {
            if (id != details.DetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsExists(details.DetailId))
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
            return View(details);
        }

        // GET: Details/Delete/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Details/Delete/5
        [Authorize(Roles = "3")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _context.Details.FindAsync(id);
            _context.Details.Remove(details);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailsExists(int id)
        {
            return _context.Details.Any(e => e.DetailId == id);
        }
    }
}
