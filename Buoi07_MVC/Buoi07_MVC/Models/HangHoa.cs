using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi07_MVC.Models
{
    public class HangHoa
    {
        [Display(Name ="Mã hàng hóa")]
        public Guid MaHh { get; set; }

        [Display(Name = "Tên hàng hóa")]
        [Required]
        public string TenHh { get; set; }

        [Display(Name = "Số lượng")]
        [Range(0, int.MaxValue)]
        public int SoLuong { get; set; }

        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }
    }
}
