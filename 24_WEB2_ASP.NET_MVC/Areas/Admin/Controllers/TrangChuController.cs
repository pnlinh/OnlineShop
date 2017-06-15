using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class TrangChuController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();       

        public ActionResult ThongKe()
        {
            Session["donHangMoi"] = db.HoaDons.Count(x => x.TrangThai == 1);

            return View();
        }

        public JsonResult DoanhThuTheoSanPham()
        {
            var doanhThu = from cthd in db.ChiTiet_HoaDon
                join hd in db.HoaDons on cthd.idHoaDon equals hd.ID
                join sp in db.SanPhams on cthd.idSanPham equals sp.ID               
                where hd.TrangThai == 3
                group new { cthd, sp } by new
                {
                    TenSP = sp.TenSanPham
                }
                into g
                select new
                {
                    g.Key.TenSP,
                    TongTien = g.Sum(x => x.cthd.DonGia * x.cthd.SoLuong)
                };

            return Json(doanhThu, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoanhThuTheoDanhMuc()
        {
            var doanhThu = from cthd in db.ChiTiet_HoaDon
                join hd in db.HoaDons on cthd.idHoaDon equals hd.ID
                join sp in db.SanPhams on cthd.idSanPham equals sp.ID
                join dm in db.Danh_Mucs on sp.idDanhMuc equals dm.ID
                where hd.TrangThai == 3
                group new { cthd, dm } by new
                {
                    DanhMuc = dm.TenDanhMuc
                }
                into g
                select new
                {
                    g.Key.DanhMuc,
                    TongTien = g.Sum(x => x.cthd.DonGia * x.cthd.SoLuong)
                };

            return Json(doanhThu, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoanhThuTheoLoai()
        {
            var doanhThu = from cthd in db.ChiTiet_HoaDon
                join hd in db.HoaDons on cthd.idHoaDon equals hd.ID
                join sp in db.SanPhams on cthd.idSanPham equals sp.ID
                join lsp in db.LoaiSanPhams on sp.idLoaiSanPham equals lsp.ID
                where hd.TrangThai == 3
                group new { cthd, lsp } by new
                {
                    LoaiSP = lsp.TenLoaiSanPham
                }
                into g
                select new
                {
                    g.Key.LoaiSP,
                    TongTien = g.Sum(x => x.cthd.DonGia * x.cthd.SoLuong)
                };

            return Json(doanhThu, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoanhThuTheoHang()
        {
            var doanhThu = from cthd in db.ChiTiet_HoaDon
                           join hd in db.HoaDons on cthd.idHoaDon equals hd.ID
                           join sp in db.SanPhams on cthd.idSanPham equals sp.ID
                           join nsx in db.NhaSanXuats on sp.idNhaSanXuat equals nsx.ID
                           where hd.TrangThai == 3
                           group new { cthd, nsx } by new
                           {
                               NhaSX = nsx.TenNhaSanXuat
                           }
                           into g
                           select new
                           {
                               g.Key.NhaSX,
                               TongTien = g.Sum(x => x.cthd.DonGia * x.cthd.SoLuong)
                           };

            return Json(doanhThu, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DoanhThuTheoThang()
        {
            var doanhThu = from dh in db.HoaDons
                           where dh.TrangThai == 3
                           group dh by new
                           {
                               Thang = SqlFunctions.DatePart("mm", dh.NgayDat)
                           }
                into g
                           select new
                           {
                               g.Key.Thang,
                               TongTien = g.Sum(dh => dh.TongTien)
                           };

            return Json(doanhThu, JsonRequestBehavior.AllowGet);
        }
    }
}