using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;
using Systems_Project_Spring_2023.Models;

namespace Systems_Project_Spring_2023.Controllers
{
    public class Kit_TypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Kit_TypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kit_Type
        public async Task<IActionResult> Index()
        {
              return _context.Kit_types != null ? 
                          View(await _context.Kit_types.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kit_types'  is null.");
        }

        // GET: Kit_Type/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Kit_types == null)
            {
                return NotFound();
            }

            var kit_Type = await _context.Kit_types
                .FirstOrDefaultAsync(m => m.Kt_id == id);
            if (kit_Type == null)
            {
                return NotFound();
            }

            return View(kit_Type);
        }

        // GET: Kit_Type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kit_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kt_id,Kt_item_name,Kt_item_qty,Kt_item_cost,Kt_date,Item_id")] Kit_Type kit_Type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kit_Type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kit_Type);
        }

        // GET: Kit_Type/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Kit_types == null)
            {
                return NotFound();
            }

            var kit_Type = await _context.Kit_types.FindAsync(id);
            if (kit_Type == null)
            {
                return NotFound();
            }
            return View(kit_Type);
        }

        // POST: Kit_Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Kt_id,Kt_item_name,Kt_item_qty,Kt_item_cost,Kt_date,Item_id")] Kit_Type kit_Type)
        {
            if (id != kit_Type.Kt_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kit_Type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Kit_TypeExists(kit_Type.Kt_id))
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
            return View(kit_Type);
        }

        // GET: Kit_Type/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Kit_types == null)
            {
                return NotFound();
            }

            var kit_Type = await _context.Kit_types
                .FirstOrDefaultAsync(m => m.Kt_id == id);
            if (kit_Type == null)
            {
                return NotFound();
            }

            return View(kit_Type);
        }

        // POST: Kit_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Kit_types == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kit_types'  is null.");
            }
            var kit_Type = await _context.Kit_types.FindAsync(id);
            if (kit_Type != null)
            {
                _context.Kit_types.Remove(kit_Type);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Kit_TypeExists(string id)
        {
          return (_context.Kit_types?.Any(e => e.Kt_id == id)).GetValueOrDefault();
        }
    }
}
