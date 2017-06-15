using System;
using _24_WEB2_ASP.NET_MVC.Models;
using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;
using _24_WEB2_ASP.NET_MVC.Helpers;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class NhaSanXuatController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();

        public ActionResult DanhSach()
        {
            var danhSach = db.NhaSanXuats.ToList().OrderByDescending(x => x.ID);

            return View(danhSach);
        }

        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them(NhaSanXuatVM model)
        {
            if (ModelState.IsValid)
            {
                NhaSanXuat nsx = new NhaSanXuat
                {
                    TenNhaSanXuat = model.TenNhaSanXuat
                };

                var kiemTra = db.NhaSanXuats.FirstOrDefault(x => x.TenNhaSanXuat == model.TenNhaSanXuat);
                if (kiemTra != null)
                {
                    ViewBag.Loi = "Tên nhà sản xuất đã tồn tại";
                    return View();
                }

                db.NhaSanXuats.Add(nsx);
                db.SaveChanges();

                return RedirectToAction("DanhSach");
            }

            return View();
        }

        public ActionResult Sua(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("ThongKe", "TrangChu");
            }

            var nsx = db.NhaSanXuats.First(x => x.ID == id);

            return View(nsx);
        }

        [HttpPost]
        public ActionResult Sua(NhaSanXuat nsx)
        {
            var nsxMoi = db.NhaSanXuats.First(x => x.ID == nsx.ID);
            nsxMoi.TenNhaSanXuat = nsx.TenNhaSanXuat;

            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }


        public ActionResult Xoa(int id)
        {
            var nsx = db.NhaSanXuats.First(x => x.ID == id);
            nsx.BiXoa = true;

            db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult KichHoat(int id)
        {
            var nsx = db.NhaSanXuats.First(x => x.ID == id);
            nsx.BiXoa = false;

            db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult TimKiem(string q, int page = 1)
        {
            q = q.Trim().ToLower();

            var danhSach = from x in db.NhaSanXuats
                select x;

            if (!string.IsNullOrEmpty(q)) 
            { 
                danhSach = danhSach
                    .Where(x => x.TenNhaSanXuat.ToLower().Contains(q) 
                    || x.TenNhaSanXuat.ToLower().Contains(q)
                    || x.TenNhaSanXuat.ToLower().StartsWith(q) 
                    || x.TenNhaSanXuat.ToLower().StartsWith(q)); 

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
    }
}