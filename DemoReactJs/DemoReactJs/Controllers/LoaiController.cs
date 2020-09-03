using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoReactJs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoReactJs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaiController(MyDbContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IEnumerable<Loai> GetAll()
        {
            return _context.Loais.ToList();
        }
    }
}