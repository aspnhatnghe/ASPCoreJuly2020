using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.Helpers;
using MyStoreDbFirst.ViewModels;

namespace MyStoreDbFirst.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly eStore20Context _context;
        private readonly IMapper _mapper;

        public HangHoasController(eStore20Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XuLyTimKiem(string TuKhoa, double? GiaTu, double? GiaDen)
        {
            var dsHangHoa = _context.HangHoa.AsQueryable();
            if(!string.IsNullOrEmpty(TuKhoa))
            {
                dsHangHoa = dsHangHoa.Where(hh =>hh.TenHh.Contains(TuKhoa));
            }
            if(GiaTu.HasValue)
            {
                dsHangHoa = dsHangHoa.Where(hh => hh.DonGia.Value >= GiaTu);
            }
            if (GiaDen.HasValue)
            {
                dsHangHoa = dsHangHoa.Where(hh => hh.DonGia.Value <= GiaDen);
            }

            var data = _mapper.Map<List<HangHoaTimKiem>>(dsHangHoa.ToList());

            return View("TimKiem", data);
        }

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            /*
            var eStore20Context = _context.HangHoa
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation);

            return View(await eStore20Context.ToListAsync());
            */

            //IQueryable - lưu cấu trúc
            var dsHangHoa = _context.HangHoa
                .Select(hh => new HangHoaViewModel { 
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    DonGia = hh.DonGia.Value,
                    Hinh = hh.Hinh,
                    Loai = hh.MaLoaiNavigation.TenLoai,
                    NhaCungCap = hh.MaNccNavigation.TenCongTy
                });

            return View("HangHoaView", await dsHangHoa.ToListAsync());

            //var mang = new List<int> { 1, 3, 4, 5 };
            //var sole = mang.Select(s => s % 2);
        }

        // GET: HangHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: HangHoas/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHh,TenHh,MaLoai,MoTaDonVi,DonGia,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                hangHoa.Hinh = FileHelper.UploadFileToFolder(Hinh, "HangHoa");
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,MaLoai,MoTaDonVi,DonGia,Hinh,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa, IFormFile myFile)
        {
            if (id != hangHoa.MaHh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(myFile != null)
                    {
                        hangHoa.Hinh = FileHelper.UploadFileToFolder(myFile, "HangHoa");
                    }
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoa.FindAsync(id);
            _context.HangHoa.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoa.Any(e => e.MaHh == id);
        }
    }
}

