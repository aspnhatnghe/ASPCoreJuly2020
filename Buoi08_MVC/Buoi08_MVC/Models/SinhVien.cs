using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi08_MVC.Models
{
    public class SinhVien
    {
        [Display(Name ="Mã sinh viên")]
        public string MaSinhVien { get; set; }
        [Display(Name ="Họ tên sinh viên")]
        public string HoTen { get; set; }
        [Display(Name ="Điểm số")]
        public double DiemSo { get; set; }
    }
}
