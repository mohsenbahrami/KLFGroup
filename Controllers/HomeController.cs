using KLFGroup.Core.Interfaces;
using KLFGroup.Models;
using KLFGroup.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KLFGroup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceHandler _serviceHandler;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IServiceHandler serviceHandler,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _serviceHandler = serviceHandler;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                _serviceHandler.UseActivityUser(new Data.Entities.UserActivity()
                {
                    ActivityId = 3,
                    UserId = userId,
                    LastOccurence = DateTime.Now
                });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            string userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                _serviceHandler.UseActivityUser(new Data.Entities.UserActivity()
                {
                    ActivityId = 1,
                    UserId = userId,
                    LastOccurence = DateTime.Now
                });
            }
            return View();
        }

        public IActionResult ContactUs()
        {
            string userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                _serviceHandler.UseActivityUser(new Data.Entities.UserActivity()
                {
                    ActivityId = 2,
                    UserId = userId,
                    LastOccurence = DateTime.Now
                });
            }
            return View();
        }

        [HttpGet]
        public IActionResult ReportUserActivity()
        {

            string userId = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userId))
            {
                var rpt = _serviceHandler.GetReportUserActivity();
                return View(rpt);
            }
            else
            {
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
