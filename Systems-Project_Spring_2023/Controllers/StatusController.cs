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
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StatusController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
              return _context.Statuses != null ? 
                          View(await _context.Statuses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Statuses'  is null.");
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Status_code == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status_code,Status_desc")] Status status)
        {
            if (ModelState.IsValid)
            {
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Created|Created Status Code'{status.Status_code}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Status_code,Status_desc")] Status status)
        {
            if (id != status.Status_code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                    var logEntry = $"Edited|Edited Status Code'{status.Status_code}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                    using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                    using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                    streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                    streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.Status_code))
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
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Statuses == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Status_code == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Statuses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Statuses'  is null.");
            }
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Deleted|Deleted Status Code'{status.Status_code}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                _context.Statuses.Remove(status);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(string id)
        {
          return (_context.Statuses?.Any(e => e.Status_code == id)).GetValueOrDefault();
        }
    }
}
