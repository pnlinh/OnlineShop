SELECT nsx.TenNhaSanXuat AS HangSX, SUM(cthd.SoLuong * cthd.DonGia) AS TongTien 
FROM dbo.ChiTiet_HoaDon cthd
JOIN dbo.HoaDon hd ON hd.ID = cthd.idHoaDon 
JOIN dbo.SanPham sp ON sp.ID = cthd.idSanPham
JOIN dbo.NhaSanXuat nsx ON nsx.ID = sp.idNhaSanXuat
WHERE hd.TrangThai = 3
GROUP BY nsx.TenNhaSanXuat