using System.ComponentModel.DataAnnotations;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Models
{
    public class AdminVM
    {
        [Required(ErrorMessage = "Email không được rỗng")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được rỗng")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 kí tự trở lên")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [DataType(DataType.Password)]
//        [Compare("Pass", ErrorMessage = "Mật khẩu xác nhận không đúng")]
        public string ConfirmPass { get; set; }

        [Required(ErrorMessage = "Họ tên không được rỗng")]
        public string Name { get; set; }

        public bool PhanQuyen { get; set; }
    }
}