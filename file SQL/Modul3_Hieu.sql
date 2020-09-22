--Vũ Minh Hiếu

USE QL_Kho_CHDAN
GO

--I--SELECT
--1
-- tổng số tiền đã nhập trong ngày '2020-03-03' (1 bảng)
SELECT SUM(PhieuNhap.TongTien) as N'Doanh Thu' from PhieuNhap WHERE Convert(date,PhieuNhap.ThoiGian) = '03/03/2020'
group by Convert(date,PhieuNhap.ThoiGian)

--2
-- tổng số lượng CoCa-CoLa đã bán trong năm nay (4 bảng)
SELECT SUM(SoLuong) AS N'Đã bán CoCa-CoLa'
 from MatHang INNER JOIN LoHang
 ON LoHang.MaHang = MatHang.MaHang AND TenHang= N'CoCa-CoLa'
 INNER JOIN DongPhieuXuat
 ON LoHang.MaLo = DongPhieuXuat.MaLo  
 INNER JOIN PhieuXuat
 ON DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat AND YEAR(ThoiGian) = YEAR(GETDATE())

 --3
 -- xem thông tin nhà cung cấp cung ứng rẻ nhất mặt hàng có tên 'Thịt gà'
 SELECT NhaCungCap.*, CungUng.GiaCungUng AS N'Giá rẻ nhất' from MatHang INNER JOIN CungUng
 on TenHang like N'%CoCa-CoLa%' AND
 MatHang.MaHang = CungUng.MaHang AND 
 CungUng.GiaCungUng =( SELECT MIN(GiaCungUng) from MatHang INNER JOIN CungUng
 on TenHang like N'%CoCa-CoLa%' AND
 MatHang.MaHang = CungUng.MaHang )
 INNER JOIN NhaCungCap
 ON CungUng.MaNCC = NhaCungCap.MaNCC 
 go


 ------------------------------------------------------------------------------------------------------------
 --II VIEW

 --1
 -- Tạo bảng mã nhân viên, tên nhân viên và số lượng phiếu mà họ đã nhập
CREATE VIEW NHANVIEN_SO_PHIEUNHAP AS 
SELECT NhanVien.MaNV, TenNV, count(PhieuNhap.MaPhieuNhap) AS N'soluongPN' FROM NhanVien INNER JOIN PhieuNhap ON NhanVien.MaNV = PhieuNhap.MaNV
GROUP BY NhanVien.MaNV, TenNV
GO
SELECT * FROM NHANVIEN_SO_PHIEUNHAP
GO

--2
-- tạo bảng mặt hàng có têm tổng số lô và số lượng tồn kho
CREATE VIEW MATHANG_PLUS1 AS
SELECT MatHang.MaHang, MatHang.TenHang, MatHang.DvTinh, MatHang.DonGia, MatHang.MaLoai, COUNT(LoHang.MaLo) AS N'TongSoLo', SUM(LoHang.TonKho) AS N'TonKho' FROM MatHang INNER JOIN LoHang ON MatHang.MaHang = LoHang.MaHang
GROUP BY MatHang.MaHang, MatHang.TenHang, MatHang.DvTinh, MatHang.DonGia, MatHang.MaLoai
GO
SELECT * FROM MATHANG_PLUS1
GO

---------------------------------------------------------------------------------------------------------
---III PROC

--1
-- thống kê danh sách các mặt hàng đã xuất trong ngày và số tiền tương ứng của chúng trong ngày hôm đấy theo thứ tự giảm dần
CREATE PROC DoanhThu_XuatHang(@date date)
	AS
