using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buoi10_Validation_Layout.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buoi10_Validation_Layout.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Employee employee)
        {
            return View();
        }

        public IActionResult CheckEmployeeExist(string EmployeeNo)
        {
            var accounts = new List<string>()
            {
                "admin", "guest", "abc"
            };

            if(accounts.Contains(EmployeeNo))
            {
                return Json("Mã này đã có");
            }

            return Json(true);
        }

        
        public IActionResult CheckSecurityCode(string MaBaoMat)
        {
            if(MaBaoMat == HttpContext.Session.GetString("SecurityCode"))
            {
                return Content("true");
            }
            return Content("false");
        }

        public IActionResult Register()
        {
            var rd = new Random();
            var pattern = @"0123456789qazwsxedcrfvtgbyhnujmiklop[]~!@#$%^&*()_+|";
            var randomCode = new StringBuilder();
            for(var i = 0; i < 6; i++)
            {
                randomCode.Append(pattern[rd.Next(0, pattern.Length)]);
            }            
            ViewBag.RandomCode = randomCode.ToString();
            HttpContext.Session.SetString("SecurityCode", randomCode.ToString());
            return View();
        }

        public IActionResult NoView()
        {
            return View();
        }

        [HttpGet("/Product")]
        public IActionResult ShowProduct()
        {
            return View();
        }
    }
}