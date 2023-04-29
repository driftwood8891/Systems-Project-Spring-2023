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
    
    public class KitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public KitsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Kits
        public async Task<IActionResult> Index()
        {
            /*var kitTypes = _context.Kit_types.Select(k => new { k.Kt_id, k.Kt_name }).ToList();
            ViewBag.kitType = kitTypes.ToDictionary(k => k.Kt_id, k => k.Kt_name);*/


            return _context.Kits != null ? 
                          View(await _context.Kits.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kits'  is null.");
        }

        // GET: Kits/Create
        public IActionResult Create()
        {
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");

            // This is code for creating a dropdown box for the Kit types
            var kitType = _context.Kit_types.ToList();
            ViewBag.Kit_types = new SelectList(kitType, "Kt_id", "Kt_name");

            // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
            var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

            ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");

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
                // Find the Kit_cost based on the Kt_id
                var kitType = await _context.Kit_types.FindAsync(kit.Kt_id);
                var kitCost = kitType.Kt_cost;
                var kitDesc = kitType.Kt_desc;

                // Set the Kit_cost to the Kit_Item_cost
                kit.Kit_cost = kitCost;
                kit.Kit_desc = kitDesc;

                _context.Add(kit);
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Created|Created Kit'{kit.Kit_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                await _context.SaveChangesAsync();

                //Create Alert
                TempData["success"] = "Kit was created successfully";

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

            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");

            // This is code for creating a dropdown box for the Kit types
            var kitType = _context.Kit_types.ToList();
            ViewBag.Kit_types = new SelectList(kitType, "Kt_id", "Kt_name");

            // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
            var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

            ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");


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

                    var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                    var logEntry = $"Edited|Edited Kit'{kit.Kit_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                    using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                    using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                    streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                    streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
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

                //Edit Alert
                TempData["success"] = "Kit was edited successfully";

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
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Deleted|Deleted Kit'{kit.Kit_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file
                _context.Kits.Remove(kit);
            }
            
            await _context.SaveChangesAsync();

            //Delete Alert
            TempData["success"] = "Kit was deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool KitExists(string id)
        {
          return (_context.Kits?.Any(e => e.Kit_id == id)).GetValueOrDefault();
        }
    }
}
