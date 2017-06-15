using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class DoiMatKhauVM
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Mật khẩu cũ trống.")]
        public string Pass { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu mới trống.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự.")]
        public string NewPass { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu nhập lại không được bỏ trống.")]
        [Compare("NewPass", ErrorMessage = "Mật khẩu nhập lại không khớp.")]
        public string ConfirmNewPass { get; set; }
    }
}