using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStoreDbFirst.Contants;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyStoreDbFirst.Controllers
{
    [Authorize]
    public class KhachHangController : Controller
    {
        private readonly eStore20Context _context;

        public KhachHangController(eStore20Context context)
        {
            _context = context;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            var khachHang = _context.KhachHang.SingleOrDefault(kh => kh.MaKh == model.Username && kh.MatKhau == model.Password);
            if(khachHang != null)
            {
                //kiễm tra user theo Business Rule
                //đăng nhập thành công

                //khai báo các claims (đặc trưng cho user)
                var claims = new List<Claim>() { 
                    new Claim(ClaimTypes.Name, khachHang.HoTen),
                    new Claim(ClaimTypes.Email, khachHang.Email),
                    new Claim(MyClaimTypes.MaKhachHang, khachHang.MaKh),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "Account")
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Profile", "KhachHang");
            }
            ViewBag.Loi = "Sai thông tin đăng nhập";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "KhachHang");
        }

        [Authorize(Roles ="Account")]
        public IActionResult ThongKe()
        {
            return Content("Thống kê");
        }

        [Authorize(Roles = "Logistics")]
        public IActionResult DieuPhoiHang()
        {
            return Content("Vận chuyển");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}