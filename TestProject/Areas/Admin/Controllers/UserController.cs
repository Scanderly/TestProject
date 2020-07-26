using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Areas.Admin.Controllers
{
    //[AdminLevel]
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
       
            List<User> users = new List<User>();
            using (TestProjectEntities db = new TestProjectEntities())
            {
                users = db.Users.OrderBy(m => m.Id).ToList();
                
            }
            return View(users);
        }
        //[HttpGet]
        //public ActionResult ChangeState()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ChangeState(int?id )
        //{
        //    if (id == null)
        //    {
        //        return new HttpNotFoundResult();
        //    }
        //    using(TestProjectEntities db=new TestProjectEntities())
        //    {
        //        User user = db.Users.Find(id);
        //        if (user != null)
        //        {
        //            if (user.IsBlocked == true)
        //            {
        //                user.IsBlocked = false;
        //            }
        //            user.IsBlocked = true;
        //        }
        //        db.SaveChanges();
        //    }
           
        //    return RedirectToAction("index", "user");
        //}
      
        [HttpGet]
        public ActionResult Update()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Update([Bind(Include = "Id,Name,Surname,IsAdmin,Email,IsBlocked")]User user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TestProjectEntities db = new TestProjectEntities())
            {
                User usr = db.Users.Find(user.Id);
                usr.Name = user.Name;
                usr.Email = user.Email;
                usr.Surname = user.Surname;
                usr.IsAdmin = user.IsAdmin;
                usr.IsBlocked = user.IsBlocked;
                db.SaveChanges();
            }
            return RedirectToAction("index");
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