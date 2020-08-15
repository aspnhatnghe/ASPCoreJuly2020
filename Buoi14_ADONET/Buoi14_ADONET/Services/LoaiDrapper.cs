using Buoi14_ADONET.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Buoi14_ADONET.Services
{
    public class LoaiDrapper : ILoaiService
    {
        private string chuoiKetNoi;
        public LoaiDrapper(IConfiguration config)
        {
            chuoiKetNoi = config.GetConnectionString("Database1");
        }
        public void CapNhat(Loai loai)
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("MaLoai", loai.MaLoai);
                parameters.Add("TenLoai", loai.TenLoai);
                parameters.Add("MoTa", loai.MoTa);
                parameters.Add("Hinh", loai.Hinh);

                int rowEffect = con.Execute("spSuaLoai", parameters, commandType: CommandType.StoredProcedure);                
            }
        }

        public Loai LayLoai(int maLoai)
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                return con.QueryFirst<Loai>("SELECT * FROM Loai WHERE MaLoai = @Ma", new { Ma = maLoai });
            }
        }

        public List<Loai> LayTatCa()
        {
            using (var con = new SqlConnection(chuoiKetNoi))
            {
                return con.Query<Loai>("SELECT * FROM Loai  ORDER BY TenLoai").ToList();
            }
        }

        public void ThemMoi(Loai loai)
        {
            throw new NotImplementedException();
        }

        public List<Loai> Tim(string tuKhoa)
        {
            throw new NotImplementedException();
        }

        public void Xoa(int maLoai)
        {
            throw new NotImplementedException();
        }
    }
}
