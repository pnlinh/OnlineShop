using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class TimKiem
    {
        public IEnumerable<SanPham> SanPham { get; set; }
        public IEnumerable<LoaiSanPham> LoaiSanPham { get; set; }
        public IEnumerable<Danh_Muc> DanhMuc { get; set; }
        public IEnumerable<NhaSanXuat> NhaSanXuat { get; set; }
        public List<SanPhamYeuThich> SanPhamYeuThich { get; set; }
    }
}