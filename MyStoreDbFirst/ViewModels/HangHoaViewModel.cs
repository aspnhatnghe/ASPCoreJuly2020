using System.ComponentModel.DataAnnotations;

namespace MyStoreDbFirst.ViewModels
{
    public class HangHoaViewModel
    {
        [Display(Name ="Mã")]
        public int MaHh { get; set; }
        [Display(Name = "Tên hàng hóa")]
        public string TenHh { get; set; }
        [Display(Name = "Loại")]
        public string Loai { get; set; }
        [Display(Name = "Nhà cung cấp")]
        public string NhaCungCap { get; set; }
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }
    }
}
