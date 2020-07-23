using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi06_MVCBasic.Models
{
    public interface Hinh
    {
        void TinhDienTichChuVi();
    }

    public class HCN : Hinh
    {
        public void TinhDienTichChuVi()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class HinhHoc
    {
        public double DienTich { get; set; }
        public double ChuVi { get; set; }
        public abstract void TinhDienTichChuVi();
        public virtual string Hello() { return "Hello"; }
    }

    public class Mang<T>
    {
        public T Data { get; set; }
    }


    public class HinhChuNhat : HinhHoc
    {
        public string ABC<T>(T a)
        {
            return a.ToString();
        }

        public override void TinhDienTichChuVi()
        {
            throw new NotImplementedException();
        }
    }


}
