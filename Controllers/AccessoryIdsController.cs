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
    public class AccessoryIdsController : Controller
    {
        private readonly xismhdqwContext _context;

        public AccessoryIdsController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: AccessoryIds
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccessoryId.ToListAsync());
        }

        // GET: AccessoryIds/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessoryId = await _context.AccessoryId
                .FirstOrDefaultAsync(m => m.AccessoryId1 == id);
            if (accessoryId == null)
            {
                return NotFound();
            }

            return View(accessoryId);
        }

        // GET: AccessoryIds/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessoryIds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessoryId1,AccName,DescriptionOfAccessory,Price,Vat,Inventory,PartnerId")] AccessoryId accessoryId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessoryId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessoryId);
        }

        // GET: AccessoryIds/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessoryId = await _context.AccessoryId.FindAsync(id);
            if (accessoryId == null)
            {
                return NotFound();
            }
            return View(accessoryId);
        }

        // POST: AccessoryIds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessoryId1,AccName,DescriptionOfAccessory,Price,Vat,Inventory,PartnerId")] AccessoryId accessoryId)
        {
            if (id != accessoryId.AccessoryId1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessoryId);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoryIdExists(accessoryId.AccessoryId1))
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
            return View(accessoryId);
        }

        // GET: AccessoryIds/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessoryId = await _context.AccessoryId
                .FirstOrDefaultAsync(m => m.AccessoryId1 == id);
            if (accessoryId == null)
            {
                return NotFound();
            }

            return View(accessoryId);
        }

        // POST: AccessoryIds/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessoryId = await _context.AccessoryId.FindAsync(id);
            _context.AccessoryId.Remove(accessoryId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoryIdExists(int id)
        {
            return _context.AccessoryId.Any(e => e.AccessoryId1 == id);
        }
    }
}
