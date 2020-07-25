using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Buoi07_MVC.Controllers
{
    public class DemoController : Controller
    {
        [Route("ABC")] // host/ABC
        public int D1()
        {
            return new Random().Next();
        }

        [Route("[controller]/XYZ")] //host/Demo/XYZ
        [Route("data/XYZ")] // host/data/xyz
        [Route("/XYZ")] //host/XYZ
        public string D2()
        {
            return $"Chuỗi : {new Random().Next()}";
        }

        [Route("Say/{ten}")]
        [Route("Hello-{ten}")]
        public string Hello(string ten)
        {
            return $"Chào bạn {ten}";
        }

        [Route("{loai}/{hanghoa}.html")]
        public string ShowProduct(string loai, string hanghoa)
        {
            return $"{loai} ==> {hanghoa}";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}