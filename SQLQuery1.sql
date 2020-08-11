----------1.VIEW
--View xenm thông tin hàng hóa
CREATE VIEW vHangHoa AS
SELECT hh.*, TenLoai, TenCongTy as TenNhaCungCap
FROM HangHoa hh JOIN Loai lo
		ON hh.MaLoai = lo.MaLoai
	JOIN NhaCungCap ncc ON ncc.MaNCC = hh.MaNCC

select MaHH, TenHH, TenLoai, TenNhaCungCap, DonGia
from vHangHoa
where DonGia BETWEEN 10 AND 100

--View chi tiết hóa đơn
CREATE VIEW vHoaDon AS
SELECT cthd.*, TenHH, TenLoai, TenCongTy as NhaCungCap
FROM ChiTietHD cthd JOIN HangHoa hh
		ON hh.MaHH = cthd.MaHH
	JOIN Loai lo ON hh.MaLoai = lo.MaLoai
	JOIN NhaCungCap ncc ON ncc.MaNCC = hh.MaNCC

select * from vHoaDon where MaHD = 10248

----------2. STORE PROC
--Viết store lấy thông tin hàng hóa theo loại, nhà cc
CREATE PROC spLayHangHoa
	@MaLoai int,
	@MaNCC nvarchar(50)
AS BEGIN
	SELECT * FROM vHangHoa
	WHERE MaLoai = @MaLoai AND MaNCC = @MaNCC
END

spLayHangHoa 1002, 'NK'
EXEC spLayHangHoa 1000, 'NK'
EXECUTE spLayHangHoa 1001, 'SS'

---Thêm Loại
CREATE PROC spThemLoai
	@MaLoai int output,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN
	--Chèn
	INSERT INTO Loai(TenLoai, MoTa, Hinh)
		VALUES(@TenLoai, @MoTa, @Hinh)
	--Lấy giá trị tăng
	SELECT @MaLoai = @@IDENTITY
END

--demo
DECLARE @Ma int
EXEC spThemLoai @Ma output, N'Bánh trung thu', null, null
PRINT CONCAT(N'Mã loại vừa tạo: ', @Ma)

---Sửa Loại
CREATE PROC spSuaLoai
	@MaLoai int,
	@TenLoai nvarchar(50),
	@MoTa nvarchar(max),
	@Hinh nvarchar(50)
AS BEGIN	
	UPDATE Loai SET TenLoai = @TenLoai, MoTa = @MoTa
	WHERE MaLoai = @MaLoai
	IF @Hinh IS NOT NULL
	BEGIN
		UPDATE Loai SET Hinh = @Hinh
		WHERE MaLoai = @MaLoai
	END
END


CREATE PROC spXoaLoai
	@MaLoai int
AS BEGIN
	DELETE FROM Loai WHERE MaLoai = @MaLoai
END

CREATE PROC spLayLoai
	@MaLoai int
AS BEGIN	
	SELECT * FROM Loai
	WHERE @MaLoai IS NULL OR MaLoai = @MaLoai
END

exec spLayLoai null
exec spLayLoai 1002

CREATE PROC spTimLoai
	@TuKhoa nvarchar(50)
AS BEGIN	
	SELECT * FROM Loai
	WHERE TenLoai LIKE N'%' + @TuKhoa + N'%'
END

exec spTimLoai 'n'

--Tìm hàng hóa gần đúng theo tên
CREATE PROC spTimHangHoa
	@TuKhoa nvarchar(50)
AS BEGIN	
	SELECT * FROM vHangHoa
	WHERE TenHH LIKE N'%' + @TuKhoa + N'%'
		OR TenLoai LIKE N'%' + @TuKhoa + N'%'
END

exec spTimHangHoa 'no'

-----------C. FUNCTION
--Tạo hàm tính doanh thu theo loại
CREATE FUNCTION fDoanhThuTheoLoai(
	@MaLoai int
) RETURNS float
AS BEGIN
	declare @DoanhThu float

	select @DoanhThu = SUM(SoLuong * cthd.DonGia 
				* (1 - cthd.GiamGia))
	from ChiTietHD cthd JOIN HangHoa hh
		ON hh.MaHH = cthd.MaHH AND MaLoai = @MaLoai

	return @DoanhThu
END

select dbo.fDoanhThuTheoLoai(1000)
select MaLoai, TenLoai, dbo.fDoanhThuTheoLoai(MaLoai) DoanhThu
from Loai
where dbo.fDoanhThuTheoLoai(MaLoai) > 150000

--Funtion trả về table
CREATE FUNCTION fThongKeDoanhThu(
	@Thang int, @Nam int
) RETURNS TABLE AS RETURN
	SELECT MaKH, SUM(SoLuong * cthd.DonGia 
				* (1 - cthd.GiamGia)) DoanhThu
	FROM ChiTietHD cthd JOIN HoaDon hd
			ON hd.MaHD = cthd.MaHD
	WHERE YEAR(hd.NgayDat) = @Nam
		AND MONTH(hd.NgayDat) = @Thang
	GROUP BY MaKH

select * from dbo.fThongKeDoanhThu(11, 1996)

CREATE FUNCTION fThongKeDoanhThuTheoLoai(
	@Quy int, @Nam int, @Loai int
) RETURNS  @temp TABLE(
	MaLoai int, TenLoai nvarchar(50),
	DoanhThu float
)
AS BEGIN
	INSERT INTO @temp
	SELECT hh.MaLoai, TenLoai, 
		SUM(SoLuong * cthd.DonGia 
				* (1 - cthd.GiamGia)) DoanhThu
	FROM ChiTietHD cthd JOIN HoaDon hd
			ON hd.MaHD = cthd.MaHD
		JOIN HangHoa hh ON cthd.MaHH = hh.MaHH
		JOIN Loai lo ON lo.MaLoai = hh.MaLoai
	WHERE YEAR(hd.NgayDat) = @Nam
		AND DATEPART(qq, hd.NgayDat) = @Quy
	GROUP BY hh.MaLoai, TenLoai

	RETURN
END

--TRIGGER
--Tự động cập nhật TongTien của hóa đơn khi T/S/X CTHD
CREATE TRIGGER trgCapNhatTongTien On ChiTietHD
AFTER INSERT, UPDATE, DELETE
AS BEGIN
	DECLARE @MaHD int, @TongTien float;

	WITH BangTam AS(
		SELECT MaHD FROM inserted
		UNION
		SELECT MaHD FROM deleted
	)
	select @MaHD = MaHD FROM BangTam;
	SELECT @TongTien = SUM(SoLuong * cthd.DonGia 
				* (1 - cthd.GiamGia))
	FROM ChiTietHD cthd WHERE MaHD = @MaHD
	update HoaDon set TongTien = @TongTien WHERE MaHD = @MaHD
END

select MaHD, TongTien
from HoaDon
where MaHD = 10248