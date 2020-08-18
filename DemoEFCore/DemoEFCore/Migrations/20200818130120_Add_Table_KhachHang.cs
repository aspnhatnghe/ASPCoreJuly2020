using Microsoft.EntityFrameworkCore.Migrations;
using System.Runtime.InteropServices.ComTypes;

namespace DemoEFCore.Migrations
{
    public partial class Add_Table_KhachHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"
--Thêm loại
CREATE PROC spThemLoai
	@MaLoai int output, -- truyền ngược từ SP ra
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	--Bước 1: Thêm vào CSDL
	INSERT INTO Loai(TenLoai, Hinh, MoTa)
	VALUES(@TenLoai, @Hinh, @MoTa)
	--Bước 2: Lấy giá trị vừa tăng
	SELECT @MaLoai = @@IDENTITY
END
GO
";
            migrationBuilder.Sql(sqlScript);

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<string>(maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(maxLength: 50, nullable: false),
                    DienThoai = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHang");
        }
    }
}
