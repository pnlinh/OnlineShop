//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _24_WEB2_ASP.NET_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiaChiGiaoHang
    {
        public int ID { get; set; }
        public Nullable<int> idTaiKhoan { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<double> TongTien { get; set; }
        public string YeuCau { get; set; }
        public string NgayDatHang { get; set; }
        public string TenKhachHang { get; set; }
        public Nullable<int> idHoaDon { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}