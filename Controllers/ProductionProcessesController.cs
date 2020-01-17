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
    public class ProductionProcessesController : Controller
    {
        private readonly xismhdqwContext _context;

        public ProductionProcessesController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: ProductionProcesses
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionProcess.ToListAsync());
        }

        // GET: ProductionProcesses/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionProcess = await _context.ProductionProcess
                .FirstOrDefaultAsync(m => m.ProductionProcessId == id);
            if (productionProcess == null)
            {
                return NotFound();
            }

            return View(productionProcess);
        }

        // GET: ProductionProcesses/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductionProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionProcessId,ProductionProcess1")] ProductionProcess productionProcess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productionProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productionProcess);
        }

        // GET: ProductionProcesses/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionProcess = await _context.ProductionProcess.FindAsync(id);
            if (productionProcess == null)
            {
                return NotFound();
            }
            return View(productionProcess);
        }

        // POST: ProductionProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductionProcessId,ProductionProcess1")] ProductionProcess productionProcess)
        {
            if (id != productionProcess.ProductionProcessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionProcess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionProcessExists(productionProcess.ProductionProcessId))
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
            return View(productionProcess);
        }

        // GET: ProductionProcesses/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionProcess = await _context.ProductionProcess
                .FirstOrDefaultAsync(m => m.ProductionProcessId == id);
            if (productionProcess == null)
            {
                return NotFound();
            }

            return View(productionProcess);
        }

        // POST: ProductionProcesses/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productionProcess = await _context.ProductionProcess.FindAsync(id);
            _context.ProductionProcess.Remove(productionProcess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionProcessExists(int id)
        {
            return _context.ProductionProcess.Any(e => e.ProductionProcessId == id);
        }
    }
}
