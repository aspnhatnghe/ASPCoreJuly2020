using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buoi14_ADONET.Services;
using Microsoft.AspNetCore.Mvc;

namespace Buoi14_ADONET.Controllers
{
    public class DemoDIController : Controller
    {
        private readonly IDemo demo;
        private readonly IDemoSingleton demoSingleton1;
        private readonly IDemoSingleton demoSingleton2;
        private readonly IDemoTransient demoTransient1;
        private readonly IDemoTransient demoTransient2;
        private readonly IDemoScoped demoScoped1;
        private readonly IDemoScoped demoScoped2;

        public DemoDIController(IDemo _demo, IDemoScoped scoped1, IDemoScoped scoped2, IDemoTransient transient1, IDemoTransient transient2, IDemoSingleton singleton1, IDemoSingleton singleton2)
        {
            demo = _demo;
            demoScoped1 = scoped1; demoScoped2 = scoped2;
            demoSingleton1 = singleton1; 
            demoSingleton2 = singleton2;
            demoTransient1 = transient1;
            demoTransient2 = transient2;
        }

        public IActionResult Index()
        {
            //return Json(demo.GetGuid());
            return Json(new { 
                scope1 = demoScoped1.GetGuid(),
                scope2 = demoScoped2.GetGuid(),
                tran1 = demoTransient1.GetGuid(),
                tran2 = demoTransient2.GetGuid(),
                sing1 = demoSingleton1.GetGuid(),
                sing2 = demoSingleton2.GetGuid()
            });
        }

        public IActionResult Index2()
        {
            //return Json(demo.GetGuid());
            return Json(new
            {
                scope1 = demoScoped1.GetGuid(),
                scope2 = demoScoped2.GetGuid(),
                tran1 = demoTransient1.GetGuid(),
                tran2 = demoTransient2.GetGuid(),
                sing1 = demoSingleton1.GetGuid(),
                sing2 = demoSingleton2.GetGuid()
            });
        }
    }
}