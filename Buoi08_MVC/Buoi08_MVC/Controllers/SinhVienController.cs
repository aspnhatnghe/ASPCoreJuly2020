using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Buoi08_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Buoi08_MVC.Controllers
{
    public class SinhVienController : Controller
    {
        public IActionResult DemoSync()
        {
            var demo = new Demo();
            var sw = new Stopwatch();
            sw.Start();
            demo.Test01();
            demo.Test02();
            demo.Test03();
            sw.Stop();
            return Content($"Chạy hết {sw.ElapsedMilliseconds} ms");
        }

        public async Task<IActionResult> AAAA()
        {
            var demo = new Demo();
            var sw = new Stopwatch();
            sw.Start();
            var x = demo.Test01Async();
            var y = demo.Test02Async();
            var z = demo.Test03Async();
            await x; await y; await z;
            sw.Stop();
            return Content($"Chạy hết {sw.ElapsedMilliseconds} ms");
        }

        public IActionResult Index()
        {
            return View();
        }

        string textFile = "sinhvien.txt";
        string jsonFile = "sinhvien.json";
        string TextFullPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", textFile);
        string JsonFullPath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", jsonFile);
        string LogFile => Path.Combine(Directory.GetCurrentDirectory(), "log.txt");

        [HttpPost]
        public IActionResult Index(SinhVien sinhVien, string GhiFile)
        {
            if (GhiFile == "Ghi file text")
            {
                var data = new string[]
                {
                    sinhVien.MaSinhVien,
                    sinhVien.HoTen,
                    sinhVien.DiemSo.ToString()
                };
                System.IO.File.WriteAllLines(TextFullPath, data);
            }
            else if (GhiFile == "Ghi file JSON")
            {
                var jsonString = JsonConvert.SerializeObject(sinhVien);
                System.IO.File.WriteAllText(JsonFullPath, jsonString);
            }
            return View("Index");
        }

        public IActionResult DocFile(string loai)
        {
            SinhVien sv = new SinhVien();
            try
            {
                if (loai == "text")
                {
                    var data = System.IO.File.ReadAllLines(TextFullPath);
                    sv.MaSinhVien = data[0];
                    sv.HoTen = data[1];
                    if (double.TryParse(data[2], out double tmp))
                    {
                        sv.DiemSo = tmp;
                    }
                    //sv.DiemSo = double.Parse(data[2]);
                }
                else if (loai == "json")
                {
                    var jsonString = System.IO.File.ReadAllText(JsonFullPath);
                    sv = JsonConvert.DeserializeObject<SinhVien>(jsonString);
                }
            }
            catch (JsonReaderException ex)
            {
                using (var file = new StreamWriter(LogFile, true))
                {
                    file.Write(ex.Message);
                }
            }
            catch (Exception ex)
            {
            }
            return View("Index", sv);
        }
    }
}