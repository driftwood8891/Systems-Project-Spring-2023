using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;
using Systems_Project_Spring_2023.Models;

namespace Systems_Project_Spring_2023.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _env;

		public ItemsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
              return _context.Items != null ? 
                          View(await _context.Items.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Items'  is null.");
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Item_id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
            var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

            //ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");
            // Get list of students from database and convert to SelectListItems
            var students = _context.Students.ToList();
            var studentItems = students.Select(s => new SelectListItem
            {
                Text = s.Student_macid,
                Value = s.Student_macid
            });

            // Set ViewBag property for Student_macid with the list of SelectListItems
            ViewBag.Students = studentItems;

            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");

            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Item_id,Item_barcode,Item_name,Item_type,Item_cost,Item_date,Item_note,Status_code,Student_macid")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                // Code that generates a report in the log.txt file 
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry =  $"Created|Created Item'{item.Item_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";

                // Open the log file in append mode with write-only access
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                // Create a StreamWriter to write to the file
                using var streamWriter = new StreamWriter(fileStream);

                // Write the log entry to the file
                streamWriter.WriteLine(logEntry);

                // Flush the StreamWriter to make sure the entry is written to the file
                streamWriter.Flush();


                return RedirectToAction(nameof(Index));
            }
            /*else { 
                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                var statusCode = _context.Statuses.ToList();
                // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
                var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

                //ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");
                // Get list of students from database and convert to SelectListItems
                var students = _context.Students.ToList();
                var studentItems = students.Select(s => new SelectListItem
                {
                    Text = s.Student_macid,
                    Value = s.Student_macid
                });

                // Set ViewBag property for Student_macid with the list of SelectListItems
                ViewBag.Students = studentItems;

                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");
            }*/


            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Item_id,Item_barcode,Item_name,Item_qty,Item_cost,Item_date,Item_note,Status_code,Student_macid")] Item item)
        {
            if (id != item.Item_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();

                    // Code that generates a report in the log.txt file
                    var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                    var logEntry =  $"Edited|Edited Item'{item.Item_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";

                    // Open the log file in append mode with write-only access
                    using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                    // Create a StreamWriter to write to the file
                    using var streamWriter = new StreamWriter(fileStream);

                    // Write the log entry to the file
                    streamWriter.WriteLine(logEntry);

                    // Flush the StreamWriter to make sure the entry is written to the file
                    streamWriter.Flush();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Item_id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Item_id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            //Inserted Code for Deletion Logging
            var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
            var logEntry = $"Deleted|Deleted Item'{item.Item_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
            using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
            using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
            streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
            streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
          return (_context.Items?.Any(e => e.Item_id == id)).GetValueOrDefault();
        }
    }
}
