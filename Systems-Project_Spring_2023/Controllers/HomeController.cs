using Grpc.Core;
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

		public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
		{
			_context = context;
			_logger = logger;
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

            foreach (var kit in kits)
            {
                itemKits.Add(new ItemKit
                {
                    Item_Kit_Barcode = kit.Kit_barcd,
                    Item_Kit_Name = kit.Kit_name,
                    Item_Kit_Cost = kit.Kit_cost,
                    Item_Kit_Note = kit.Kit_note,
                    Item_Kit_Type = "kit",
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
                    Item_Kit_Type = "item",
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
	        viewModel.Items = _context.Items.ToList();
	        return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkout(string kitName, string studentName, string itemName)
        {
	        var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
	        var student = _context.Students.FirstOrDefault(s => s.Student_fname == studentName);
	        var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);
	        if (kit == null && student == null && item == null)
	        {
		        // handle the case where no matching Kit is found
		        return NotFound();
	        }
	        else
	        {
		        // use the selected Kit object and its properties
		        kit.Status_code = "2";
		        kit.Student_macid = student.Student_macid;
		        // ...
		        _context.SaveChanges();
		        return RedirectToAction("Checkout");
	        }
        }

        public IActionResult Checkin()
        {
	        var viewModel = new JoinData();
			
            viewModel.Kits = _context.Kits.Where(k => k.Status_code == "2").ToList();
	        viewModel.Students = _context.Students.ToList();
	        viewModel.Items = _context.Items.ToList();
	        return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkin(string kitName, string studentName, string itemName)
        {
	        var kit = _context.Kits.FirstOrDefault(k => k.Kit_name == kitName);
	        var student = _context.Students.FirstOrDefault(s => s.Student_fname == studentName);
	        var item = _context.Items.FirstOrDefault(i => i.Item_name == itemName);
	        if (kit == null && student == null && item == null)
	        {
		        // handle the case where no matching Kit is found
		        return NotFound();
	        }
	        else
	        {
		        // use the selected Kit object and its properties
		        kit.Status_code = "1";
		        kit.Student_macid = student.Student_macid;
		        // ...
		        _context.SaveChanges();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}