BEGIN			
	SELECT TongTien_Theo_Lo.MaHang, TongTien_Theo_Lo.TenHang, SUM(TongTien_Theo_Lo.Tien_Theo_Lo) as TongTien from 
		(SELECT LoHang.MaLo, MatHang.MaHang, MatHang.TenHang, SUM(DongPhieuXuat.SoLuong)*MatHang.DonGia as Tien_Theo_Lo  from MatHang, LoHang, DongPhieuXuat, PhieuXuat 
			WHERE MatHang.MaHang=LoHang. MaHang AND LoHang.MaLo= DongPhieuXuat.MaLo  AND DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat AND
			CONVERT(date, PhieuXuat.ThoiGian)= @date
			GROUP BY LoHang.MaLo, MatHang.DonGia, MatHang.MaHang, MatHang.TenHang
		) 
		as TongTien_Theo_Lo
		GROUP BY TongTien_Theo_Lo.MaHang, TongTien_Theo_Lo.TenHang	
	ORDER BY  TongTien DESC		
END
GO
EXEC DoanhThu_XuatHang '03/03/2020'
GO

--2
--thống kê mức độ tiêu thụ của các mặt hàng trong từng khoảng thời gian
CREATE PROC ThongKe_TieuThu(@start date, @end date)
AS
begin			
	select MatHang.TenHang, sum(Tong_Theo_Lo) AS TieuThu from MatHang, 
		(select LoHang.MaLo, MaHang, sum(DongPhieuXuat.SoLuong) as Tong_Theo_Lo from LoHang 
		inner join DongPhieuXuat on LoHang.MaLo = DongPhieuXuat.MaLo
		inner join PhieuXuat on DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat and PhieuXuat.ThoiGian between @start and @end
		group by LoHang.MaLo, MaHang
		) 
	AS AsTongLo
	where MatHang.MaHang = AsTongLo.MaHang
	GROUP BY MatHang.MaHang, MatHang.TenHang
	ORDER BY TieuThu DESC	
END
GO
EXEC ThongKe_TieuThu '01/03/2019', '12/30/2020'
GO

--3
-- Nhập mã mặt hàng. trả về thông tin các lô hàng của mặt hàng đó và giá chênh lệch so với giá cung ứng hiện nay
-- tham số truyền vào: mã hàng, giá cung ứng của nhà sản xuất đưa ra: max: đắt nhất -- min: rẻ nhất ( giá trị mặc định)	
CREATE PROC ChenhLech_Gia (@MaHang INT, @Gia_CungUng_OP  CHAR(3) = 'MIN')
	AS
	Begin TRAN
	SET @Gia_CungUng_OP = UPPER(@Gia_CungUng_OP)
	DECLARE @Gia_CungUng DECIMAL(12,3)
	IF(@Gia_CungUng_OP ='MAX')		
		select  @Gia_CungUng = MAX(CungUng.GiaCungUng) from CungUng, MatHang where CungUng.MaHang = MatHang.MaHang and MatHang.MaHang = @MaHang		
	ELSE IF(@Gia_CungUng_OP = 'MIN')		
		select @Gia_CungUng = MIN(CungUng.GiaCungUng) from CungUng, MatHang where CungUng.MaHang = MatHang.MaHang and MatHang.MaHang = @MaHang		
	ELSE
		BEGIN
			RAISERROR('@Gia_CungUng_OP PHẢI NHẬP LÀ MAX HOẶC MIN', 16, 1)
			ROLLBACK
		END
	-------------------------------------------	
	select LoHang.MaLo, LoHang.TonKho,LoHang.HSD, MatHang.TenHang, (CungUng.GiaCungUng - DongPhieuNhap.DonGiaNhap) AS Chenh_lech, DongPhieuNhap.DonGiaNhap, CungUng.NgayCapNhatGia from LoHang INNER JOIN MatHang
	on LoHang.MaHang = MatHang.MaHang
	INNER JOIN CungUng
	ON MatHang.MaHang = CungUng.MaHang and
	MatHang.MaHang = @MaHang and			
	CungUng.GiaCungUng = @Gia_CungUng	
	INNER JOIN DongPhieuNhap
	ON LoHang.MaDPN = DongPhieuNhap.MaDPN 	
	order by LoHang.HSD DESC	
COMMIT TRAN
GO
EXEC ChenhLech_Gia 1
GO

-----------------------------------------------------------------------------------
-- TRIGGER

