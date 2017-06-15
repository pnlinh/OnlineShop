using System.ComponentModel.DataAnnotations;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Models
{
    public class DanhMucVM
    {
        [Required(ErrorMessage = "Tên danh mục không được rỗng")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên danh mục phải từ 3-50 kí tự")]
        //[RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "Tên danh mục không hợp lệ")]
        public string TenDanhMuc { get; set; }

        public bool BiXoa { get; set; }
    }
}