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
    public class LabAssistantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabAssistantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LabAssistants
        public async Task<IActionResult> Index()
        {
              return _context.LabAssistants != null ? 
                          View(await _context.LabAssistants.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LabAssistants'  is null.");
        }

        // GET: LabAssistants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabAssistants == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistants
                .FirstOrDefaultAsync(m => m.La_id == id);
            if (labAssistant == null)
            {
                return NotFound();
            }

            return View(labAssistant);
        }

        // GET: LabAssistants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LabAssistants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("La_id,La_fname,La_lname,La_camp,La_sch")] LabAssistant labAssistant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labAssistant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labAssistant);
        }

        // GET: LabAssistants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabAssistants == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistants.FindAsync(id);
            if (labAssistant == null)
            {
                return NotFound();
            }
            return View(labAssistant);
        }

        // POST: LabAssistants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("La_id,La_fname,La_lname,La_camp,La_sch")] LabAssistant labAssistant)
        {
            if (id != labAssistant.La_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labAssistant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabAssistantExists(labAssistant.La_id))
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
            return View(labAssistant);
        }

        // GET: LabAssistants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabAssistants == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistants
                .FirstOrDefaultAsync(m => m.La_id == id);
            if (labAssistant == null)
            {
                return NotFound();
            }

            return View(labAssistant);
        }

        // POST: LabAssistants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LabAssistants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LabAssistants'  is null.");
            }
            var labAssistant = await _context.LabAssistants.FindAsync(id);
            if (labAssistant != null)
            {
                _context.LabAssistants.Remove(labAssistant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabAssistantExists(int id)
        {
          return (_context.LabAssistants?.Any(e => e.La_id == id)).GetValueOrDefault();
        }
    }
}
