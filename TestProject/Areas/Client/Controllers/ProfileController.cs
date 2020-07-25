using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Areas.Client.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Client/Profile
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DisplayMenu()
        {
            List<Menu> menulist = new List<Menu>();
            using (TestProjectEntities db = new TestProjectEntities())
            {
                menulist = db.Menus.ToList();
            }
            return PartialView(menulist);
        }
    }
}