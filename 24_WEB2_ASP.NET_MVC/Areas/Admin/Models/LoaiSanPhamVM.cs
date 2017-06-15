using System.ComponentModel.DataAnnotations;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Models
{
    public class LoaiSanPhamVM
    {
        [Required(ErrorMessage = "Tên loại sản phẩm không được rỗng")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên loại sản phẩm phải từ 3-50 kí tự")]
        public string TenLoaiSanPham { get; set; }

        public int idDanhMuc { get; set; }       
    }
}