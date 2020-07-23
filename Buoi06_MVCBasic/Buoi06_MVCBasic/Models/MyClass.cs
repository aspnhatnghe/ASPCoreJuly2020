using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi06_MVCBasic.Models
{
    /*Extention Method : Phương thức được thêm vào 1 lớp đã có mà ko cần kế thừa
     * Tên lớp định nghĩa method phải chứa static
     * Tên method phải chứa static
     * Tham số đầu tiên (để chỉ method này thuộc lớp nào) phải chứa từ khóa this
     */
    public static class MyClass
    {
        public static int ToiTet(this DateTime date)
        {
            var tet = new DateTime(2021, 1, 1);

            return (tet - date).Days;
        }

        public static string Vietlotte(this string str, bool power)
        {
            int maxValue = power ? 55 : 45;
            Random rd = new Random();
            List<int> ds = new List<int>();
            for(int i = 0; i < 6; i++)
            {
                ds.Add(rd.Next(1, maxValue));
            }

            return string.Join(", ", ds);
        }
    }
}
