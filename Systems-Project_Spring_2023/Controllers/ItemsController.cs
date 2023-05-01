using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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
        private readonly LogFileHelper _logFileHelper;

        public ItemsController(ApplicationDbContext context, IWebHostEnvironment env, LogFileHelper logFileHelper)
        {
            _context = context;
            _env = env;
            _logFileHelper = logFileHelper;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var statusCodes = _context.Statuses.Select(k => new { k.Status_code, k.Status_desc }).ToList();
            ViewBag.statusCode = statusCodes.ToDictionary(k => k.Status_code, k => k.Status_desc);

            return _context.Items != null ? 
                          View(await _context.Items.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Items'  is null.");
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
            var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

            ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");

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
			// Check if an item with the same name already exists in the database
			if (_context.Items.Any(x => x.Item_name == item.Item_name))
			{
				ModelState.AddModelError("Item_name", "An item with the same name already exists.");
			}

			// Check if an item with the same barcode already exists in the database
			if (_context.Items.Any(x => x.Item_barcode == item.Item_barcode))
			{
				ModelState.AddModelError("Item_barcode", "An item with the same barcode already exists.");
			}

            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                // Code that generates a report in the log.txt file 
                _logFileHelper.LogEvent("Create", $"Created Item '{item.Item_name}'");

                //Create Alert
                TempData["success"] = "Item was created successfully";

                return RedirectToAction(nameof(Index));
            }
            else 
            {
                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                var statusCode = _context.Statuses.ToList();
                // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
                var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

                ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");

                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");
            }

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


            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
            var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

            ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");

            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");


			return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Item_id,Item_barcode,Item_name,Item_type,Item_cost,Item_date,Item_note,Status_code,Student_macid")] Item item)
        {
            if (id != item.Item_id)
            {
                return NotFound();
            }

			// Check if an item with the same name already exists in the database
			if (_context.Items.Any(x => x.Item_name == item.Item_name && x.Item_id != item.Item_id))
			{
				ModelState.AddModelError("Item_name", "An item with the same name already exists.");
			}

			// Check if an item with the same barcode already exists in the database
			if (_context.Items.Any(x => x.Item_barcode == item.Item_barcode && x.Item_id != item.Item_id))
			{
				ModelState.AddModelError("Item_barcode", "An item with the same barcode already exists.");
			}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();

                    // Code that generates a report in the log.txt file 
                    _logFileHelper.LogEvent("Edit", $"Edited Item '{item.Item_name}'");
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

                //Edit Alert
                TempData["success"] = "Item was edited successfully";

                return RedirectToAction(nameof(Index));
            }
            else 
            {
                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                var statusCode = _context.Statuses.ToList();
                // This is code for creating a dropdown box for the MACC IDs(Pulls MACC IDs from Student table).
                var maccid_room = _context.Students.Select(s => new { s.Student_macid }).ToList();

                ViewBag.Students = new SelectList(maccid_room, "Student_macid", "Student_macid");

                // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
                ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");
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


            // Code that generates a report in the log.txt file 
            _logFileHelper.LogEvent("Delete", $"Deleted Item '{item.Item_name}'");
            
            await _context.SaveChangesAsync();

            //Delete Alert
            TempData["success"] = "Item was deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
          return (_context.Items?.Any(e => e.Item_id == id)).GetValueOrDefault();
        }
    }
}
