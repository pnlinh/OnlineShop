using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;

namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class SanPham_VM
    {
        public string TenDM { get; set; }
        public string TenNsx { get; set; }
        public string TenTL { get; set; }
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> LuotXem { get; set; }
        [DisplayName("numbercart")]
        [System.Web.Mvc.Remote("UpdateCart", "TrangChu", ErrorMessage = "Số lượng không hợp lệ")]
        public int SoLuong { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<int> idLoaiSanPham { get; set; }
        public Nullable<int> idDanhMuc { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public string NoiDung { get; set; }
        public Nullable<bool> BiXoa { get; set; }
        public Nullable<int> idNhaSanXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_HoaDon> ChiTiet_HoaDon { get; set; }
        public virtual Danh_Muc Danh_Muc { get; set; }
        public virtual LoaiSanPham LoaiSanPham { get; set; }
        public virtual NhaSanXuat NhaSanXuat { get; set; }
        IEnumerable<DanhMucVM> DanhMuc { get; set; }
    }
}