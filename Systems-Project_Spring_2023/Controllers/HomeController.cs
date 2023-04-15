using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Systems_Project_Spring_2023.Models;
using System.Web;

namespace Systems_Project_Spring_2023.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
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
            return View();
        }

        public IActionResult CheckIn()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Docs()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Reports()
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