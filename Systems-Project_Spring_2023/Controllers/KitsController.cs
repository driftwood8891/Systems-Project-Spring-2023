using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;
using Systems_Project_Spring_2023.Models;

namespace Systems_Project_Spring_2023.Controllers
{
    [Authorize(Roles = "Assistant")]
    public class KitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kits
        public async Task<IActionResult> Index()
        {
              return _context.Kits != null ? 
                          View(await _context.Kits.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kits'  is null.");
        }

        // GET: Kits/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Kits == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits
                .FirstOrDefaultAsync(m => m.Kit_id == id);
            if (kit == null)
            {
                return NotFound();
            }

            return View(kit);
        }

        // GET: Kits/Create
        public IActionResult Create()
        {
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");

            return View();
        }

        // POST: Kits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kit_id,Kit_barcd,Kit_name,Kit_qty,Kit_desc,Kit_cost,Kit_date,Kit_note,Kt_id,Status_code,Student_macid")] Kit kit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kit);
        }

        // GET: Kits/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Kits == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits.FindAsync(id);
            if (kit == null)
            {
                return NotFound();
            }
            return View(kit);
        }

        // POST: Kits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Kit_id,Kit_barcd,Kit_name,Kit_qty,Kit_desc,Kit_cost,Kit_date,Kit_note,Kt_id,Status_code,Student_macid")] Kit kit)
        {
            if (id != kit.Kit_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitExists(kit.Kit_id))
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
            return View(kit);
        }

        // GET: Kits/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Kits == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits
                .FirstOrDefaultAsync(m => m.Kit_id == id);
            if (kit == null)
            {
                return NotFound();
            }

            return View(kit);
        }

        // POST: Kits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Kits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kits'  is null.");
            }
            var kit = await _context.Kits.FindAsync(id);
            if (kit != null)
            {
                _context.Kits.Remove(kit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitExists(string id)
        {
          return (_context.Kits?.Any(e => e.Kit_id == id)).GetValueOrDefault();
        }
    }
}
