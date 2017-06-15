using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Models;
namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    public class SanPhamController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
        // GET: SanPham
        [HttpGet]
        public ActionResult ChiTiet(int? id)
        {
            var link = string.Format(
    "http://www.facebook.com/sharer.php?s=100&p%5Burl%5D={0}&p%5Btitle%5D={1}",
    Url.Encode("http://mysite.xyz"),
    Url.Encode("This is my site")
);
            
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                ViewBag.Link = link;
                var product = db.SanPhams.Where(p => p.ID == id).FirstOrDefault();
                ViewBag.IdProduct = id;
                if (product == null)
                {
                    return new HttpNotFoundResult();
                }
                if (product != null)
                {

                    product.LuotXem = product.LuotXem + 1;
                    db.SaveChanges();
                }
                var model = new IndexViewModel();
                model.SanPham = db.SanPhams.ToList();
                model.SanPhamYeuThich = db.SanPhamYeuThiches.ToList();
                model.Details = product;
                return View(model);
            }
        }
        public ActionResult _Header()
        {
            var list = new IndexViewModel();
            list.DanhMuc = db.Danh_Mucs.ToList();
            list.ChiTiet=db.ChiTiet_HoaDon.ToList();
            if (Session["UserID"] != null) {
                var getName = Session["UserEmail"].ToString();
                int count = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Count();
                ViewBag.cartCount = count;
                list.Count = ViewBag.cartCount;
                var Gia = db.ChiTiet_HoaDon.Where(s => s.Email == getName && (s.idHoaDon == null)).Select(i => i.DonGia*i.SoLuong).Sum();
                //ViewBag.Total = Gia;
                list.DonGia = Convert.ToInt32(Gia);
                int countwish = db.SanPhamYeuThiches.Where(s=>s.TaiKhoan.Email==getName).Count();
                ViewBag.listwish = countwish;
            }
            return PartialView(list);
        }
    }
}