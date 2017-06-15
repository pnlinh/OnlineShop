using System.Linq;
using System.Web.Mvc;
using _24_WEB2_ASP.NET_MVC.Areas.Admin.Models;
using _24_WEB2_ASP.NET_MVC.Helpers;
using _24_WEB2_ASP.NET_MVC.Models;
using DangNhapVM = _24_WEB2_ASP.NET_MVC.Areas.Admin.Models.DangNhapVM;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();

        // GET: Admin/DangNhap
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DangNhapVM model)
        {
            TaiKhoan admin = null;

            if (!string.IsNullOrEmpty(model.Pass))
            {
                var matKhauMd5 = StringUtils.Md5(model.Pass);
                admin = db.TaiKhoans
                    .FirstOrDefault(x => x.Email == model.Email && x.Pass == matKhauMd5 && x.PhanQuyen);
            }

            if (admin != null)
            {
                Session["dangnhap"] = 1;
                Session["admin"] = admin;

                return RedirectToAction("ThongKe", "TrangChu");
            }

            ViewBag.Loi = "Đăng nhập thất bại";
            return View();
        }

        
        public ActionResult DangXuat()
        {
            CurrentContext.Xoa();

            return RedirectToAction("Index", "DangNhap");
        }
    }
}