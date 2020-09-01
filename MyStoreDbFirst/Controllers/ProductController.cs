using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStoreDbFirst.Entities;
using SQLitePCL;

namespace MyStoreDbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly eStore20Context _context;

        public ProductController(eStore20Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> LayTatCa()
        {
            return await _context.HangHoa.ToListAsync();
        }

        [HttpGet("{id}")]
        public IActionResult LayMotHangHoa(int id)
        {
            var hh = _context.HangHoa.SingleOrDefault(hh => hh.MaHh == id);
            if (hh == null) return BadRequest();
            return Ok(hh);
        }
    }
}