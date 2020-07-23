using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi06_MVCBasic.Models
{
    public class HangHoa
    {
        //Field
        private string _tenHh;
        private double _donGia;

        //Property
        public double DonGia
        {
            get { return _donGia; }
            set { _donGia = value; }
        }
        public string TenHh
        {
            get => _tenHh;
            set => _tenHh = value;
        }

        //Automatic Property
        public int SoLuong { get; set; }

        //Property Read only 
        public bool ConHang
        {
            get { return SoLuong > 5; }
        }
        public bool DangCoHang => SoLuong > 5;

        public static int Dem { get; set; } = 0;
        
        public static string InDem() => $"{Dem}";

        #region [Contructor - Hàm tạo/dựng]
        public HangHoa()
        {
            Dem++;
        }

        public HangHoa(string ten, double gia, int soLuong)
        {
            Dem++;
            TenHh = ten; DonGia = gia;
            SoLuong = soLuong;
        }
        #endregion

        public string In()
        {
            return $"{TenHh} - {DonGia} = còn {SoLuong}";
        }

        public override string ToString()
        {
            //return base.ToString();
            return $"{TenHh} - {DonGia} = còn {SoLuong}";
        }

    }//end class
}
