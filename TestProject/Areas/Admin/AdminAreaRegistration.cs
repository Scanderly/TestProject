using System.Web.Mvc;

namespace TestProject.Areas.Admin
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
                new {controller="dashboard", action = "Index", id = UrlParameter.Optional },
                 new[] { "TestProject.Areas.Admin.Controllers" }
            );
        }
    }
}