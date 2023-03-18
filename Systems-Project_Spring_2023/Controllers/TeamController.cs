using Microsoft.AspNetCore.Mvc;

namespace Systems_Project_Spring_2023.Controllers
{
	public class TeamController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult NathanCochran()
		{
			return View();
		}
	}
}
