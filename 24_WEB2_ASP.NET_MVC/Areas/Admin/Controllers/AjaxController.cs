using _24_WEB2_ASP.NET_MVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers
{
    public class AjaxController : Controller
    {
        private OnlineShopEntities db = new OnlineShopEntities();

        [HttpPost]
        public ActionResult LoaiSanPham(int? id)
        {
            var lsp = db.LoaiSanPhams.Where(x => x.idDanhMuc == id).ToList();

            return PartialView(lsp);
        }

        [HttpPost]
        public ActionResult KiemTraEmail(string email)
        {
            if (email == "")
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var tk = db.TaiKhoans
                .Where(x => x.Email == email)
                .Select(x => new
                {
                    x.Email
                }
                )
                .FirstOrDefault();

            if (tk == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            return Json(tk, JsonRequestBehavior.AllowGet);
        }
    }
}