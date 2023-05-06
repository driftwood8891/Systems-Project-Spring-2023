﻿using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Systems_Project_Spring_2023.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Systems_Project_Spring_2023.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Systems_Project_Spring_2023.Controllers
{
	//[Authorize(Roles = "Student")]
	public class HomeController : Controller
	{
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly LogFileHelper _logFileHelper;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IWebHostEnvironment env, LogFileHelper logFileHelper)
		{
			_context = context;
			_logger = logger;
			_env = env;
            _logFileHelper = logFileHelper;
        }

        public IActionResult Index()
		{
            if (User.Identity.IsAuthenticated)
            {
                // User is logged in, return the user's profile
                var logItems = _logFileHelper.GetLogItems("log.txt");
                return View("UserProfile", logItems);
            }
            else
            {
                // User is not logged in, return a welcome screen
                return View("Welcome");
            }
		}

		public IActionResult Privacy()
		{
			return View();
		}

        // Listing both items and kits in the same list
        public IActionResult InventoryManagement()
        {


            var itemKits = new List<ItemKit>();
            var kits = _context.Kits.ToList();
            var items = _context.Items.ToList();
            var kitTypes = _context.Kit_types.ToList();

            var statusCodes = _context.Statuses.Select(k => new { k.Status_code, k.Status_desc }).ToList().ToDictionary(k => k.Status_code, k => k.Status_desc);

            foreach (var kit in kits)
            {
                var kitType = kitTypes.FirstOrDefault(k => k.Kt_id == kit.Kt_id);
                
                itemKits.Add(new ItemKit
                {
                    Item_Kit_Barcode = kit.Kit_barcd,
                    Item_Kit_Name = kit.Kit_name,
                    Item_Kit_Cost = kit.Kit_cost,
                    Item_Kit_Note = kit.Kit_note,
                    Item_Kit_ID = "kit",
                    Item_Kit_Type = kitType.Kt_name,
                    Status_code = statusCodes[kit.Status_code],
                    Student_macid = kit.Student_macid
                });
            }

            foreach (var item in items)
            {
                itemKits.Add(new ItemKit
                {
                    Item_Kit_Barcode = item.Item_barcode,
                    Item_Kit_Name = item.Item_name,
                    Item_Kit_Cost = item.Item_cost,
                    Item_Kit_Note = item.Item_note,
                    Item_Kit_ID = "item",
                    Item_Kit_Type = item.Item_type?.ToString(),
                    Status_code = statusCodes[item.Status_code],
                    Student_macid = item.Student_macid
                });
            }

            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            var statusCode = _context.Statuses.ToList();
            // This is code for creating a dropdown box for the status codes(Pulls descriptions from database).
            ViewBag.Statuses = new SelectList(statusCode, "Status_desc", "Status_desc");


            return View(itemKits);
        }





        // GET - For Checkout view 
        public IActionResult Checkout()
        {
            // This string allows us to filter multiple status codes
            string[] checkinCodes = new[] { "1", "8" };

	        var viewModel = new JoinData();
			
	        // list dropdown boxes
            viewModel.Kits = _context.Kits.Where(k => checkinCodes.Contains(k.Status_code)).ToList();
	        viewModel.Students = _context.Students.ToList();
            viewModel.Items = _context.Items.Where(i => checkinCodes.Contains(i.Status_code)).ToList();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(string checkOutOption, string kitName, string studentName, string itemName)
        {
            // Reference students, items, and kits by their names
	        var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
	        var student = _context.Students.FirstOrDefault(s => s.Student_macid == studentName);
	        var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);

            // If else setup for radio button
	        if (checkOutOption == "kit" && kit != null && student != null)
	        {
		        // use the selected Kit object and its properties
		        kit.Status_code = "2";
		        kit.Student_macid = student.Student_macid;

				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Checked Out", $"Checked Out '{kit.Kit_name}'");
                
                _context.SaveChanges();
				TempData["success"] = "Kit successfully checked out"; // create alert
				return RedirectToAction("InventoryManagement");
			}
	        else if (checkOutOption == "item" && item != null && student != null)
	        {
		        // use the selected Item object and its properties
		        item.Status_code = "2";
		        item.Student_macid = student.Student_macid;

				// Code that generates a report in the log.txt file 
				_logFileHelper.LogEvent("Checked Out", $"Checked Out '{item.Item_name}'");

                _context.SaveChanges();
				TempData["success"] = "Item successfully checked out"; // create alert
				return RedirectToAction("InventoryManagement");
			}
	        else
	        {
		        // Set an error message to display in the view
		        TempData["ErrorMessage"] = "No matching kit, student, or item found.";
		        return RedirectToAction("Checkout");
	        }
        }


        // GET - For CheckIn view
        public IActionResult Checkin()
        {
            var viewModel = new JoinData();
			
	        // Display data within dropdown boxes and Filtering by status code
            viewModel.Kits = _context.Kits.Where(k => k.Status_code == "2").ToList();
            viewModel.Items = _context.Items.Where(i => i.Status_code == "2").ToList();
            viewModel.Locations = _context.Location.ToList();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkin(string checkInOption, string kitName, string campus, string itemName)
        {
            // Reference kits, students, and items by their names
            var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
           var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);
           var location = _context.Location.FirstOrDefault(l => l.loc_name == campus);

            if (checkInOption == "kit" && kit != null && campus != null)
            {
                // use the selected Kit object and its properties
                kit.Status_code = "1";
                kit.Student_macid = campus;

                // Code to generate logging
				_logFileHelper.LogEvent("Checked In", $"Checked In '{kit.Kit_name}'");

                _context.SaveChanges();
				TempData["success"] = "Kit successfully checked in"; // create alert
				return RedirectToAction("InventoryManagement");
			}
            else if (checkInOption == "item" && item != null && campus != null)
            {
                // use the selected Item object and its properties
                item.Status_code = "1";
                item.Student_macid = campus;

				// Code to generate logging
				_logFileHelper.LogEvent("Checked In", $"Checked In '{item.Item_name}'");

				_context.SaveChanges();
				TempData["success"] = "Item successfully checked in"; // create alert
				return RedirectToAction("InventoryManagement");
			}
            else
            {
                // Set an error message to display in the view
                TempData["ErrorMessage"] = "No matching kit, campus, or item found.";
                return RedirectToAction("Checkin");
            }
        }


        public IActionResult Docs()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            var logItems = _logFileHelper.GetLogItems("log.txt");
            return View(logItems);
        }

        public IActionResult Credit()
        {
	        return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}