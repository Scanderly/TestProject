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
        public ActionResult Login(string email, string password)
        {
           
            if (email == null || password == null)
            {
                 Session["LogError"] = "This field required";
            }
           using(TestProjectEntities db=new TestProjectEntities())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == email);
                if (user != null&& Crypto.VerifyHashedPassword(user.Password, password))
                {
                    if (user.IsBlocked == true)
                    {

                        return RedirectToAction("register", "login");
                    }
                    user.ErrorCount = 0;
                    if (user.IsAdmin == false)
                    {
                        return RedirectToAction("index", "profile", new { Area = "Client" });
                    }
                    return RedirectToAction("index", "dashboard", new { Area = "Admin" });
                  
                }
                
                Session["LoginError"] = "The email or password is not valid";
                Session.Timeout = 1;
                //int error = user.ErrorCount;
                user.ErrorCount++;
                db.SaveChanges();

                if (user.ErrorCount == 3)
                {
                    user.IsBlocked = true;
                    user.ErrorCount = 0;
                    db.SaveChanges();
                    return RedirectToAction("register", "login");
                }
            }
            
            return View();
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
                    LastLoggetDate = DateTime.Now
                };
                Session["Register"] = true;
                db.Users.Add(usr);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
    
}