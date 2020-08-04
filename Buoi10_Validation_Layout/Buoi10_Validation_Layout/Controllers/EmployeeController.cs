using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buoi10_Validation_Layout.Models;
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
    }
}