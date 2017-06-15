using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "ThongKe", id = UrlParameter.Optional },
                new string[] { "_24_WEB2_ASP.NET_MVC.Areas.Admin.Controllers" }
            );
        }
    }
}