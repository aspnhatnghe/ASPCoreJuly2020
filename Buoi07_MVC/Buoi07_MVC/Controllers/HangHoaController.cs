using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buoi07_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi07_MVC.Controllers
{
    public class HangHoaController : Controller
    {
        public static List<HangHoa> dsHangHoa = new List<HangHoa>();

        public IActionResult Index()
        {
            return View(dsHangHoa);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangHoa hh)
        {
            hh.MaHh = Guid.NewGuid();
            dsHangHoa.Add(hh);
            return RedirectToAction(actionName: "Index", controllerName: "HangHoa");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var hh = dsHangHoa.SingleOrDefault(p => p.MaHh == Guid.Parse(id));

            if(hh != null)
            {
                return View(hh);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(HangHoa hangHoa)
        {
            var hh = dsHangHoa.SingleOrDefault(p => p.MaHh == hangHoa.MaHh);

            if (hh != null)
            {
                hh.TenHh = hangHoa.TenHh;
                hh.SoLuong = hangHoa.SoLuong;
                hh.DonGia = hangHoa.DonGia;
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Delete(Guid id)
        {
            var hh = dsHangHoa.SingleOrDefault(p => p.MaHh == id);
            if(hh != null)
            {
                dsHangHoa.Remove(hh);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}