using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class IndexViewModel
    {
        public ThanhPhoVM TestOne { get; set; }
        public TaiKhoanVM TestTwo { get; set; }
        public HuyenVM TestThree { get; set; }
        public DoiMatKhauVM DoiMatKhau { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public ThanhToanVM ThanhToan { get; set; }
        public SanPham Details { get; set; }
        public DiaChiGiaoHang GiaoHang { get; set; }
        public List<SanPham> SanPham { get; set; }
        public List<ChiTiet_HoaDon> ChiTiet { get; set; }
        public List<Danh_Muc> DanhMuc { get; set; }
        public HoaDon HoaDon { get; set; }
        public List<HoaDon> ListHoaDon { get; set; }
        public List<SanPhamYeuThich> SanPhamYeuThich { get; set; }
        public SanPham_VM sp { get; set; }
        public int Count { get; set; }
        public int DonGia { get; set; }
    }
}