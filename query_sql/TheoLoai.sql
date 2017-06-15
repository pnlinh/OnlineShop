SELECT dm.TenDanhMuc AS DanhMuc, SUM(cthd.SoLuong * cthd.DonGia) AS TongTien
FROM dbo.ChiTiet_HoaDon cthd 
JOIN dbo.HoaDon hd ON hd.ID = cthd.idHoaDon 
JOIN dbo.SanPham sp ON sp.ID = cthd.idSanPham
JOIN dbo.[Danh Muc] dm ON dm.ID = sp.idDanhMuc
WHERE hd.TrangThai = 3
GROUP BY dm.TenDanhMuc