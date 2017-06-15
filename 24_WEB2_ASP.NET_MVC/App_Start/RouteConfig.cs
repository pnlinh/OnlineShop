using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _24_WEB2_ASP.NET_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "_24_WEB2_ASP.NET_MVC.Controllers" }
            );

            routes.MapRoute(
            name: "MuaSam",
             url: "{ controller}/{ action}/{id}",
             defaults: new { controller = "MuaSam", action = "SanPham", id = UrlParameter.Optional }
         );
            routes.MapRoute(
           name: "LichSu",
            url: "{ controller}/{ action}/{id}",
            defaults: new { controller = "LichSu", action = "ChiTietHoaDon", id = UrlParameter.Optional }
        );
        }
    }
}