--1
-- xóa dữ liệu bảng Nhà cung cấp có liên quan với bảng cung ứng và Phiếu nhập
-- cài đặt khóa ngoại mã phiếu nhập của dòng phiếu nhập bằng null
CREATE TRIGGER tg_DeleteNhaCungCap ON dbo.NhaCungCap
INSTEAD OF DELETE
AS
BEGIN
	UPDATE PhieuNhap SET MaNCC = NUll 	where
	PhieuNhap.MaNCC IN ( SELECT deleted.MaNCC  FROM  deleted)	
	DELETE FROM dbo.CungUng WHERE MaNCC IN
	( SELECT deleted.MaNCC  FROM  deleted)
	
END
GO
DELETE DBO.NhaCungCap WHERE MaNCC =4 
GO
-- 2
-- update lại dữ liệu dòng phiếu xuất 
-- cập nhật lại tồn kho của lô hàng
-- cập nhật lại tổng tiền của phiếu xuất
CREATE trigger tg_Update_DPX on dbo.DongPhieuXuat
for UPDATE
as
begin
	if exists (select * from inserted)
		begin
			DECLARE @soluong int
			DECLARE @dongia decimal(10,3)
			select  @soluong = inserted.SoLuong from inserted
			select @dongia = MatHang.DonGia from inserted, LoHang, MatHang where inserted.MaLo = LoHang.MaLo and LoHang.MaHang = MatHang.MaHang
			update LoHang set TonKho = TonKho + @soluong where MaLo = (select MaLo from inserted)
			update PhieuXuat set TongTien = TongTien+ @soluong*@dongia where MaPhieuXuat = (select MaPhieuXuat from inserted)
			if exists (select* from deleted)
				begin
					select  @soluong = deleted.SoLuong from deleted					
					update LoHang set TonKho = TonKho - @soluong where MaLo = (select MaLo from deleted)
					update PhieuXuat set TongTien = TongTien- @soluong*@dongia where MaPhieuXuat = (select MaPhieuxuat  from deleted)
				end		
			else
			ROLLBACK TRAN
		end
	else
		begin						
			ROLLBACK TRAN						
		end
end
GO
UPDATE DBO.DongPhieuXuat SET SoLuong =4 WHERE DongPhieuXuat.MaLo=1 AND DongPhieuXuat.MaPhieuXuat =1
GO

--3
-- update lại dữ liệu dòng phiếu nhập 
-- cập nhật lại tồn kho của lô hàng
-- cập nhật lại tổng tiền của phiếu nhập
CREATE trigger tg_Update_DPN on dbo.DongPhieuNhap
for UPDATE
as
begin
	if exists (select * from inserted)
		begin
			DECLARE @soluong int
			DECLARE @dongia decimal(10,3)
			select  @soluong = inserted.SoLuong from inserted
			select @dongia = inserted.DonGiaNhap from inserted
			update LoHang set TonKho = TonKho + @soluong where MaDPN = (select MaDPN from inserted)
			update PhieuNhap set TongTien = TongTien+ @soluong*@dongia where MaPhieuNhap = (select MaPhieuNhap from inserted)
			if exists (select* from deleted)
				begin
					select  @soluong = deleted.SoLuong from deleted
					select @dongia = deleted.DonGiaNhap from deleted
					update LoHang set TonKho = TonKho - @soluong where MaDPN = (select MaDPN from deleted)
					update PhieuNhap set TongTien = TongTien- @soluong*@dongia where MaPhieuNhap = (select MaPhieuNhap from deleted)
				end		
			else
			ROLLBACK TRAN
		end
	else
		begin						
			ROLLBACK TRAN						
		end
end
GO
UPDATE DBO.DongPhieuNhap SET SoLuong =60, DonGiaNhap = 7000.000 WHERE DongPhieuNhap.MaDPN =18
GO

