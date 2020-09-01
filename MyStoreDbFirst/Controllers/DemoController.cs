using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyStoreDbFirst.Entities;

namespace MyStoreDbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        public static List<Loai> DanhSach { get; set; } = new List<Loai>();
        // GET: api/Demo
        [HttpGet]
        public IEnumerable<Loai> Get()
        {
            return DanhSach;
        }

        // GET: api/Demo/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var loai = DanhSach.FirstOrDefault(lo => lo.MaLoai == id);
            if (loai != null)
                return Ok(loai);
            return NotFound();
        }

        // POST: api/Demo
        [HttpPost]
        public IActionResult Post([FromBody] Loai loai)
        {
            var lo = DanhSach.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if (lo != null) return BadRequest();
            DanhSach.Add(loai);
            return Ok();
        }

        // PUT: api/Demo/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Loai loai)
        {
            if (id != loai.MaLoai)
                return BadRequest();
            var lo = DanhSach.SingleOrDefault(lo => lo.MaLoai == id);
            if (lo == null)
                return BadRequest();

            lo.TenLoai = loai.TenLoai;
            lo.MoTa = loai.MoTa;
            lo.Hinh = loai.Hinh;
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lo = DanhSach.SingleOrDefault(lo => lo.MaLoai == id);
            if (lo == null)
                return BadRequest();
            DanhSach.Remove(lo);
            return Ok();
        }
    }
}
