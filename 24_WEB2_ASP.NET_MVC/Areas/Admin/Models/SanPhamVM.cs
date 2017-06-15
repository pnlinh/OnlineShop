using System.ComponentModel.DataAnnotations;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Models
{
    public class SanPhamVM
    {
        [Required(ErrorMessage = "Tên sản phẩm không được rỗng")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Tên sảm phẩm phải từ 10-50 kí tự")]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
        public string Gia { get; set; }

        [Required(ErrorMessage = "Mô tả không được rỗng")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Mô tả phải từ 3-50 kí tự")]
        public string MoTa { get; set; }       

        [Required(ErrorMessage = "Hãy nhập số lượng")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Hãy chọn hình ảnh")]
        public string HinhAnh { get; set; }

        //[Required(ErrorMessage = "Hãy chọn ngày nhập")]
        //public string NgayNhap { get; set; }

        [Required(ErrorMessage = "Hãy nhập nội dung")]
        public string NoiDung { get; set; }

        [Required(ErrorMessage = "Hãy chọn loại sản phẩm")]
        public int idLoaiSanPham { get; set; }

        [Required(ErrorMessage = "Hãy chọn danh mục")]
        public int idDanhMuc { get; set; }

        [Required(ErrorMessage = "Hãy chọn nhà sản xuất")]
        public int idNhaSanXuat { get; set; }
    }
}