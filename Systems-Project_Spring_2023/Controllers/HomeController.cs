﻿using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Systems_Project_Spring_2023.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Systems_Project_Spring_2023.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Systems_Project_Spring_2023.Controllers
{
	//[Authorize(Roles = "Student")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

		public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IWebHostEnvironment env)
		{
			_context = context;
			_logger = logger;
			_env = env;
		}

        public IActionResult Index()
		{
            if (User.Identity.IsAuthenticated)
            {
                // User is logged in, return the user's profile
                return View("UserProfile");
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

        public IActionResult InventoryManagement()
        {

            var itemKits = new List<ItemKit>();
            var kits = _context.Kits.ToList();
            var items = _context.Items.ToList();
            var kitTypes = _context.Kit_types.ToList();

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
                    Status_code = kit.Status_code,
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
                    Status_code = item.Status_code,
                    Student_macid = item.Student_macid
                });
            }

            return View(itemKits);
        }






        public IActionResult Checkout()
        {
	        var viewModel = new JoinData();
			
	        
            viewModel.Kits = _context.Kits.Where(k => k.Status_code == "1").ToList();
	        viewModel.Students = _context.Students.ToList();
	        viewModel.Items = _context.Items.Where(i => i.Status_code == "1").ToList();
	        return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkout(string checkOutOption, string kitName, string studentName, string itemName)
        {
	        var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
	        var student = _context.Students.FirstOrDefault(s => s.Student_fname == studentName);
	        var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);
	        if (checkOutOption == "kit" && kit != null && student != null)
	        {
		        // use the selected Kit object and its properties
		        kit.Status_code = "2";
		        kit.Student_macid = student.Student_macid;

                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Checked Out|Kit Checked Out'{kit.Kit_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

                _context.SaveChanges();
		        TempData["ErrorMessage"] = "Kits are checked out.";
		        return RedirectToAction("Checkout");
	        }
	        else if (checkOutOption == "item" && item != null && student != null)
	        {
		        // use the selected Item object and its properties
		        item.Status_code = "2";
		        item.Student_macid = student.Student_macid;

                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Checked Out |Item Checked Out'{item.Item_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

                _context.SaveChanges();
		        TempData["ErrorMessage"] = "Items are checked out.";
		        return RedirectToAction("Checkout");
	        }
	        else
	        {
		        // Set an error message to display in the view
		        TempData["ErrorMessage"] = "No matching kit, student, or item found.";
		        return RedirectToAction("Checkout");
	        }
        }



        public IActionResult Checkin()
        {
            var viewModel = new JoinData();
			
	        // Display data within dropdown boxes and Filtering by status code
            viewModel.Kits = _context.Kits.Where(k => k.Status_code == "2").ToList();
            viewModel.Students = _context.Students.ToList();
            viewModel.Items = _context.Items.Where(i => i.Status_code == "2").ToList();
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkin(string checkInOption, string kitName, string studentName, string itemName)
        {
            var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
            var student = _context.Students.FirstOrDefault(s => s.Student_fname == studentName);
            var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);
            if (checkInOption == "kit" && kit != null && student != null)
            {
                // use the selected Kit object and its properties
                kit.Status_code = "1";
                kit.Student_macid = student.Student_macid;

                // Adding logging to CheckIn
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Checked In |Kit Checked In'{kit.Kit_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

                _context.SaveChanges();
                TempData["ErrorMessage"] = "Kits are checked out.";
                return RedirectToAction("Checkin");
            }
            else if (checkInOption == "item" && item != null && student != null)
            {
                // use the selected Item object and its properties
                item.Status_code = "1";
                item.Student_macid = student.Student_macid;

                // Adding logging to CheckIn
                var logFilePath = Path.Combine(_env.WebRootPath, "LogFile", "log.txt");
                var logEntry = $"Checked In |Item Checked In'{item.Item_name}'|{DateTime.Now.ToString()}{Environment.NewLine}";
                using var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);     // Open the log file in append mode with write-only access
                using var streamWriter = new StreamWriter(fileStream);                                                          // Create a StreamWriter to write to the file
                streamWriter.WriteLine(logEntry);                                                                               // Write the log entry to the file
                streamWriter.Flush();                                                                                           // Flush the StreamWriter to make sure the entry is written to the file

                _context.SaveChanges();
                TempData["ErrorMessage"] = "Items are checked out.";
                return RedirectToAction("Checkin");
            }
            else
            {
                // Set an error message to display in the view
                TempData["ErrorMessage"] = "No matching kit, student, or item found.";
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
            return View();
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