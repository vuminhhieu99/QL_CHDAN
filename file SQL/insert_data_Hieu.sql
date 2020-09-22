---------------------------------------INSERT DATA---------------------------------------
------------------------------PHẦN A : NHẬP CÁC BẢNG DANH MỤC----------------------------

-- TẠO dòng phiếu nhập và lô hàng tương ứng
-- cập nhập tổng tiền cho phiếu nhập

CREATE PROC INSERT_DongPhieuNhap_LoHang(@maPhieuNhap INT, @SoLuong INT, @DonGiaNhap DECIMAL(10,3), @MaHang INT, @HSD DATE)
AS
BEGIN
	INSERT DBO.DongPhieuNhap(MaPhieuNhap,SoLuong, DonGiaNhap) VALUES (@maPhieuNhap, @SoLuong, @DonGiaNhap)
	declare @MaDPN INT
	SELECT @MaDPN = COUNT(*) FROM DBO.DongPhieuNhap
	IF NOT EXISTS(SELECT * FROM LoHang WHERE LoHang.MaDPN = @MaDPN)
	BEGIN
		INSERT DBO.LoHang(HSD,TonKho, MaHang, MaDPN) VALUES(@HSD, @SoLuong, @MaHang, @MaDPN)
	END
	update PhieuNhap set TongTien = TongTien + @DonGiaNhap*@SoLuong from PhieuNhap where PhieuNhap.MaPhieuNhap = @maPhieuNhap
END
GO	
-- TẠO dòng phiếu xuất
-- cập nhập tổng tiền cho phiếu xuất
-- cập nhật tồn kho cho lô hàng
CREATE PROC INSERT_DongPhieuXuat(@MaLo INT, @maPhieuXuat INT, @soluong INT)
AS
BEGIN
	INSERT DBO.DongPhieuXuat(MaLo,MaPhieuXuat,SoLuong) VALUES (@MaLo,@maPhieuXuat, @SoLuong)
	DECLARE @DonGia DECIMAL(10,3)
	SELECT @DonGia = MatHang.DonGia from MatHang join LoHang on MatHang.MaHang = LoHang.MaHang
	update PhieuXuat set TongTien = TongTien + @DonGia*@SoLuong from PhieuXuat where PhieuXuat.MaPhieuXuat = @maPhieuXuat
	update LoHang set TonKho = TonKho - @SoLuong from LoHang where LoHang.MaLo = @MaLo

END
GO

--Loại hàng----------------------
INSERT dbo.LoaiHang (TenLoai ) VALUES  ( N'món ăn' )
INSERT dbo.LoaiHang (TenLoai ) VALUES  ( N'đồ uống' )
INSERT dbo.LoaiHang (TenLoai ) VALUES  ( N'nguyên liệu chế biến')
INSERT dbo.LoaiHang (TenLoai ) VALUES  ( N'dụng cụ')
INSERT dbo.LoaiHang (TenLoai ) VALUES  ( N'nhiên liệu')
GO

--Mặt hàng-------------------------
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'CoCa-CoLa' ,  N'lon' , 15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Pepsi' ,  N'lon' , 15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Fanta' ,  N'lon' , 15000, 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Gà Lắc Chanh Sả' , N'suất', 70000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bánh Bao Xá Xíu' ,  N'chiếc', 20000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Gà Bó Xôi Chiên' ,  N'con', 120000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Gà Rán KFC' ,  N'suất', 80000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Khoai Tây Chiên', N'suất', 20000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Khoai Lang Lắc', N'suất', 30000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Pizza Hải Sản' , N'chiếc',  80000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Humberger Thịt Lớn', N'chiếc', 30000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bánh Bao Thịt Lớn',  N'chiếc', 20000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bánh Bao Chay Đậu Xanh',  N'chiếc' , 15000 , 1  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bia Hà Nội',  N'lon',  15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bia Haniken' ,  N'lon' ,  15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bia Tiger' ,  N'lon' ,  15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Nước Lọc Lavie' ,  N'chai' ,  15000 , 2  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Thịt Gà',  N'kg', 70000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Thịt Lợn',  N'kg' , 150000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bột Mì',  N'kg',  30000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bột gạo' ,  N'kg' ,  14000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Mực' ,  N'kg' ,  270000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'TÔm' ,  N'kg' ,  180000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Cá Thu' ,  N'kg' ,  310000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Đậu Xanh' ,  N'kg' ,  40000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'khai Tây' ,  N'kg' ,  30000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Bột Tẩm Chiên' ,  N'kg' ,  350000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Tương Ớt' ,  N'lít' ,  122000 , 3  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Đĩa' ,  N'chiếc' ,  122000 , 4  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Dao' ,  N'chiếc' ,  122000 , 4  )
INSERT dbo.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES  ( N'Cốc' ,  N'chiếc' ,  122000 , 4  )
GO

