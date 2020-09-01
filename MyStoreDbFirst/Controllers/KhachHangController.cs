using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.Models;
using MyStoreDbFirst.ViewModels;

namespace MyStoreDbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly eStore20Context _context;
        private readonly IConfiguration _config;
        public KhachHangController(eStore20Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //api/KhachHang/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult SignIn(LoginVM model)
        {
            var kh = _context.KhachHang.SingleOrDefault(kh => kh.MaKh == model.UserName && kh.MatKhau == model.Password);
            if(kh == null)
            {
                return Ok(new ApiResultModel { 
                    Success = false,
                    Message = "Invalid username/password"
                });
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, kh.HoTen),
                new Claim(ClaimTypes.Email, kh.Email),
                new Claim("MaKH", kh.MaKh)
            };

            var secretKey = _config["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenInfo = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenInfo);

            return Ok(new ApiResultModel { 
                Success = true,
                Data = tokenHandler.WriteToken(token)
            });
        }

        // GET: api/KhachHang
        [HttpGet]
        [Authorize]
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