------------------------------------------------------------------------------------------
--IV MẪU BIỂU
-- sử dụng function trả về bảng báo cáo
-- sử dụng con trỏ
-- LƯU Ý. CÓ TRƯỜNG HỢP THÀNH TIỀN <0, SỐ LƯỢNG <0. lỖI DỮ LIÊU(DO XÓA DỮ LIỆU KHÔNG THỐNG NHẤT TỪ TRƯỚC) , KHÔNG PHẢI LỖI FUNTION
CREATE FUNCTION BaoCao_TonKho (@ma_loai INT, @dau_ky DATE , @cuoi_ky DATE)
RETURNS @BAO_CAO Table (
		MaHang INT,
		TenHang NVARCHAR(500),
		MaLo INT,
		SoLuong_DauKy INT,
		ThanhTien_DauKy DECIMAL(12,3),
		SoLuong_NhapKho INT,
		ThanhTien_NhapKho DECIMAL(12,3),
		SoLuong_XuatKho INT,
		ThanhTien_XuatKho DECIMAL(12,3),
		SoLuong_CuoiKy INT,
		ThanhTien_CuoiKy DECIMAL(12,3),
		HSD date		
) AS
BEGIN  
	DECLARE @MaHang INT
	DECLARE @TenHang NVARCHAR(500)
	DECLARE @MaLo INT
	DECLARE @SoLuong_DauKy INT = 0
	DECLARE @ThanhTien_DauKy DECIMAL(12,3) = 0
	DECLARE @SoLuong_NhapKho INT = 0
	DECLARE @ThanhTien_NhapKho DECIMAL(12,3) = 0
	DECLARE @SoLuong_XuatKho INT = 0
	DECLARE @ThanhTien_XuatKho DECIMAL(12,3) = 0
	DECLARE @SoLuong_CuoiKy INT = 0
	DECLARE @ThanhTien_CuoiKy DECIMAL(12,3) = 0
	DECLARE @HSD date
	-- 
	DECLARE @Don_gia DECIMAL(12,3)
	DECLARE @Nhap_them INT
	DECLARE @Da_ban INT
	DECLARE @Ngay_hien_tai DATE = GETDATE()
	DECLARE @Ngay_tinh DATE 
	SET @Ngay_tinh = DATEADD(day,-1,@cuoi_ky)
	
	
	DECLARE MaLo_Cursor cursor for select MaLo from LoaiHang INNER JOIN MatHang ON LoaiHang.MaLoai = @ma_loai AND LoaiHang.MaLoai = MatHang.MaLoai INNER JOIN LoHang ON LoHang.MaHang = MatHang.MaHang
	open MaLo_Cursor 	
	FETCH NEXT FROM MaLo_Cursor INTO @MaLo

	while @@FETCH_STATUS = 0
	BEGIN
		SET @SoLuong_DauKy = 0
		SET @ThanhTien_DauKy = 0
		SET @SoLuong_NhapKho = 0
		SET @ThanhTien_NhapKho = 0
		SET @SoLuong_XuatKho = 0
		SET @ThanhTien_XuatKho = 0
		SET @SoLuong_CuoiKy = 0
		SET @ThanhTien_CuoiKy = 0

		SET @Don_gia = 0
		SET @Nhap_them =0
		SET @Da_ban = 0 
		SELECT @MaHang = MatHang.MaHang , @TenHang = MatHang.TenHang, @Don_gia = MatHang.DonGia FROM MatHang INNER JOIN LoHang ON MatHang.MaHang = LoHang.MaHang AND LoHang.MaLo = @MaLo
		SELECT @Da_ban = SUM(DongPhieuXuat.SoLuong) FROM DongPhieuXuat INNER JOIN PhieuXuat ON  DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat AND DongPhieuXuat.MaLo = @MaLo AND CONVERT(DATE, PhieuXuat.ThoiGian) BETWEEN @Ngay_tinh AND @Ngay_hien_tai
		GROUP BY DongPhieuXuat.MaLo
		SELECT @Nhap_them = SUM(DongPhieuNhap.SoLuong) FROM DongPhieuNhap INNER JOIN PhieuNhap ON  DongPhieuNhap.MaPhieuNhap = PhieuNhap.MaPhieuNhap INNER JOIN LoHang ON LoHang.MaDPN = DongPhieuNhap.MaDPN AND LoHang.MaLo = @MaLo AND CONVERT(DATE, PhieuNhap.ThoiGian) BETWEEN @Ngay_tinh AND @Ngay_hien_tai
		GROUP BY LoHang.MaLo
		--PRINT @Da_ban
		--print @Nhap_them
		SELECT @SoLuong_CuoiKy = LoHang.TonKho + @Da_ban - @Nhap_them FROM LoHang WHERE LoHang.MaLo = @MaLo
		SELECT @ThanhTien_CuoiKy = @SoLuong_CuoiKy*@Don_gia
		--PRINT @ThanhTien_CuoiKy
		SELECT @SoLuong_XuatKho = SUM(DongPhieuXuat.SoLuong) FROM DongPhieuXuat INNER JOIN PhieuXuat ON  DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat AND DongPhieuXuat.MaLo = @MaLo AND CONVERT(DATE, PhieuXuat.ThoiGian) BETWEEN @dau_ky AND @cuoi_ky
		GROUP BY DongPhieuXuat.MaLo
		SELECT @ThanhTien_XuatKho = @SoLuong_XuatKho*@Don_gia

		SELECT @SoLuong_NhapKho = SUM(DongPhieuNhap.SoLuong) FROM DongPhieuNhap INNER JOIN PhieuNhap ON  DongPhieuNhap.MaPhieuNhap = PhieuNhap.MaPhieuNhap INNER JOIN LoHang ON LoHang.MaDPN = DongPhieuNhap.MaDPN AND LoHang.MaLo = @MaLo AND CONVERT(DATE, PhieuNhap.ThoiGian) BETWEEN @dau_ky AND @cuoi_ky
		GROUP BY LoHang.MaLo
		SELECT @ThanhTien_NhapKho = @SoLuong_NhapKho*@Don_gia

		SET @SoLuong_DauKy = @SoLuong_CuoiKy - @SoLuong_NhapKho + @SoLuong_XuatKho
		SET @ThanhTien_DauKy = @SoLuong_DauKy*@Don_gia

		SELECT @HSD = LoHang.HSD FROM LoHang WHERE LoHang.MaLo = @MaLo

		--SELECT * FROM MatHang INNER JOIN LoHang ON MatHang.MaHang = LoHang.MaHang AND LoHang.MaLo = @MaLo
		--SELECT * FROM DongPhieuXuat INNER JOIN PhieuXuat ON  DongPhieuXuat.MaPhieuXuat = PhieuXuat.MaPhieuXuat AND DongPhieuXuat.MaLo = @MaLo AND CONVERT(DATE, PhieuXuat.ThoiGian) BETWEEN @Ngay_tinh AND @Ngay_hien_tai
		--SELECT * FROM DongPhieuNhap INNER JOIN PhieuNhap ON  DongPhieuNhap.MaPhieuNhap = PhieuNhap.MaPhieuNhap INNER JOIN LoHang ON LoHang.MaDPN = DongPhieuNhap.MaDPN AND LoHang.MaLo = @MaLo AND CONVERT(DATE, PhieuNhap.ThoiGian) BETWEEN @Ngay_tinh AND @Ngay_hien_tai
			
			--//
		INSERT INTO @BAO_CAO VALUES (@MaHang,@TenHang,@MaLo,@SoLuong_DauKy,@ThanhTien_DauKy,@SoLuong_NhapKho,@ThanhTien_NhapKho,@SoLuong_XuatKho,@ThanhTien_XuatKho,@SoLuong_CuoiKy,@ThanhTien_CuoiKy,@HSD)
		FETCH NEXT FROM MaLo_Cursor INTO @MaLo
	END
	CLOSE MaLo_Cursor
	DEALLOCATE MaLo_Cursor	
	RETURN
END
GO
SELECT * FROM BaoCao_TonKho(2, '03/01/2020','04/09/2020')


 ---- IMPRESSION
 --  DECLARE @ma_loai INT =1
 --     DECLARE @dau_ky DATE = '03/01/2020'   
	--  DECLARE @cuoi_ky DATE = '03/02/2020'
	--  ---
	 