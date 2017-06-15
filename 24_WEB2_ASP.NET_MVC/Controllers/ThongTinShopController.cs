using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Controllers
{
    public class ThongTinShopController : Controller
    {
        // GET: ThongTinShop
        public ActionResult ThongTin()
        {
            return View();
        }
        public ActionResult SuKien()
        {
            return View();
        }
    }
}