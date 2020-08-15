using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buoi14_ADONET.Models;
using Buoi14_ADONET.Services;
using Microsoft.AspNetCore.Mvc;

namespace Buoi14_ADONET.Controllers
{
    public class LoaisController : Controller
    {
        private readonly ILoaiService loaiService;
        private readonly IDemoSingleton demoSingleton1;
        public LoaisController(ILoaiService _loaiService, IDemoSingleton _demoSingleton1)
        {
            loaiService = _loaiService;
            demoSingleton1 = _demoSingleton1;
        }


        public IActionResult GetGuid()
        {
            return Json(demoSingleton1.GetGuid());
        }
        public IActionResult Index()
        {
            return View("~/Views/QLLoai/Index.cshtml", loaiService.LayTatCa());
        }

        public IActionResult Edit(int id)
        {
            return View("~/Views/QLLoai/Edit.cshtml", loaiService.LayLoai(id));

        }
        [HttpPost]
        public IActionResult Edit(Loai loai)
        {
            try
            {
                loaiService.CapNhat(loai);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id = loai.MaLoai });
            }
        }
    }

}