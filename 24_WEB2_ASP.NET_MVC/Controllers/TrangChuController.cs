using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Models;
namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    public class TrangChuController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
        // GET: TrangChu của người dùng
        public ActionResult Index(IndexViewModel model)
        {

            if (Session["UserName"] != null)
            {
                string getName = Session["UserEmail"].ToString();
                ViewBag.userName = getName;
                int count = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Count();
                ViewBag.cartCount = count;
            }
            model.SanPham = db.SanPhams.ToList();
            model.SanPhamYeuThich = db.SanPhamYeuThiches.ToList();
            return View(model);
        }
        public IList<SanPham> getAllItems()
        {
            IList<SanPham> items = new List<SanPham>();
            items = db.SanPhams.ToList();
            return items;
        }
        public JsonResult AddCart(int? ItemId)
        {
            int count;
            string username = Session["UserEmail"].ToString();
            var author = db.SanPhams.Where(s => s.ID == ItemId).Select(s => new {s.ID, s.SoLuong, s.Gia, s.HinhAnh,s.TenSanPham }).FirstOrDefault();
            var a = db.ChiTiet_HoaDon.Where(s => s.idSanPham == ItemId && (s.Email == username) &&(s.idHoaDon==null)).FirstOrDefault();
            try
            {
                
                if (a == null)
                {
                    
                    ChiTiet_HoaDon objcart = new ChiTiet_HoaDon()
                    {
                        Email = username,
                        idSanPham = ItemId,
                        SoLuong = 1,
                        DonGia = author.Gia
                    };
                    db.ChiTiet_HoaDon.Add(objcart);

                }
                if (a != null)
                {
                    a.SoLuong++;
                    if (a.SoLuong > a.SanPham.SoLuongTon)
                    {
                        a.SoLuong = Convert.ToInt32(a.SanPham.SoLuongTon);
                    }
                    //a.DonGia = a.SoLuong * a.SanPham.Gia;

                }
                db.SaveChanges();

                count = db.ChiTiet_HoaDon.Where(s => s.Email == username).Count();
                return Json(author, JsonRequestBehavior.AllowGet);

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
        public JsonResult AddWihsList(int? id)
        {
            var author = db.SanPhams.Where(s => s.ID == id).Select(s => new { s.ID, s.SoLuong, s.Gia, s.HinhAnh, s.TenSanPham }).FirstOrDefault();
            int idUser = Convert.ToInt32(Session["UserID"]);
            var dis = db.SanPhamYeuThiches.Where(s => s.idSanPham == id).Select(s => new {s.idSanPham}).FirstOrDefault();
            try
            {
                SanPhamYeuThich obj = new SanPhamYeuThich()
                {
                    idKhachHang = idUser,
                    idSanPham = id
                };
                db.SanPhamYeuThiches.Add(obj);
                db.SaveChanges();
                return Json(author, JsonRequestBehavior.AllowGet);
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
            public JsonResult LoadCart()
        {
            try
            {
                string username = Session["UserEmail"].ToString();
                var item = db.ChiTiet_HoaDon.Where(s => s.Email == username && (s.idHoaDon == null)).Select(s => new { s.ID, s.Email, s.DonGia, s.idSanPham, s.SanPham.HinhAnh, s.SanPham.TenSanPham, s.SanPham.Gia, s.SoLuong,s.SanPham.SoLuongTon }).ToList();
                return Json(item, JsonRequestBehavior.AllowGet);
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

        public JsonResult LoadNumber(int? id) {
            try
            {
                string username = Session["UserEmail"].ToString();
                var item = db.ChiTiet_HoaDon.Where(s => s.Email == username &&(s.SanPham.ID==id) && (s.idHoaDon == null)).Select(s => new { SoLuong=s.SoLuong,SoLuongSanPham=s.SanPham.SoLuong}).FirstOrDefault();
                return Json(item, JsonRequestBehavior.AllowGet);
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
        /*public PartialViewResult GetCartItems()
        {
            string username = Session["UserEmail"].ToString();
            int sum = 0;
            var GetItemsId = db.ChiTiet_HoaDon.Where(s => s.Email == username).Select(u => u.idSanPham).ToList();
            var GetCartItem = from itemList in db.SanPhams where GetItemsId.Contains(itemList.ID) select itemList;
            foreach (var totalsum in GetCartItem)
            {
                sum = sum + (int)totalsum.Gia;
            }
            ViewBag.Total = sum;
            return PartialView("_Header", GetCartItem);

        }*/
        public string DeleteCart(int? itemId)
        {
            string getName = Session["UserEmail"].ToString();
            var removeCart = db.ChiTiet_HoaDon.Where(s => s.ID == itemId && (s.Email == getName)).FirstOrDefault();
            try
            {
                db.ChiTiet_HoaDon.Remove(removeCart);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return itemId.ToString();
        }

        public string DeleteWishList(int? itemId)
        {
            string getName = Session["UserEmail"].ToString();
            var removeCart = db.SanPhamYeuThiches.Where(s => s.ID == itemId && (s.TaiKhoan.Email == getName)).FirstOrDefault();
            try
            {
                db.SanPhamYeuThiches.Remove(removeCart);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            return itemId.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart( FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var getname = Session["UserEmail"].ToString();
                var list = db.ChiTiet_HoaDon
                    .Where(s => s.Email == getname && (s.idHoaDon == null))
                    .ToList();
                int a, b;
                string[] num = form.GetValues("numbercart");
                for (int i = 0; i < list.Count(); i++)
                {
                    if (!String.IsNullOrEmpty(num[i]))
                    {
                        a = int.Parse(num[i]);
                        b = list[i].SanPham.ID;

                        var quan = db.SanPhams.Where(s => (s.ID == b) && (s.SoLuong >= a)).FirstOrDefault();

                        if (quan == null)
                        {
                            ModelState.AddModelError("numbercart", "Số lượng không hợp lệ.");
                        }
                        else
                        {
                            list[i].SoLuong = int.Parse(num[i]);
                            db.SaveChanges();
                        }
                    }
                }
               
            }
            
            return RedirectToAction("GioHang","MuaSam");
        }

        public JsonResult loadModal(int? id)
        {
            var list = db.SanPhams.Where(s=>s.ID==id).Select(s=>new {s.TinhTrang,s.SoLuongTon,s.TenSanPham,s.NhaSanXuat.TenNhaSanXuat,s.MoTa,s.Gia,s.SoLuong,s.ID,s.HinhAnh,s.LoaiSanPham.TenLoaiSanPham }).FirstOrDefault();
            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
}