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

        #region [Load More]
        [HttpGet]
        public IActionResult LoadMore()
        {
            return View();
        }

        const int SoSanPhamMoiTrang = 10;
        [HttpPost]
        public IActionResult LoadMore(int page = 1)
        {
            //Filter
            var data = _context.HangHoa.AsQueryable();

            var result = data.Skip((page - 1) * SoSanPhamMoiTrang).Take(SoSanPhamMoiTrang)
                .Select(hh => new { 
                    hh.MaHh, hh.TenHh,hh.Hinh, 
                    DonGia = hh.DonGia.Value
                });
            var total = data.Count();
            var pageCount = Convert.ToInt32(Math.Ceiling(1.0 * total / SoSanPhamMoiTrang));
            return Json(new { 
                data = result,
                paging = new
                {
                    total = total,
                    totalPage = pageCount,
                    currentPage = page
                }
            });
        }
        #endregion


        #region [JSON Search]
        [HttpGet]
        public IActionResult JsonSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JsonSearch(HangHoaJsonSearch model)
        {
            var data = _context.HangHoa.AsQueryable();
            if (!string.IsNullOrEmpty(model.TuKhoa))
            {
                data = data.Where(hh => hh.TenHh.Contains(model.TuKhoa));
            }
            if (model.GiaTu.HasValue) {
                data = data.Where(hh => hh.DonGia.Value >= model.GiaTu.Value);
            }
            if (model.GiaDen.HasValue)
            {
                data = data.Where(hh => hh.DonGia.Value >= model.GiaTu.Value);
            }

            var result = data.Select(hh=> new {
                hh.MaHh,
                hh.TenHh, hh.Hinh,
                DonGia = hh.DonGia.Value, 
                Loai = hh.MaLoaiNavigation.TenLoai
            }).ToList();
            return Json(result);
        }
        #endregion

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