using AutoMapper;
using MyStoreDbFirst.Entities;
using MyStoreDbFirst.ViewModels;

namespace MyStoreDbFirst.Models
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<HangHoa, HangHoaTimKiem>()
                .ForMember(dest => dest.NgaySanXuat,
                    opt => opt.MapFrom(src => src.NgaySx))
                .ForMember(hhv => hhv.Loai,
                    opt => opt.MapFrom(src => src.MaLoaiNavigation.TenLoai));
                //.ReverseMap() : map 2 chiều
        }
    }
}
