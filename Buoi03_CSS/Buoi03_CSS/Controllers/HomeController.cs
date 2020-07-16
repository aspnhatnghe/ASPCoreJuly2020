using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Buoi03_CSS.Models;

namespace Buoi03_CSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult SanPham()
        {
            Random rd = new Random();
            int soLuong = rd.Next(4, 100);
            List<HangHoa> dsHangHoa = new List<HangHoa>();

            for (int i = 0; i < soLuong; i++)
            {
                dsHangHoa.Add(new HangHoa { 
                    TenHH = $"Samsung {rd.Next()}",
                    DonGia = rd.Next(1000, 10000),
                    GiamGia = rd.NextDouble()
                });
            }

            return View(dsHangHoa);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
