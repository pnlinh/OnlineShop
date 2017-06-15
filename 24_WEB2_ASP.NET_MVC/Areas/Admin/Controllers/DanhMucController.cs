using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    [CheckLogin]
    public class DanhMucController : Controller
    {      
        private OnlineShopEntities db = new OnlineShopEntities();

        // GET: Admin/DanhMuc/DanhSach
        public ActionResult DanhSach()
        {            
            var danhSach = db.Danh_Mucs.ToList().OrderByDescending(x => x.ID);

            return View(danhSach);
        }

        public ActionResult Them()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Them(DanhMucVM model)
        {           
            if (ModelState.IsValid)
            {
                Danh_Muc dm = new Danh_Muc
                {
                    TenDanhMuc = model.TenDanhMuc
                };

                var kiemTra = db.Danh_Mucs.FirstOrDefault(x => x.TenDanhMuc == model.TenDanhMuc);
                if (kiemTra != null)
                {                   
                    ViewBag.Loi = "Tên danh mục đã tồn tại";
                    return View();
                }

                db.Danh_Mucs.Add(dm);
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

            var dm = db.Danh_Mucs.First(x => x.ID == id);

            return View(dm);
        }

        [HttpPost]
        public ActionResult Sua(Danh_Muc dm)
        {
            var dmMoi = db.Danh_Mucs.First(x => x.ID == dm.ID);

            dmMoi.TenDanhMuc = dm.TenDanhMuc;

            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult Xoa(int id)
        {
            var dm = db.Danh_Mucs.First(x => x.ID == id);
            dm.BiXoa = true;

            db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }

        public ActionResult KichHoat(int id)
        {
            var dm = db.Danh_Mucs.First(x => x.ID == id);
            dm.BiXoa = false;

            db.Entry(dm).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhSach");
        }
    }
}