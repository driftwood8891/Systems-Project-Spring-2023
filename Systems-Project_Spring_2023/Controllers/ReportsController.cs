using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Systems_Project_Spring_2023.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_Project_Spring_2023.Data;
using Microsoft.Extensions.Logging;

namespace Systems_Project_Spring_2023.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly LogFileHelper _logFileHelper;

        public ReportsController(ApplicationDbContext context, ILogger<ReportsController> logger, LogFileHelper logFileHelper)
        {
            _context = context;
            _logger = logger;
            _logFileHelper = logFileHelper;
        }



        public IActionResult Index()
        {
            var logItems = _logFileHelper.GetLogItems("log.txt");
            return View(logItems);
        }


		// Setting up status-code based report tables
        public async Task<IActionResult> StatusReports(string status)
		{
			// Query for items
	    var items = from i in _context.Items
		    select i;

	    if (!string.IsNullOrEmpty(status) && status != "All")
	    {
		    items = items.Where(i => i.Status_code == status);
	    }
			// Query for kits
	    var kits = from k in _context.Kits
		    select k;

	    if (!string.IsNullOrEmpty(status) && status != "All")
	    {
		    kits = kits.Where(k => k.Status_code == status);
	    }


	    var data = new SharedData()
	    {
			// Pulling shared data from the SharedData model(Read-only)
		    itemdetails = await items.ToListAsync(),
		    kitdetails = await kits.ToListAsync()
	    };
			// Setting up our list of status codes
	    ViewBag.StatusCodes = new List<SelectListItem>()
	    {
		    new SelectListItem() { Text = "All", Value = "All", Selected = status == "All" },
		    new SelectListItem() { Text = "Checked-In", Value = "1", Selected = status == "1" },
		    new SelectListItem() { Text = "Checked Out", Value = "2", Selected = status == "2" },
		    new SelectListItem() { Text = "Dead", Value = "3", Selected = status == "3" },
		    new SelectListItem() { Text = "Lost", Value = "4", Selected = status == "4" },
		    new SelectListItem() { Text = "In_Transit", Value = "5", Selected = status == "5" },
		    new SelectListItem() { Text = "Needs_Repair", Value = "6", Selected = status == "6" },
		    new SelectListItem() { Text = "Pending", Value = "7", Selected = status == "7" },
		    new SelectListItem() { Text = "Ready", Value = "8", Selected = status == "8" },
		    new SelectListItem() { Text = "Unknown", Value = "9", Selected = status == "9" }
	    };

	    return View(data);
    }

    }

    
}
