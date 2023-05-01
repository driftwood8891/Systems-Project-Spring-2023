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
    public class Kit_TypeController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly LogFileHelper _logFileHelper;

		public Kit_TypeController(ApplicationDbContext context, IWebHostEnvironment env, LogFileHelper logFileHelper)
		{
			_context = context;
			_env = env;
			_logFileHelper = logFileHelper;
		}

		// GET: Kit_Type
		public async Task<IActionResult> Index()
        {
              return _context.Kit_types != null ? 
                          View(await _context.Kit_types.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kit_types'  is null.");
		}

		// GET: Kit_Type/Create
        [Authorize(Roles = "Admin,Assistant")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kit_Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant")]
        public async Task<IActionResult> Create([Bind("Kt_id,Kt_name,Kt_desc,Kt_cost,Kt_date")] Kit_Type kit_Type)
        {
			// Check if a kit type with the same name already exists in the database
			if (_context.Kit_types.Any(x => x.Kt_name == kit_Type.Kt_name))
			{
				ModelState.AddModelError("Kt_name", "A kit type with the same name already exists.");
			}

			if (ModelState.IsValid)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Create", $"Created Kit Type '{kit_Type.Kt_name}'");                                    // Flush the StreamWriter to make sure the entry is written to the file

                _context.Add(kit_Type);
                await _context.SaveChangesAsync();

                // Create Alert
                TempData["success"] = "Kit Type was created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(kit_Type);
        }

        // GET: Kit_Type/Edit/5
        [Authorize(Roles = "Admin,Assistant")]
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
        [Authorize(Roles = "Admin,Assistant")]
        public async Task<IActionResult> Edit(string id, [Bind("Kt_id,Kt_name,Kt_desc,Kt_cost,Kt_date,Item_id")] Kit_Type kit_Type)
        {
            if (id != kit_Type.Kt_id)
            {
                return NotFound();
            }

			// Check if a kit type with the same name already exists in the database
			if (_context.Kit_types.Any(x => x.Kt_name == kit_Type.Kt_name && x.Kt_id != kit_Type.Kt_id))
			{
				ModelState.AddModelError("Kt_name", "A kit type with the same name already exists.");
			}

			if (ModelState.IsValid)
            {
                try
                {
					// Code that generates a report in the log.txt file 
					_logFileHelper.LogEvent("Edit", $"Edited Kit Type '{kit_Type.Kt_name}'");

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

                //Edit Alert
                TempData["success"] = "Kit Type was edited successfully";

                return RedirectToAction(nameof(Index));
            }


            return View(kit_Type);
        }

        // GET: Kit_Type/Delete/5
        [Authorize(Roles = "Admin,Assistant")]
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
        [Authorize(Roles = "Admin,Assistant")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Kit_types == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kit_types'  is null.");
            }
            var kit_Type = await _context.Kit_types.FindAsync(id);
            if (kit_Type != null)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Delete", $"Deleted Kit Type '{kit_Type.Kt_name}'");

				_context.Kit_types.Remove(kit_Type);
            }
            
            await _context.SaveChangesAsync();

            //Delete Alert
            TempData["success"] = "Kit Type was deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool Kit_TypeExists(string id)
        {
          return (_context.Kit_types?.Any(e => e.Kt_id == id)).GetValueOrDefault();
        }
    }
}
