
using Microsoft.EntityFrameworkCore;

namespace DemoEFCore.Entities
{
    public class MyDbContext : DbContext
    {
        public DbSet<Loai> Loais { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }

        public MyDbContext(DbContextOptions options):base(options)
        {
        }
    }
}
