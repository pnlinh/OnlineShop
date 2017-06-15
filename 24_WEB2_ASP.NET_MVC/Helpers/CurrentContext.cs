using System.Web;
using _24_WEB2_ASP.NET_MVC.Models;

namespace _24_WEB2_ASP.NET_MVC.Helpers
{
    public class CurrentContext
    {       
        public static int SoLuongDonHangMoi()
        {
            return (int) HttpContext.Current.Session["donHangMoi"];
        }

        public static bool DaDangNhap()
        {
            var kiemTra = HttpContext.Current.Session["dangnhap"];

            return kiemTra != null && (int) kiemTra != 0;
        }

        public static TaiKhoan LayThongTinAdmin()
        {
            return (TaiKhoan) HttpContext.Current.Session["admin"];
        }

        public static void Xoa()
        {
            HttpContext.Current.Session["dangnhap"] = 0;           
            HttpContext.Current.Session["admin"] = null;
        }
    }
}