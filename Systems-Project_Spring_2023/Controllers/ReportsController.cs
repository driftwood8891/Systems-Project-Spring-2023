using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Systems_Project_Spring_2023.Models;
using System.Web;
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
    }
}
