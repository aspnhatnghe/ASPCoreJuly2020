using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;

namespace Buoi14_ADONET.Controllers
{
    public class DemoController : Controller
    {
        public DemoController(IConfiguration configuration)
        {
            var chuoiKetNoi = configuration.GetConnectionString("Database1");
        }

        string chuoiKetNoi = @"Data Source=.;Initial Catalog=eStore20;Integrated Security=True";
        public IActionResult GetData()
        {
            SqlConnection connection = new SqlConnection(chuoiKetNoi);

            var sql = "SELECT MaHH, TenHH, DonGia FROM HangHoa";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            //Xử lý kết quả
            var sb = new StringBuilder();
            foreach(DataRow dr in dataTable.Rows)
            {
                sb.AppendLine($"{dr["MaHH"]} - {dr["TenHH"]} - {dr["DonGia"]}");
            }

            return Content(sb.ToString());
            //return View();
        }

        public IActionResult ChangeData()
        {
            SqlConnection connection = new SqlConnection(chuoiKetNoi);

            var sql = "INSERT INTO Loai(TenLoai, MoTa, Hinh) VALUES(N'Nước giải khát', N'Uống có hại nhưng lợi', NULL)";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            int result = command.ExecuteNonQuery();
            connection.Close();

            return Content($"{result}");
        }

        public IActionResult DocJsonConfig()
        {
            var builder = 
new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("myappsettings.json");
            var config = builder.Build();
            var ten = config["Ten"];
            var web = config["KhoaHoc:Web"];

            var cisco = config["KhoaHoc:Mang:Cisco"];

            var conStr1 = config["ConnectionStrings:Database1"];
            var conStr2 = config.GetConnectionString("Database1");

            return Json(true);
        }
    }
}