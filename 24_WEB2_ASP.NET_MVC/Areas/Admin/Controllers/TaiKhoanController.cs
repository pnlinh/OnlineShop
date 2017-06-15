using System;
using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class TaiKhoanController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();

        // GET: Admin/TaiKhoan
        public ActionResult DanhSach(int page = 1)
        {
            int n = db.TaiKhoans.Count();

            int rowPerPage = 10;

            int totalPage = (int)Math.Ceiling((double)n / rowPerPage);

            ViewBag.totalPage = totalPage;
            ViewBag.curPage = page;

            var danhSach = db.TaiKhoans
                .Where(x => x.Email != "admin@gmail.com")
                .OrderBy(x => x.ID)
                .Skip((page - 1) * rowPerPage)
                .Take(rowPerPage)
                .ToList();

            return View(danhSach);
        }

        public ActionResult Them()
        {
            return View();
        }

        public ActionResult Sua(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("ThongKe", "TrangChu");
            }

            var tk = db.TaiKhoans.First(x => x.ID == id);

            return View(tk);
        }

        [HttpPost]
        public ActionResult Sua(TaiKhoan tk)
        {
            var tkMoi = db.TaiKhoans.First(x => x.ID == tk.ID);

            tkMoi.PhanQuyen = tk.PhanQuyen;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult KichHoat(int id)
        {
            var tk = db.TaiKhoans.First(x => x.ID == id);
            tk.BiXoa = false;

            db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult Xoa(int id)
        {
            var tk = db.TaiKhoans.First(x => x.ID == id);
            tk.BiXoa = true;

            db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

//        public ActionResult ThemAdmin()
//        {
//            return View();
//        }
//
//        [HttpPost]
//        public ActionResult ThemAdmin(AdminVM model)
//        {
//            if (ModelState.IsValid)
//            {
//                TaiKhoan tk = new TaiKhoan
//                {
//                    Email = model.Email,
//                    Pass = StringUtils.Md5(model.Pass),
//                    Name = model.Name,
//                    PhanQuyen = true,
//                    Phone = null,
//                    idThanhPho = null,
//                    Address = null,
//                    BiXoa = false
//                };
//
//                db.TaiKhoans.Add(tk);
//                db.SaveChanges();
//
//                return RedirectToAction("ThongKe", "TrangChu");
//            }
//            return View();
//        }       

        public ActionResult TimKiem(string q, int page = 1)
        {
            q = q.Trim().ToLower();           

            var danhSach = from x in db.TaiKhoans
                       select x;

            if (!string.IsNullOrEmpty(q))
            {
                danhSach = danhSach
                    .Where(x => x.Name.ToLower().Contains(q)
                || x.Name.ToLower().Contains(q)
                || x.Name.ToLower().StartsWith(q)
                || x.Name.ToLower().StartsWith(q)
                || x.Email.ToLower().Contains(q)
                || x.Email.ToLower().Contains(q)
                || x.Email.ToLower().StartsWith(q)
                || x.Email.ToLower().StartsWith(q)
                || x.Phone.ToLower().Contains(q)
                || x.Phone.ToLower().Contains(q)
                || x.Phone.ToLower().StartsWith(q)
                || x.Phone.ToLower().StartsWith(q));


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
    
        public ActionResult ThongTinAdmin(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("ThongKe", "TrangChu");
            }

            var admin = db.TaiKhoans
                .Where(x => x.PhanQuyen)
                .Single(x => x.ID == id);

            return View(admin);
        }

        [HttpPost]
        public ActionResult ThongTinAdmin(TaiKhoan admin)
        {
            var adminMoi = db.TaiKhoans.Where(x => x.PhanQuyen).First(x => x.ID == admin.ID);

            adminMoi.Name = admin.Name;

            if (admin.Pass != null)
            {
                adminMoi.Pass = StringUtils.Md5(admin.Pass);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "DangNhap");
        }
    }
}