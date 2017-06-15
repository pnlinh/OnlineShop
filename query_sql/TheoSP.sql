SELECT sp.TenSanPham AS TenSP, SUM(cthd.DonGia * cthd.SoLuong) AS TongTien
FROM dbo.ChiTiet_HoaDon cthd 
JOIN dbo.SanPham sp ON sp.ID = cthd.idSanPham
Join HoaDon hd on hd.ID = cthd.idHoaDon
where hd.TrangThai = 3
GROUP BY sp.TenSanPham