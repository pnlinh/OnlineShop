using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class ThanhToanVM
    {
        public int ID { get; set; }
        public Nullable<int> idTaiKhoan { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được trống.")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Email không được trống.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }
        public Nullable<double> TongTien { get; set; }
        public string YeuCau { get; set; }
        public string NgayDatHang { get; set; }
        [Required(ErrorMessage = "Tên không được trống.")]
        public string TenKhachHang { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}