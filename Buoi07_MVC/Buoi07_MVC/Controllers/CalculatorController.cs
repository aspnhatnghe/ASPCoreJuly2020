using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Buoi07_MVC.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double SoThuNhat, double SoThuHai, string PhepToan)
        {
            double ketQua = 0;
            switch (PhepToan)
            {
                case "+": ketQua = SoThuNhat + SoThuHai; break;
                case "-": ketQua = SoThuNhat - SoThuHai; break;
                case "*": ketQua = SoThuNhat * SoThuHai; break;
                case "/": ketQua = SoThuNhat / SoThuHai; break;
                case "%": ketQua = SoThuNhat % SoThuHai; break;
                case "^": ketQua = Math.Pow(SoThuNhat, SoThuHai); break;
            }

            //return Content(ketQua.ToString());

            ViewBag.KetQua = ketQua;
            ViewBag.A = SoThuNhat;
            ViewBag.B = SoThuHai;
            ViewBag.PhepToan = PhepToan;
            return View("Index");
        }
    }
}