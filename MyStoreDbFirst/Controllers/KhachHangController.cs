using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStoreDbFirst.Entities;

namespace MyStoreDbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly eStore20Context _context;

        public KhachHangController(eStore20Context context)
        {
            _context = context;
        }

        // GET: api/KhachHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHang()
        {
            return await _context.KhachHang.ToListAsync();
        }

        // GET: api/KhachHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(string id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // PUT: api/KhachHang/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(string id, KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KhachHang
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            _context.KhachHang.Add(khachHang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachHang.MaKh))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKh }, khachHang);
        }

        // DELETE: api/KhachHang/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KhachHang>> DeleteKhachHang(string id)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();

            return khachHang;
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHang.Any(e => e.MaKh == id);
        }
    }
}
