using Microsoft.AspNetCore.Mvc;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.ViewModels;
using System.Linq;

namespace MyStoreDbFirst.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly eStore20Context _context;

        public ThongKeController(eStore20Context ctx)
        {
            _context = ctx;
        }

        public IActionResult Index()
        {
            var data = _context.ChiTietHd
                .GroupBy(cthd => cthd.MaHhNavigation.MaLoaiNavigation.TenLoai)
                .Select(g => new ThongKeViewModel
                {
                    Loai = g.Key,
                    DoanhThu = g.Sum(cthd => cthd.SoLuong * cthd.DonGia * (1 - cthd.GiamGia)),
                    GiaNN = g.Min(cthd => cthd.DonGia),
                    GiaTB = g.Average(cthd => cthd.DonGia),
                    SoMatHangDaBan = g.Sum(cthd => cthd.SoLuong)
                });

            return Json(data.ToList());
            //return View();
        }

        public IActionResult DoanhThuKhachHang()
        {
            var data = _context.ChiTietHd
                .GroupBy(cthd => new
                {
                    MaKh = cthd.MaHdNavigation.MaKh,
                    HoTen = cthd.MaHdNavigation.MaKhNavigation.HoTen,
                    Thang = cthd.MaHdNavigation.NgayDat.Month,
                    Nam = cthd.MaHdNavigation.NgayDat.Year
                })
                .Select(g => new { 
                    MaKh = g.Key.MaKh,
                    HoTen = g.Key.HoTen,
                    NamThang = $"{g.Key.Thang}/{g.Key.Nam}",
                    DoanhThu = g.Sum(cthd => cthd.SoLuong * cthd.DonGia * (1 - cthd.GiamGia))
                });

            return Json(data.ToList());
        }

        public IActionResult DoanhThuThang()
        {
            var data = _context.ChiTietHd
                .GroupBy(cthd => cthd.MaHhNavigation.MaLoaiNavigation.TenLoai)
                .Select(g => new LoaiThongKe { 
                    Loai = g.Key,
                    DoanhThu = g.Sum(cthd => cthd.SoLuong * cthd.DonGia * (1 - cthd.GiamGia))
                }).ToList();

            return View(data);
        }
    }
}