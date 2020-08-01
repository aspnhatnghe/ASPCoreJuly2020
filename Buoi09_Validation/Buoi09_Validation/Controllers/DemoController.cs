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
            return RedirectToAction("Index");
        }
    }
}