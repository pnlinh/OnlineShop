using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class TaiKhoanVM
    {
        [System.Web.Mvc.Remote("CheckUserEmail1", "TaiKhoan", ErrorMessage = "Emai đã trùng")]
        [Required(ErrorMessage = "Email không được trống.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được trống")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự.")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Bitrh { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu nhập lại không được bỏ trống.")]
        [Compare("Pass", ErrorMessage = "Mật khẩu nhập lại không khớp.")]
        public String Confirm
        {
            get; set;
        }
        public bool PhanQuyen { get; set; }
        public Nullable<int> idThanhPho { get; set; }
        public Nullable<bool> BiXoa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiGiaoHang> DiaChiGiaoHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamYeuThich> SanPhamYeuThiches { get; set; }
        public virtual ThanhPho ThanhPho { get; set; }
    }

    
}