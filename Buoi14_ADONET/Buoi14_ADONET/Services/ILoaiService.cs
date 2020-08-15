using Buoi14_ADONET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi14_ADONET.Services
{
    public interface ILoaiService
    {
        List<Loai> LayTatCa();
        List<Loai> Tim(string tuKhoa);
        Loai LayLoai(int maLoai);
        void CapNhat(Loai loai);
        void ThemMoi(Loai loai);
        void Xoa(int maLoai);
    }
}
