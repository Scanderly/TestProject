using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Filters
{
    public class UserLevel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Login"] == null)
            {
                filterContext.Result = new RedirectResult("~/client/home");
                return;
            }

            User user = HttpContext.Current.Session["User"] as User;
            if (user.IsAdmin == true)
            {
                filterContext.Result = new RedirectResult("~/admin");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
    
}