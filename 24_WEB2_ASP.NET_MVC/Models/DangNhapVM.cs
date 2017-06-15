using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace _24_WEB2_ASP.NET_MVC.Models
{
    public class DangNhapVM
    {
        public int ID
        {
            get; set;
        }
        [DisplayName("Email")]
        [System.Web.Mvc.Remote("CheckUserEmail", "TaiKhoan", ErrorMessage = "Emai không chính xác.")]
        [Required(ErrorMessage = "Hãy nhập Email đăng nhập.")]
        public string Email
        {
            get; set;
        }
        [DisplayName("Pass")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu.")]
        public string Pass
        {
            get; set;
        }
    }
}