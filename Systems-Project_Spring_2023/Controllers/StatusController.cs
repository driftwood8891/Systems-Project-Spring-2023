using System;
using System.Collections.Generic;
using System.Data;
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
    public class StatusController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly LogFileHelper _logFileHelper;

		public StatusController(ApplicationDbContext context, IWebHostEnvironment env, LogFileHelper logFileHelper)
		{
			_context = context;
			_env = env;
			_logFileHelper = logFileHelper;
		}

		// GET: Status
		public async Task<IActionResult> Index()
        {
              return _context.Statuses != null ? 
                          View(await _context.Statuses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Statuses'  is null.");
		}

		// GET: Status/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Status_code,Status_desc")] Status status)
        {
            if (ModelState.IsValid)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Create", $"Created Status Code '{status.Status_code}'");

                _context.Add(status);
                await _context.SaveChangesAsync();

                //Create Alert
                TempData["success"] = "Status was created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Status/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

					// Code that generates a report in the log.txt file 
					_logFileHelper.LogEvent("Edit", $"Edited Status Code '{status.Status_code}'");

					_context.Update(status);
                    await _context.SaveChangesAsync();

                    //Create Alert
                    TempData["success"] = "Status was edited successfully";
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Statuses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Statuses'  is null.");
            }
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Delete", $"Deleted Status Code '{status.Status_code}'");

				_context.Statuses.Remove(status);
            }
            
            await _context.SaveChangesAsync();

            //Create Alert
            TempData["success"] = "Status was deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(string id)
        {
          return (_context.Statuses?.Any(e => e.Status_code == id)).GetValueOrDefault();
        }
    }
}
