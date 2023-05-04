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

            // Query for Status codes
        var statusCode = _context.Statuses.ToList();
        ViewBag.Statuses = new SelectList(statusCode, "Status_code", "Status_desc");


	    var data = new JoinData()
	    {
			// Pulling shared data from the JoinData model
		    Items = await items.ToListAsync(),
		    Kits = await kits.ToListAsync()
	    };

        return View(data);
    }

    }

    
}
