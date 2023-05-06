using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;
using Systems_Project_Spring_2023.Models;

namespace Systems_Project_Spring_2023.Controllers
{
    public class StudentsController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly LogFileHelper _logFileHelper;

		public StudentsController(ApplicationDbContext context, IWebHostEnvironment env, LogFileHelper logFileHelper)
		{
			_context = context;
			_env = env;
			_logFileHelper = logFileHelper;
		}

		// GET: Students
		public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Create
        
        public IActionResult Create()
        {
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var location = _context.Location.ToList();
            ViewBag.Location = new SelectList(location, "loc_name", "loc_name");


            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Student_macid,Student_fname,Student_lname,Student_cmail,Student_pmail,Student_phone,Student_ephone,Student_addr,Student_cour,Student_camp,Student_instr")] Student student)
        {

            if (ModelState.IsValid)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Create", $"Created Student '{student.Student_fname}'");

				_context.Add(student);
                await _context.SaveChangesAsync();

                //Create Alert
                TempData["success"] = "Student was created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
       
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var location = _context.Location.ToList();
            ViewBag.Location = new SelectList(location, "loc_name", "loc_name");

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(string id, [Bind("Student_macid,Student_fname,Student_lname,Student_cmail,Student_pmail,Student_phone,Student_ephone,Student_addr,Student_cour,Student_camp,Student_instr")] Student student)
        {
            if (id != student.Student_macid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					// Code that generates a report in the log.txt file 
					_logFileHelper.LogEvent("Edit", $"Edited Student '{student.Student_fname}'");

					_context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Student_macid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Edit Alert
                TempData["success"] = "Student was edited successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
       
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Student_macid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Delete", $"Deleted Student '{student.Student_fname}'");

				_context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();

            //Delete Alert
            TempData["success"] = "Student was deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
          return (_context.Students?.Any(e => e.Student_macid == id)).GetValueOrDefault();
        }
    }
}
