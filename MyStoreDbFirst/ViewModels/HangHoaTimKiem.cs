using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStoreDbFirst.ViewModels
{
    public class HangHoaTimKiem
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public double DonGia { get; set; }
        public double GiamGia { get; set; }
        public double GiaBan => DonGia * (1 - GiamGia);
        public DateTime NgaySanXuat { get; set; }
        public string Loai { get; set; }
    }
}
