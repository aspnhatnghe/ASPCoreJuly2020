
using ASPCore.ADONETLab.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Buoi14_ADONET.Models
{
    public class LoaiDataAccessLayer
    {
        public static List<Loai> GetAll()
        {
            var dsLoai = new List<Loai>();
            var dtLoai = DataProvider.SelectData("spLayTatCaLoai", CommandType.StoredProcedure, null);
            foreach (DataRow dr in dtLoai.Rows)
            {
                dsLoai.Add(new Loai
                {
                    MaLoai = Convert.ToInt32(dr["MaLoai"]),
                    TenLoai = dr["TenLoai"].ToString(),
                    MoTa = dr["MoTa"].ToString(),
                    Hinh = dr["Hinh"].ToString()
                });
            }
            return dsLoai;
        }

        public static int? AddLoai(Loai loai)
        {
            try
            {
                var pa = new SqlParameter[4];
                pa[0] = new SqlParameter("MaLoai", SqlDbType.Int);
                pa[0].Direction = ParameterDirection.Output;
                pa[1] = new SqlParameter("TenLoai", loai.TenLoai);
                pa[2] = new SqlParameter("MoTa", loai.MoTa);
                pa[3] = new SqlParameter("Hinh", loai.Hinh);

                DataProvider.ExcuteNonQuery("spThemLoai", CommandType.StoredProcedure, pa);

                int.TryParse(pa[0].Value.ToString(), out int maLoai);
                return maLoai;
            }
            catch { return null; }
        }
    }
}
