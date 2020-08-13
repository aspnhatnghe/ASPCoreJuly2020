using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCore.ADONETLab.Models;
using Buoi14_ADONET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi14_ADONET.Controllers
{
    public class LoaiController : Controller
    {
        public IActionResult Index()
        {
            return View(LoaiDataAccessLayer.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Loai loai)
        {
            var maLoai = LoaiDataAccessLayer.AddLoai(loai);
            if (!maLoai.HasValue)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}