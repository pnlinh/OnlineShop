using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    
    public class LichSuController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
        // GET: LichSu
        [HttpGet]
        public ActionResult HoaDon(IndexViewModel model, int page=1)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                int idkh = Convert.ToInt32(Session["UserID"]);
                var authors = db.HoaDons.Where(s => s.idKhachHang == idkh).ToList();
                int n = authors.Count();
                ViewBag.listproduct = authors.Count();
                int perPage = 9;
                int totalPage = (int)Math.Ceiling((double)n / perPage);
                ViewBag.totalPage = totalPage;
                ViewBag.curPage = page;
                model.ListHoaDon = authors
                    .OrderBy(s => s.ID)
                    .Skip((page - 1) * perPage)
                    .Take(perPage).ToList();
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult ChiTietHoaDon(int id)
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                var model = new IndexViewModel();
                var hd = db.HoaDons.Where(s => s.ID == id).FirstOrDefault();
                model.ChiTiet = db.ChiTiet_HoaDon.Where(s=>s.idHoaDon==id).ToList();
                var dc = db.DiaChiGiaoHangs.Where(s=>s.HoaDon.ID==id).FirstOrDefault();
                if(hd==null || dc == null)
                {
                    return new HttpNotFoundResult();
                }
                model.HoaDon = hd;
                model.GiaoHang = dc;
                return View(model);

            }
        }
        public ActionResult YeuThich(IndexViewModel model)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            else
            {
                model.SanPhamYeuThich = db.SanPhamYeuThiches.ToList();
                return View(model);
            }
        }
    }
}