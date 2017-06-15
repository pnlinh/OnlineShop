using _24_WEB2_ASP.NET_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Helpers;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class DonHangController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();

        // GET: Admin/DonHang
        public ActionResult TimKiem(string q, int page = 1)
        {
            q = q.Trim().ToLower();

            var danhSach = from x in db.HoaDons select x;

            if (!string.IsNullOrEmpty(q))
            {
                danhSach = danhSach.Where(x => x.TaiKhoan.Name.ToLower().Contains(q));
                int n = danhSach.Count();

                int rowPerPage = 10;

                int totalPage = (int)Math.Ceiling((double)n / rowPerPage);

                ViewBag.totalPage = totalPage;
                ViewBag.curPage = page;

                danhSach = danhSach.OrderByDescending(x => x.ID).Skip((page - 1) * rowPerPage).Take(rowPerPage);
            }

            ViewBag.q = q;

            return View(danhSach);
        }

        public ActionResult DanhSach(int page = 1)
        {
            int n = db.HoaDons.Count();

            int rowPerPage = 12;

            int totalPage = (int)Math.Ceiling((double)n / rowPerPage);

            ViewBag.totalPage = totalPage;
            ViewBag.curPage = page;

            var danhSach = db.HoaDons
                .OrderByDescending(x => x.TrangThai == 1)
                .Skip((page - 1) * rowPerPage)
                .Take(rowPerPage)
                .ToList();

            return View(danhSach);
        }

        public ActionResult ChiTiet(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("ThongKe", "TrangChu");
            }

            var dh = db.HoaDons.First(x => x.ID == id);

            ViewBag.DiaChiGiaoHang = db.DiaChiGiaoHangs.First(x => x.idHoaDon == id);

            return View(dh);
        }

        public ActionResult GiaoHang(int id)
        {
            var dh = db.HoaDons.First(x => x.ID == id);
            dh.TrangThai = 2;

            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach", "DonHang");
        }

        public ActionResult ThanhToan(int id)
        {
            var dh = db.HoaDons.First(x => x.ID == id);
            dh.TrangThai = 3;

            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var sanPham = db
                .ChiTiet_HoaDon
                .Where(x => x.idHoaDon == id)
                .Select(x => new
                {
                    x.idSanPham,
                    x.SoLuong
                }).ToList();

            foreach (var item in sanPham)
            {
                var sp = db.SanPhams.First(x => x.ID == item.idSanPham);
                sp.SoLuongBan += item.SoLuong;
                sp.SoLuongTon = sp.SoLuong - sp.SoLuongBan;

                db.SaveChanges();
            }

            return RedirectToAction("DanhSach", "DonHang");
        }

        public ActionResult Xoa(int id)
        {
            var dh = db.HoaDons.First(x => x.ID == id);
            dh.TrangThai = 4;

            db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }
    }
}