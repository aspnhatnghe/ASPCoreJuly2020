using Microsoft.AspNetCore.Mvc;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.ViewModels;
using System;
using System.Linq;

namespace MyStoreDbFirst.Controllers
{
    public class AjaxController : Controller
    {
        private readonly eStore20Context _context;
        public AjaxController(eStore20Context context)
        {
            _context = context;
        }

        public IActionResult ServerTime()
        {
            return Content(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View("SearchView");
        }

        [HttpPost]
        public IActionResult Search(string Keyword)
        {
            var data = _context.HangHoa.AsQueryable();
            Keyword = Keyword.Trim();
            if(!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(hh => hh.TenHh.Contains(Keyword));
            }

            var result = data.Select(hh => new HangHoaTimKiem { 
                MaHh = hh.MaHh, TenHh = hh.TenHh,
                DonGia = hh.DonGia.Value,
                GiamGia = hh.GiamGia,
                Loai = hh.MaLoaiNavigation.TenLoai,
                NgaySanXuat = hh.NgaySx
            });
            return PartialView(result.ToList());
        }
    }
}