--Nhà cung cấp---------------------------------
INSERT dbo.NhaCungCap ( TenNCC )VALUES  ( N'Cty CP Thực phẩm Vinfarm' )
INSERT dbo.NhaCungCap ( TenNCC )VALUES  ( N'Cty CP Thực phẩm Funfarm' )
INSERT dbo.NhaCungCap ( TenNCC )VALUES  ( N'Cty Pepsi Hà Nội' )
INSERT dbo.NhaCungCap ( TenNCC )VALUES  ( N'Cty TNHH Bia Việt Nam' )
INSERT dbo.NhaCungCap ( TenNCC )VALUES  ( N'Cty Coca-Cola Hà Nội' )
GO

--Nhân viên------------------------------------
INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( N'Vũ Minh Hiếu', 1, '12/01/1999', N'0815665478 ', N'thứ 2 tới thứ 7, sáng: 7h- 13h, chiều 13h - 15h',8500000.000, NULL)
INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( N'Nguyên Minh Lý', 1, '11/23/1970', N'0987874562 ', N'thứ 2 tới cn sáng: 7h-10h chiều: 18h- 21h' ,3000000.000, 1)
INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( N'Trần Thị Nhung', 1, '8/01/1995', N'0977884562 ', N'thứ 2 tới cn sáng: 7h-10h chiều: 18h- 21h',5000000.000, 1)
INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( N'Vũ Đức Minh', 1, '06/15/1995', N'0845765444 ', N'thứ 2 tới cn sáng: 7h-10h chiều: 18h- 21h',7000000.000, 1)
INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( N'Đinh Thị Diệu Linh', 1, '8/30/1992', N'0977884562 ', N'thứ 2 tới cn sáng: 7h-10h chiều: 18h- 21h',5000000.000, 1)
GO

