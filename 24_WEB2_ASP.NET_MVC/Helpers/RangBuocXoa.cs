using System.Linq;
using _24_WEB2_ASP.NET_MVC.Models;

namespace _24_WEB2_ASP.NET_MVC.Helpers
{
    public class RangBuocXoa
    {       
        public static bool TheoDanhMuc(int id)
        {
            using (var db = new OnlineShopEntities())
            {
                return db.SanPhams.Count(x => x.idDanhMuc == id) > 0;
            }
        }

        public static bool TheoLoaiSanPham(int id)
        {
            using (var db = new OnlineShopEntities())
            {
                return db.SanPhams.Count(x => x.idLoaiSanPham == id) > 0;
            }
        }

        public static bool TheoNhaSanXuat(int id)
        {
            using (var db = new OnlineShopEntities())
            {
                return db.SanPhams.Count(x => x.idNhaSanXuat == id) > 0;
            }
        }

        public static bool TheoHoaDon(int id)
        {
            using (var db = new OnlineShopEntities())
            {
                return db.ChiTiet_HoaDon.Count(x => x.idSanPham == id) > 0;
            }
        }

        public static bool TheoTaiKhoan(int id)
        {
            using (var db = new OnlineShopEntities())
            {
                return db.HoaDons.Count(x => x.idKhachHang == id) > 0;
            }
        }
    }
}