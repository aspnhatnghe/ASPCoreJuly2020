using Buoi09_Validation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace Buoi09_Validation.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private void MoveUploadedFile(IFormFile myfile, List<string> folder)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory());
            for (int i = 0; i < folder.Count; i++)
            {
                fullPath = Path.Combine(fullPath, folder[i]);
            }
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            fullPath = Path.Combine(fullPath, Path.GetFileName(myfile.FileName));

            using (var file = new FileStream(fullPath, FileMode.Create))
            {
                myfile.CopyTo(file);
            }
        }

        [HttpPost]
        public IActionResult SingleFile(IFormFile myfile)
        {
            MoveUploadedFile(myfile, new List<string> { "wwwroot", "SingleFiles" });
            /*
            var uploadedFileName = Path.GetFileName(myfile.FileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Data", uploadedFileName);

            if (myfile != null && myfile.Length > 0)
            {
                using (var file = new FileStream(fullPath, FileMode.Create))
                {
                    myfile.CopyTo(file);
                }
            }
            */
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult MultipleFile(List<IFormFile> myfile)
        {
            foreach (var file in myfile)
            {
                MoveUploadedFile(file, new List<string> { "DataFiles", "Documents" });
            }
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(EmployeeInfo emp)
        {
            if(ModelState.IsValid)
            {
                ModelState.AddModelError("loi", "Hết lỗi rồi");
            }
            else
            {
                ModelState.AddModelError("loi", "Vẫn còn lỗi");
            }
            return View();
        }
    }
}