using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestProject.Models;

namespace TestProject.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Client/Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DisplayMenu()
        {
            List<MenuList> menulist = new List<MenuList>();
            using(TestProjectEntities db=new TestProjectEntities())
            {
                menulist = db.MenuLists.ToList();
            }
            return PartialView(menulist);
        }
    }
}