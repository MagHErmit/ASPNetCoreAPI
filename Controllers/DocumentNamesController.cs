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
    public class DocumentNamesController : Controller
    {
        private readonly xismhdqwContext _context;

        public DocumentNamesController(xismhdqwContext context)
        {
            _context = context;
        }

        // GET: DocumentNames
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentName.ToListAsync());
        }

        // GET: DocumentNames/Details/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentName = await _context.DocumentName
                .FirstOrDefaultAsync(m => m.DocumentNameId == id);
            if (documentName == null)
            {
                return NotFound();
            }

            return View(documentName);
        }

        // GET: DocumentNames/Create
        [Authorize(Roles = "2")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentNameId,DocumentName1")] DocumentName documentName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentName);
        }

        // GET: DocumentNames/Edit/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentName = await _context.DocumentName.FindAsync(id);
            if (documentName == null)
            {
                return NotFound();
            }
            return View(documentName);
        }

        // POST: DocumentNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentNameId,DocumentName1")] DocumentName documentName)
        {
            if (id != documentName.DocumentNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentNameExists(documentName.DocumentNameId))
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
            return View(documentName);
        }

        // GET: DocumentNames/Delete/5
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentName = await _context.DocumentName
                .FirstOrDefaultAsync(m => m.DocumentNameId == id);
            if (documentName == null)
            {
                return NotFound();
            }

            return View(documentName);
        }

        // POST: DocumentNames/Delete/5
        [Authorize(Roles = "2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentName = await _context.DocumentName.FindAsync(id);
            _context.DocumentName.Remove(documentName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentNameExists(int id)
        {
            return _context.DocumentName.Any(e => e.DocumentNameId == id);
        }
    }
}
