using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buoi09_Validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi09_Validation.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Employee emp)
        {   

            return View();
        }
    }
}