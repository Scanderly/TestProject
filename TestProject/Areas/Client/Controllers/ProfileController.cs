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
            List<MenuList> menulist = new List<MenuList>();
            using (TestProjectEntities db = new TestProjectEntities())
            {
                menulist = db.MenuLists.ToList();
            }
            return PartialView(menulist);
        }
    }
}