--Cung ứng-----------------------------------------
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (1,5,13000,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (2,3,13000,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (3,3,13700,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (4,5,13000,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (12,2,13500,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (13,2,22000,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (14,4,33200,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (15,4,13500,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (16,4,10000,1)
INSERT dbo.CungUng(MaHang,MaNCC,GiaCungUng,DangNhap) values (17,2,10500,1)
GO

--Phiếu nhập------------------------------------------
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 4 , 5)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 4 , 3)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 5 , 3)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 5 , 4)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 4 , 2)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 4 , 2)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 4 , 3)
INSERT dbo.PhieuNhap ( MaNV, MaNCC) VALUES  ( 5 , 2)
GO

--Dòng phiếu nhập và lô hàng-------------------------
EXEC INSERT_DongPhieuNhap_LoHang 1, 100, 12000, 1, '12/05/2021'
EXEC INSERT_DongPhieuNhap_LoHang 2, 100, 12500, 2, '12/25/2020'
EXEC INSERT_DongPhieuNhap_LoHang 2, 50, 13000, 3, '01/28/2021'
EXEC INSERT_DongPhieuNhap_LoHang 5, 20, 12000, 12, '03/30/2020'
EXEC INSERT_DongPhieuNhap_LoHang 5, 15, 22500, 13, '03/30/2020'
EXEC INSERT_DongPhieuNhap_LoHang 6, 15, 22500, 12, '04/19/2020'
EXEC INSERT_DongPhieuNhap_LoHang 6, 15, 22500, 13, '04/27/2020'
EXEC INSERT_DongPhieuNhap_LoHang 6, 10, 30000, 5, '04/22/2020'
EXEC INSERT_DongPhieuNhap_LoHang 7, 50, 10000, 2, '12/03/2021'
EXEC INSERT_DongPhieuNhap_LoHang 3, 30, 10000, 2, '12/06/2020'
EXEC INSERT_DongPhieuNhap_LoHang 3, 20, 10000, 3, '07/23/2020'
EXEC INSERT_DongPhieuNhap_LoHang 4, 24, 10000, 14, '09/05/2021'
EXEC INSERT_DongPhieuNhap_LoHang 4, 24, 10500, 15, '12/16/2020'
EXEC INSERT_DongPhieuNhap_LoHang 4, 24, 10500, 16, '03/12/2021'

EXEC INSERT_DongPhieuNhap_LoHang 8, 50, 70000, 18, '03/08/2023'
EXEC INSERT_DongPhieuNhap_LoHang 8, 50, 15000, 19, '08/11/2022'
GO

--Phiếu xuất-------------------------------------------
INSERT DBO.PhieuXuat(MaNV,TenNguoiNhan,DiaChiKho) VALUES (3, 'Hoàng Thúy An', '120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
INSERT DBO.PhieuXuat(MaNV,TenNguoiNhan,DiaChiKho) VALUES (5, 'Sa Minh Lợi','120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
INSERT DBO.PhieuXuat(MaNV,TenNguoiNhan,DiaChiKho) VALUES (3, 'Phạm Văn Hào','120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
INSERT DBO.PhieuXuat(MaNV,TenNguoiNhan,DiaChiKho) VALUES (5, 'Đồng Đức Năng','120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
INSERT DBO.PhieuXuat(MaNV,TenNguoiNhan,DiaChiKho) VALUES (5, 'Hoàng Quý Thái','120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
INSERT DBO.PhieuXuat(ThoiGian,MaNV,TenNguoiNhan,DiaChiKho) VALUES (GETDATE(), 5, 'Hoàng Quý Thái','120 Đốc Ngữ - Vĩnh Phú - Ba Đình - Hà Nội')
GO

--Dòng phiếu xuất-----------------------------------------
EXEC INSERT_DongPhieuXuat 1, 1, 3
EXEC INSERT_DongPhieuXuat 5, 1, 1
EXEC INSERT_DongPhieuXuat 7, 1, 2
EXEC INSERT_DongPhieuXuat 8, 2, 6
EXEC INSERT_DongPhieuXuat 9, 2, 6
EXEC INSERT_DongPhieuXuat 11, 3, 1
EXEC INSERT_DongPhieuXuat 4, 3, 5
EXEC INSERT_DongPhieuXuat 9, 3, 12
EXEC INSERT_DongPhieuXuat 3, 3, 2
EXEC INSERT_DongPhieuXuat 12, 4, 10
EXEC INSERT_DongPhieuXuat 4, 5, 2
EXEC INSERT_DongPhieuXuat 10, 5, 3
EXEC INSERT_DongPhieuXuat 8, 5, 1
GO


--Nhập theo quy trình nghiệp vụ--------------------
-- hàng hóa: mã hàng,tên hàng
-- Lô hàng: mã lô, tồn kho, hạn sử dụng
-- loại hàng: tên loại
-- phiếu xuất :mã phiếu xuất, thời gian
-- dòng phiếu xuất:mã phiếu xuất, số lượng, đơn giá nhập

CREATE PROC INSERT_NV_BaoCaoTonKho(@TenHang NVARCHAR(500),  @TenLoai NVARCHAR(50), @SoLuongNhap INT, @DonGiaNhap DECIMAL(10,3), @HSD DATE)
AS
BEGIN
	-- Kiểm tra dữ liệu đã có thông tin  mặt hàng và loại hàng của lô hàng đã tồn tại hay chưa
	IF NOT EXISTS(SELECT * FROM DBO.MatHang WHERE MatHang.TenHang = @TenHang)
	BEGIN
		IF NOT EXISTS(SELECT * FROM DBO.LoaiHang WHERE LoaiHang.TenLoai = @TenLoai)
		BEGIN
		INSERT DBO.LoaiHang(TenLoai) VALUES(@TenHang)
		END
		DECLARE @MaLoai TINYINT;
		SELECT @MaLoai = LoaiHang.MaLoai FROM LoaiHang WHERE LoaiHang.TenLoai = @TenLoai
		INSERT DBO.MatHang(TenHang, DvTinh, MaLoai) VALUES(@TenHang, NULL, @MaLoai)
	END
	DECLARE @MaHang INT;
	SELECT @MaHang = MatHang.MaHang FROM MatHang WHERE MatHang.TenHang = @TenHang
	-- tạo dữ liệu lô hàng đi kèm với dòng phiếu nhập	
	EXEC INSERT_DongPhieuNhap_LoHang NULL, @SoLuongNhap, @DonGiaNhap, @MaHang, @HSD
END
GO

-- tạo dòng phiếu xuất 
-- cập nhật tổng tiền cho phiếu xuất
-- cập nhật tồn kho cho lô hàng tương ứng
CREATE PROC INSERT_NV_DongPhieuXuat(@MaLo INT, @maPhieuXuat INT, @soluong INT)
AS
BEGIN
	INSERT DBO.DongPhieuXuat(MaLo,MaPhieuXuat,SoLuong) VALUES (@MaLo,@maPhieuXuat, @SoLuong)
	DECLARE @DonGia DECIMAL(10,3)
	SELECT @DonGia = MatHang.DonGia from MatHang join LoHang on MatHang.MaHang = LoHang.MaHang
	update PhieuXuat set TongTien = TongTien + @DonGia*@SoLuong from PhieuXuat where PhieuXuat.MaPhieuXuat = @maPhieuXuat
	update LoHang set TonKho = TonKho - @SoLuong from LoHang where LoHang.MaLo = @MaLo

END
GO

-- NHập dữ liệu
 --1
EXEC INSERT_NV_BaoCaoTonKho N'CoCa-CoLa', 'đồ uống', 64, 12500, '09/16/2020'
INSERT DBO.PhieuXuat(ThoiGian,MaNV,TenNguoiNhan,DiaChiKho) VALUES ('2019-08-01 17:29:15.000', 3 , NULL, NULL)
EXEC INSERT_NV_DongPhieuXuat 1, 7 , 5
EXEC INSERT_NV_DongPhieuXuat 1, 6 , 2
GO

--2
EXEC INSERT_NV_BaoCaoTonKho N'Bia Hà Nội', 'đồ uống', 48, 10000, '09/16/2020'
INSERT DBO.PhieuXuat(ThoiGian,MaNV,TenNguoiNhan,DiaChiKho) VALUES ('2019-08-01 17:29:15.000', 3 , NULL, NULL)
EXEC INSERT_NV_DongPhieuXuat 4, 6 , 1
EXEC INSERT_NV_DongPhieuXuat 15, 7 , 1
GO
--3
EXEC INSERT_NV_BaoCaoTonKho N'Bánh Bao Xá Xíu', 'món ăn', 22, 10000, '03/16/2020'
INSERT DBO.PhieuXuat(ThoiGian,MaNV,TenNguoiNhan,DiaChiKho) VALUES ('2019-08-01 17:29:15.000', 3 , NULL, NULL)
EXEC INSERT_NV_DongPhieuXuat 8, 7 , 1
GO