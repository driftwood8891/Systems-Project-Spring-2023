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
        private readonly IWebHostEnvironment _env;

        public LabAssistantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LabAssistants
        public async Task<IActionResult> Index()
        {
              return _context.LabAssistant != null ? 
                          View(await _context.LabAssistant.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LabAssistant'  is null.");
        }

        // GET: LabAssistants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabAssistant == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistant
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

                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Created|Created Lab Assistant'{labAssistant.La_fname}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labAssistant);
        }

        // GET: LabAssistants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabAssistant == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistant.FindAsync(id);
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
                    var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                    var logEntry = $"Edited|Edited Lab Assistant'{labAssistant.La_fname}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                    using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                    using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                    streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                    streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
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
            if (id == null || _context.LabAssistant == null)
            {
                return NotFound();
            }

            var labAssistant = await _context.LabAssistant
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
            if (_context.LabAssistant == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LabAssistant'  is null.");
            }
            var labAssistant = await _context.LabAssistant.FindAsync(id);
            if (labAssistant != null)
            {
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Deleted|Deleted Lab Assistant'{labAssistant.La_fname}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                _context.LabAssistant.Remove(labAssistant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabAssistantExists(int id)
        {
          return (_context.LabAssistant?.Any(e => e.La_id == id)).GetValueOrDefault();
        }
    }
}
