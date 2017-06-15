using System.Web.Mvc;

namespace _24_WEB2_ASP.NET_MVC.Helpers
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!CurrentContext.DaDangNhap())
            {
                filterContext.Result = new RedirectResult("~/Admin/DangNhap/Index");

                return;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}