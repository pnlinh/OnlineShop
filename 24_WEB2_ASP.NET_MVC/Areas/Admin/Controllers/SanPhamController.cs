using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;
using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class SanPhamController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
      
        // GET: Admin/SanPham/DanhSach
        public ActionResult DanhSach(int page = 1)
        {
            int n = db.SanPhams.Count();

            int rowPerPage = 10;

            int totalPage = (int)Math.Ceiling((double)n / rowPerPage);

            ViewBag.totalPage = totalPage;
            ViewBag.curPage = page;

            var danhSach = db.SanPhams.
                OrderByDescending(x => x.ID)
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

            ViewBag.LoaiSanPhams = db.LoaiSanPhams
                .Where(x => x.BiXoa == false)
                .ToList();

            ViewBag.NhaSanXuats = db.NhaSanXuats
                .Where(x => x.BiXoa == false)
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(SanPhamVM model, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                SanPham sp = new SanPham
                {
                    idDanhMuc = model.idDanhMuc,
                    idLoaiSanPham = model.idLoaiSanPham,
                    idNhaSanXuat = model.idNhaSanXuat,
                    TenSanPham = model.TenSanPham,
                    MoTa = model.MoTa,
                    Gia = Int32.Parse(model.Gia),
                    SoLuong = model.SoLuong,
                    SoLuongTon = model.SoLuong,
                    SoLuongBan = 0,
                    NgayNhap = DateTime.Now,
                    NoiDung = HttpUtility.HtmlEncode(model.NoiDung),
                    LuotXem = 0,
                    BiXoa = false,
                    TinhTrang = true
                };

                var kiemTra = db.SanPhams.FirstOrDefault(x => x.TenSanPham == model.TenSanPham);
                if (kiemTra != null)
                {
                    ViewBag.Loi = "Tên sản phẩm đã tồn tại";

                    ViewBag.DanhMucs = db.Danh_Mucs
                        .Where(x => x.BiXoa == false)
                        .ToList();

                    ViewBag.LoaiSanPhams = db.LoaiSanPhams
                        .Where(x => x.BiXoa == false)
                        .ToList();

                    ViewBag.NhaSanXuats = db.NhaSanXuats
                        .Where(x => x.BiXoa == false)
                        .ToList();

                    return View();
                }

                db.SanPhams.Add(sp);
                db.SaveChanges();

                int lstID = sp.ID;
                
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    string dateCur = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string extention = Path.GetExtension(HinhAnh.FileName);
                    string fileName = Path.GetFileName(dateCur + "_" + StringUtils.convertToUnSign(sp.TenSanPham) + extention);

                    string dirPath = Server.MapPath("~/Assets/HinhAnh");
                    string targetDirPath = Path.Combine(dirPath, lstID.ToString());
                    Directory.CreateDirectory(targetDirPath);

                    string path = Path.Combine(targetDirPath, fileName);

                    HinhAnh.SaveAs(path);
                    sp.HinhAnh = fileName;

                    db.SaveChanges();
                }

                return RedirectToAction("DanhSach");
            }

            ViewBag.DanhMucs = db.Danh_Mucs
                .Where(x => x.BiXoa == false)
                .ToList();

            ViewBag.LoaiSanPhams = db.LoaiSanPhams
                .Where(x => x.BiXoa == false)
                .ToList();

            ViewBag.NhaSanXuats = db.NhaSanXuats
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

            ViewBag.LoaiSanPhams = db.LoaiSanPhams
                .Where(x => x.BiXoa == false)
                .ToList();

            ViewBag.NhaSanXuats = db.NhaSanXuats
                .Where(x => x.BiXoa == false)
                .ToList();

            var sp = db.SanPhams
                .Single(x => x.ID == id);

            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(SanPham sp, HttpPostedFileBase HinhAnh)
        {
            var spMoi = db.SanPhams.First(x => x.ID == sp.ID);
            spMoi.TenSanPham = sp.TenSanPham;
            spMoi.MoTa = sp.MoTa;
            spMoi.Gia = sp.Gia;
            spMoi.SoLuong = sp.SoLuong;
            spMoi.NoiDung = HttpUtility.HtmlEncode(sp.NoiDung);
            spMoi.idDanhMuc = sp.idDanhMuc;
            spMoi.idLoaiSanPham = sp.idLoaiSanPham;
            spMoi.idNhaSanXuat = sp.idNhaSanXuat;
            spMoi.SoLuongTon = spMoi.SoLuong - spMoi.SoLuongBan;

            db.SaveChanges();

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                string fileNameCur = spMoi.HinhAnh;
                string pathCur = Path.Combine(Server.MapPath("~/Assets/HinhAnh/" + spMoi.ID), fileNameCur);
                if (System.IO.File.Exists(pathCur))
                {
                    System.IO.File.Delete(pathCur);
                }

                string dateCur = DateTime.Now.ToString("yyyyMMddHHmmss");
                string extention = Path.GetExtension(HinhAnh.FileName);
                string fileName = Path.GetFileName(dateCur + "_" + StringUtils.convertToUnSign(sp.TenSanPham) + extention);               

                string dirPath = Server.MapPath("~/Assets/HinhAnh");
                string targetDirPath = Path.Combine(dirPath, sp.ID.ToString());
                Directory.CreateDirectory(targetDirPath);

                string path = Path.Combine(targetDirPath, fileName);

                HinhAnh.SaveAs(path);
                spMoi.HinhAnh = fileName;

                db.SaveChanges();
            }

            return RedirectToAction("DanhSach");
        }

        public ActionResult Xoa(int id)
        {
            var sp = db.SanPhams.First(x => x.ID == id);
            sp.BiXoa = true;

            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult KichHoat(int id)
        {
            var sp = db.SanPhams.First(x => x.ID == id);
            sp.BiXoa = false;

            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult TimKiem(string q, int page = 1)
        {
            q = q.Trim().ToLower();

            var danhSach = from x in db.SanPhams
                       select x;

            if (!string.IsNullOrEmpty(q))
            {
                danhSach = danhSach
                    .Where(x => x.TenSanPham.ToLower().Contains(q)
                || x.TenSanPham.ToLower().Contains(q)              
                || x.TenSanPham.ToLower().StartsWith(q)
                || x.TenSanPham.ToLower().StartsWith(q));


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