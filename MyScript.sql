use NhatNghe
go

select MADV, TenDV
from DonVi

-- LAB04
--Chèn 5 đơn vị
INSERT INTO DonVi(TenDV, MADV)
	VALUES(N'Kế hoạch', '11111')

INSERT INTO DonVi(MADV, TenDV)
	VALUES('22222', N'Kế hoạch Tổng hợp')

--Thêm nhân viên
INSERT INTO NhanVien(HoTen, NgaySinh, Luong)
	VALUES(N'Hà Huy Tập', '2000-1-1', '1999')

INSERT INTO NhanVien(HoTen, NgaySinh, Luong, DonVi, CMND)
VALUES(N'Huy Giáp', '1999-11-1', '199', '11111', '123456789')

INSERT INTO NhanVien(HoTen, NgaySinh, Luong, DonVi, CMND)
VALUES(N'Lệ Lợi','2015-11-1', '121.137', '11111', '213456789')
INSERT INTO NhanVien(HoTen, NgaySinh, Luong, DonVi, CMND)
VALUES(N'Lê Lai','2015-1-1', '280', 'PKHTC', '213456798')

---TRUY VẤN DỮ LIỆU
--1/ Lấy thông tin nhân viên có đơn vị
SELECT MaNV, HoTen, GioiTinh, 
	CASE
		WHEN GioiTinh = 0 THEN N'Nữ'
		WHEN GioiTinh = 1 THEN N'Nam'
	END as GT,
	NgaySinh, 
	YEAR(GETDATE()) - YEAR(NgaySinh) as Tuoi,
	Luong
FROM NhanVien
WHERE DonVi IS NOT NULL

select * from NhanVien
where HoTen like N'%Thị%'

select * from NhanVien
where DonVi IN ('11111', 'PKHTC', 'PHCKT')
--DonVi = '11111' OR DonVi = 'PKHTC'

select * from NhanVien
where DonVi IN (
	select madv from DonVi
)

select * from NhanVien
where luong between 20 and 1950

select 4 + ' Anh'
select concat(4, ' Anh', N' hề')

select GETUTCDATE()

--Thông kê
select max(luong) LuongLonNhat,
	min(YEAR(GETDATE()) - YEAR(NgaySinh)) TuoiNN,
	count(*) SoLuongNv
from NhanVien

select GioiTinh, DonVi, max(luong) LuongLonNhat,
	min(YEAR(GETDATE()) - YEAR(NgaySinh)) TuoiNN,
	count(*) SoLuongNv
from NhanVien
group by DonVi, GioiTinh
having max(luong) > 500

select MaNV, HoTen, Luong, DonVi, TenDV
from NhanVien nv JOIN DonVi dv 
		ON dv.MADV = nv.DonVi
ORDER BY TenDV ASC, Luong DESC

