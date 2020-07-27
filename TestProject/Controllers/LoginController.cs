using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Client/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User usr)
        {
           
            if (usr.Email == null || usr.Password == null)
            {
                 Session["LogError"] = "This field required";
            }
           using(TestProjectEntities db=new TestProjectEntities())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == usr.Email);
                if (user!=null)
                {
                    if (user.IsBlocked == false)
                    {
                        if (Crypto.VerifyHashedPassword(user.Password, usr.Password))
                        {
                            user.ErrosCount = 0;
                            HttpContext.Session["Login"] = true;
                            HttpContext.Session["User"] = user;
                            user.LastLoginDate = DateTime.Now;
                            if (user.IsAdmin == false)
                            {
                                return RedirectToAction("index", "profile", new { Area = "Client" });
                            }
                            return RedirectToAction("index", "dashboard", new { Area = "Admin" });
                        }
                        Session["LoginError"] = "The email or password is not valid";
                        user.ErrosCount++;
                        db.SaveChanges();

                        if (user.ErrosCount ==3)
                        {
                            user.IsBlocked = true;
                            user.ErrosCount = 0;
                            db.SaveChanges();
                            return Content("Your account is blocked, please contact adminstrator");
                        }
                        
                        return View();
                    }
                    ModelState.AddModelError(string.Empty, "This email is not exist");
                    return RedirectToAction("register", "login");
                }
                return RedirectToAction("register", "login");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if(user.Name==null|| user.Surname == null || user.Email == null || user.Password == null)
            {
                Session["Register"] = "This fields are required";
            }
            using(TestProjectEntities db=new TestProjectEntities())
            {
                User usr = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.Name == user.Name && u.Surname == user.Surname);
                if (usr != null)
                {
                    return Content("This user already exist");
                }
                usr = new User
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = Crypto.HashPassword(user.Password),
                    IsAdmin = false,
                    IsBlocked = false,
                    RegistrateDate = DateTime.Now,
                    LastLoginDate = DateTime.Now
                };
                Session["Register"] = true;
                db.Users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            
        }
       
        public ActionResult LogOut()
        {
            Session["User"] = null;

            return RedirectToAction("index","home",new {Area="" });
        }
    }
    
}