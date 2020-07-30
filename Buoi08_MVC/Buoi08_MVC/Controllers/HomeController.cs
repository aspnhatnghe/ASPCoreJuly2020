using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Buoi08_MVC.Models;

namespace Buoi08_MVC.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ABC()
        {
            ViewBag.TenTrungTam = "Nhất Nghệ";
            ViewData["Email"] = "info@nhatnghe.com";
            ViewData["so dien thoai"] = 989366785;
            return View();
        }

        public IActionResult Demo()
        {
            ViewBag.ABC = "ABC XYZ";
            TempData["HoTen"] = "Nhất Nghệ";

            return RedirectToAction("Show", "Home");
        }

        public IActionResult Show()
        {
            var abc = ViewBag.ABC;
            var hoTen = TempData["HoTen"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
