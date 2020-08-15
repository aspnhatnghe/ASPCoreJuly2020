using Buoi14_ADONET.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi14_ADONET.Controllers
{
    public class QLLoaiController : Controller
    {
        private readonly string chuoiKetNoi;
        public QLLoaiController(IConfiguration _config)
        {
            chuoiKetNoi = _config.GetConnectionString("Database1");
        }

        public async Task<IActionResult> Index()
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                var dsLoai = await con.QueryAsync<Loai>("SELECT * FROM Loai ORDER BY TenLoai");

                return View(dsLoai.ToList());
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                var loai = await con.QueryFirstAsync<Loai>("SELECT * FROM Loai WHERE MaLoai = @Ma", new { Ma = id });

                return View(loai);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Loai loai)
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("MaLoai", loai.MaLoai);
                parameters.Add("TenLoai", loai.TenLoai);
                parameters.Add("MoTa", loai.MoTa);
                parameters.Add("Hinh", loai.Hinh);

                int rowEffect = con.Execute("spSuaLoai", parameters, commandType: CommandType.StoredProcedure);
                if(rowEffect > 0)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new { id = loai.MaLoai});
                //return Edit(loai.MaLoai);
            }
        }
    }
}