CREATE PROC DoanhThuTheoHang @HangSX NVARCHAR(50) OUT, @TongTien INT OUT
AS
BEGIN
	SELECT nsx.TenNhaSanXuat AS HangSX, SUM(cthd.SoLuong * cthd.DonGia) AS TongTien 
	FROM dbo.ChiTiet_HoaDon cthd
	JOIN dbo.HoaDon hd ON hd.ID = cthd.idHoaDon 
	JOIN dbo.SanPham sp ON sp.ID = cthd.idSanPham
	JOIN dbo.NhaSanXuat nsx ON nsx.ID = sp.idNhaSanXuat
	WHERE hd.TrangThai = 3
	GROUP BY nsx.TenNhaSanXuat
END


EXEC dbo.DoanhThuTheoHang @HangSX = N'', -- nvarchar(50)
    @TongTien = 0 -- int


