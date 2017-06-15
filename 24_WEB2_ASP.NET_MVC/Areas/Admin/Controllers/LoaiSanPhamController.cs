using System;
using _24_WEB2_ASP.NET_MVC.Models;
using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;
using _24_WEB2_ASP.NET_MVC.Helpers;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class LoaiSanPhamController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();        

        // GET: Admin/LoaiSanPham/DanhSach
        public ActionResult DanhSach(int page = 1)
        {            
            int n = db.LoaiSanPhams.Count();

            int rowPerPage = 10;

            int totalPage = (int)Math.Ceiling((double)n / rowPerPage);

            ViewBag.totalPage = totalPage;
            ViewBag.curPage = page;

            var danhSach = db.LoaiSanPhams
                .OrderByDescending(x => x.idDanhMuc)
                .Skip((page - 1) * rowPerPage)
                .Take(rowPerPage)
                .ToList();

            return View(danhSach);
        }

        public ActionResult Them()
        {
            ViewBag.DanhMucs = db.Danh_Mucs
                .Where(x => x.BiXoa == false)
                .ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Them(LoaiSanPhamVM model)
        {
            if (ModelState.IsValid)
            {
                LoaiSanPham lsp = new LoaiSanPham
                {
                    TenLoaiSanPham = model.TenLoaiSanPham,
                    idDanhMuc = model.idDanhMuc
                };

                var kiemTra = db.LoaiSanPhams.FirstOrDefault(x => x.TenLoaiSanPham == model.TenLoaiSanPham);
                if (kiemTra != null)
                {
                    ViewBag.Loi = "Tên loại sản phẩm đã tồn tại";

                    ViewBag.DanhMucs = db.Danh_Mucs
                        .Where(x => x.BiXoa == false)
                        .ToList();
                    return View();
                }

                db.LoaiSanPhams.Add(lsp);
                db.SaveChanges();

                return RedirectToAction("DanhSach");
            }

            ViewBag.DanhMucs = db.Danh_Mucs
                .Where(x => x.BiXoa == false)
                .ToList();

            return View();
        }

        public ActionResult Sua(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("ThongKe", "TrangChu");
            }

            ViewBag.DanhMucs = db.Danh_Mucs
                .Where(x => x.BiXoa == false)
                .ToList();

            var lsp = db.LoaiSanPhams.First(x => x.ID == id);

            return View(lsp);
        }

        [HttpPost]
        public ActionResult Sua(LoaiSanPham lsp)
        {
            var lspMoi = db.LoaiSanPhams.First(x => x.ID == lsp.ID);
            lspMoi.TenLoaiSanPham = lsp.TenLoaiSanPham;
            lspMoi.idDanhMuc = lsp.idDanhMuc;

            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult Xoa(int id)
        {
            var lsp = db.LoaiSanPhams.First(x => x.ID == id);
            lsp.BiXoa = true;

            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult KichHoat(int id)
        {
            var lsp = db.LoaiSanPhams.First(x => x.ID == id);
            lsp.BiXoa = false;

            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult TimKiem(string q, int page = 1)
        {
            q = q.Trim().ToLower();

            var danhSach = from x in db.LoaiSanPhams
                select x;

            if (!string.IsNullOrEmpty(q)) 
            { 
                danhSach = danhSach
                    .Where(x => x.TenLoaiSanPham.ToLower().Contains(q) 
                || x.TenLoaiSanPham.ToLower().Contains(q)
                || x.TenLoaiSanPham.ToLower().StartsWith(q)
                || x.TenLoaiSanPham.ToLower().StartsWith(q)); 

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