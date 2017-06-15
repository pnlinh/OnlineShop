using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Models;
namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    public class MuaSamController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
        static int? ParseInt32(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
        public ActionResult SanPham(string search, string id, string minmoney, string maxmoney, string nsx, string dm, string tlsp, int page = 1)
        {
            
            var nsx1 = ValueProvider.GetValue("nsx");
            var dm1 = ValueProvider.GetValue("dm");
            var tlsp1 = ValueProvider.GetValue("tlsp");
            if (nsx1 != null)
            {
                nsx = nsx1.AttemptedValue;
            }
            if (dm1 != null)
            {
                dm = dm1.AttemptedValue;
            }
            if (tlsp1 != null)
            {
                tlsp = tlsp1.AttemptedValue;
            }

            ViewBag.Search = search;
            TimKiem mod = new TimKiem();
            if (Session["UserEmail"] != null)
            {
                var getname = Session["UserEmail"].ToString();
                mod.SanPhamYeuThich = db.SanPhamYeuThiches.Where(s => s.TaiKhoan.Email == getname &&(s.BiXoa==false)).ToList();
            }
            mod.DanhMuc = db.Danh_Mucs.ToList();
            mod.LoaiSanPham = db.LoaiSanPhams.Where(s => (s.BiXoa == false)).ToList();

            mod.NhaSanXuat = db.NhaSanXuats.Where(s => (s.BiXoa == false)).ToList();
            ViewBag.s = search;
            ViewBag.sanxuat = nsx;
            ViewBag.danhmucsp = dm;
            ViewBag.tloaisp = tlsp;
            ViewBag.Minmoney = minmoney;
            ViewBag.Maxmoney = maxmoney;
            var authors = from a in db.SanPhams where (a.BiXoa==false) select a;
            var c = db.SanPhams.Where(a => (a.BiXoa == false)).OrderByDescending(a => a.Gia).FirstOrDefault();
            ViewBag.maxvalue = c.Gia;
            var d = db.SanPhams.Where(a => (a.BiXoa == false)).OrderBy(a => a.Gia).FirstOrDefault();
            ViewBag.minvalue = d.Gia;
            LoaiSanPham lsp = new LoaiSanPham();
            if (!String.IsNullOrEmpty(search))
            {
                authors = authors.Where(s => s.TenSanPham.Contains(search) && (s.BiXoa == false));
            }
            if (id != null)
            {
                authors = authors.Where(s => s.LoaiSanPham.ID.ToString() == id && (s.BiXoa == false));
            }
            if (String.IsNullOrEmpty(search) && id == null)
            {
                if (string.IsNullOrEmpty(minmoney) && string.IsNullOrEmpty(maxmoney))
                {


                    if (String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {

                        authors = authors.Where(s => s.Danh_Muc.TenDanhMuc == dm && (s.LoaiSanPham.TenLoaiSanPham == tlsp) && (s.BiXoa == false));
                    }

                    if (!String.IsNullOrEmpty(nsx) || !String.IsNullOrEmpty(dm) || !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx || s.Danh_Muc.TenDanhMuc == dm || s.LoaiSanPham.TenLoaiSanPham == tlsp && (s.BiXoa == false));

                    }
                    if (!String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx && (s.LoaiSanPham.TenLoaiSanPham == tlsp) && (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx && (s.Danh_Muc.TenDanhMuc == dm) && (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s=> (s.BiXoa == false));
                    }
                }
                if (!String.IsNullOrEmpty(minmoney) && !String.IsNullOrEmpty(maxmoney))
                {

                    int min = int.Parse(minmoney);
                    int max = int.Parse(maxmoney);
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) || !String.IsNullOrEmpty(dm) || !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx || s.Danh_Muc.TenDanhMuc == dm || s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));

                    }
                    if (!String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                }
                if (!String.IsNullOrEmpty(minmoney) && String.IsNullOrEmpty(maxmoney))
                {
                    maxmoney = c.Gia.ToString();
                    int min = int.Parse(minmoney);
                    int max = int.Parse(maxmoney);
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) || !String.IsNullOrEmpty(dm) || !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx || s.Danh_Muc.TenDanhMuc == dm || s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));

                    }
                    if (!String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                }
                if (String.IsNullOrEmpty(minmoney) && !String.IsNullOrEmpty(maxmoney))
                {
                    minmoney = d.Gia.ToString();
                    int min = int.Parse(minmoney);
                    int max = int.Parse(maxmoney);
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) || !String.IsNullOrEmpty(dm) || !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx || s.Danh_Muc.TenDanhMuc == dm || s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));

                    }
                    if (!String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (!String.IsNullOrEmpty(nsx) && !String.IsNullOrEmpty(dm) && !String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.NhaSanXuat.TenNhaSanXuat == nsx).Where(s => s.Danh_Muc.TenDanhMuc == dm).Where(s => s.LoaiSanPham.TenLoaiSanPham == tlsp).Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                    if (String.IsNullOrEmpty(nsx) && String.IsNullOrEmpty(dm) && String.IsNullOrEmpty(tlsp))
                    {
                        authors = authors.Where(s => s.Gia >= min && s.Gia <= max).Where(s => (s.BiXoa == false));
                    }
                }
            }
            int n = authors.Count();
            ViewBag.listproduct = authors.Count();
            int perPage = 9;
            int totalPage = (int)Math.Ceiling((double)n / perPage);
            ViewBag.totalPage = totalPage;
            ViewBag.curPage = page;
            mod.SanPham = authors
                .OrderBy(s => s.ID)
                .Skip((page - 1) * perPage)
                .Take(perPage).ToList();
            return View(mod);
        }
        [HttpGet]
        public ActionResult ThanhToan()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");
                var model = new IndexViewModel();
                model.SanPham = db.SanPhams.ToList();
                if (Session["UserID"] != null)
                {
                    var getName = Session["UserEmail"].ToString();
                    var productcart = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).ToList();
                    model.ChiTiet = productcart;
                    ViewBag.error = productcart;
                    var Gia = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Select(i => i.DonGia).Sum();
                    ViewBag.Total = Gia;
                    int count = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Count();
                    ViewBag.checkCount = count;
                    model.DonGia = Convert.ToInt32(Gia);
                    model.TaiKhoan = db.TaiKhoans.Where(s => s.Email == getName).FirstOrDefault();
                    if (model.DonGia == 0)
                    {
                        return RedirectToAction("Index", "TrangChu");
                    }
                }
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThanhToan(string Command, IndexViewModel model)
        {
            ViewBag.Country = new SelectList(db.ThanhPhoes, "ID", "TenThanhPho");

            try
            {
                if (Session["UserID"] != null)
                {
                    var getName = Session["UserEmail"].ToString();
                    var getId = Convert.ToInt32(Session["UserID"]);
                    if (ModelState.IsValid)
                    {

                        if (Request.Form["Command2"] != null)
                        {

                            TrangThai_HoaDon tt = new TrangThai_HoaDon();
                            var count = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Select(s => s.SoLuong).Sum();
                            var total = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Select(i => i.DonGia * i.SoLuong).Sum();
                            HoaDon hd = new HoaDon
                            {
                                idKhachHang = Convert.ToInt32(Session["UserID"]),
                                NgayDat = DateTime.Now,
                                TongTien = total,
                                TrangThai = 1,
                                SoLuong = count
                            };
                            db.HoaDons.Add(hd);
                            db.SaveChanges();
                            var top = db.HoaDons.Where(d => d.TrangThai_HoaDon.ID == 1).OrderByDescending(d => d.ID).Select(d => d.ID).FirstOrDefault();
                            //var ad = db.TaiKhoans.Where(s => s.Email == getName).Select(s=>new {s.ThanhPho.TenThanhPho,s.Address }).FirstOrDefault();
                            //ViewBag.add = String.Concat(ad.Address,ad.TenThanhPho);
                            DiaChiGiaoHang dc = new DiaChiGiaoHang
                            {
                                TenKhachHang = model.ThanhToan.TenKhachHang,
                                DiaChi = model.ThanhToan.DiaChi,
                                Email = model.ThanhToan.Email,
                                Phone = model.ThanhToan.Phone,
                                YeuCau = model.ThanhToan.YeuCau,
                                NgayDatHang = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"),
                                TongTien = total,
                                idHoaDon = top,
                                idTaiKhoan = getId
                            };
                            db.DiaChiGiaoHangs.Add(dc);
                            var ct = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).ToList();
                            foreach (var item in ct)
                            {
                                item.idHoaDon = top;
                            }
                            db.SaveChanges();
                            var a = db.HoaDons.Where(s => s.TaiKhoan.Email == getName).OrderByDescending(s => s.ID).FirstOrDefault();
                            model.HoaDon = a;
                        }
                    }

                }
                return View("../MuaSam/ThanhCong", model);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        [HttpPost]
        public JsonResult GetStates(string id, FormCollection formCollection)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            var stateList = this.Getstate(Convert.ToInt32(id));
            var stateData = stateList.Select(m => new SelectListItem()
            {
                Text = m.TenHuyen,
                Value = m.ID.ToString(),
            });
            return Json(stateData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public IList<Huyen> Getstate(int CountryId)
        {
            return db.Huyens.Where(m => m.idThanhPho == CountryId).ToList();
        }
        public ActionResult GioHang()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                var model = new IndexViewModel();
                model.ChiTiet = db.ChiTiet_HoaDon.ToList();
                if (Session["UserID"] != null)
                {
                    var getName = Session["UserEmail"].ToString();
                    int count = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Count();
                    ViewBag.cartCount = count;
                    model.Count = ViewBag.cartCount;
                    var Gia = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Select(i => i.DonGia).Sum();
                    ViewBag.Total = Gia;
                }
                return View(model);
            }
        }

        public ActionResult ThanhCong(IndexViewModel model)
        {
            if (Session["UserID"] != null)
            {
                string getName = Session["UserEmail"].ToString();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "TrangChu");
            }
        }

    }
}