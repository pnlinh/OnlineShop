using System.ComponentModel.DataAnnotations;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Models
{
    public class NhaSanXuatVM
    {
        [Required(ErrorMessage = "Tên nhà sản xuất không được rỗng")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên nhà sản xuất phải từ 3-50 kí tự")]
        public string TenNhaSanXuat { get; set; }       
    }
}