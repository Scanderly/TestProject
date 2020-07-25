﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Areas.Admin.Controllers
{
    public class EditController : Controller
    {
        // GET: Admin/Edit
        public ActionResult Index()
        {
            List<Menu> menus = new List<Menu>();
           using(TestProjectEntities db=new TestProjectEntities())
            {
                 menus= db.Menus.OrderByDescending(m => m.Id).ToList();
            }
            return View(menus);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            using(TestProjectEntities db =new TestProjectEntities())
            {
                Menu menu1 = db.Menus.FirstOrDefault(m =>m.Name == menu.Name && m.Link == menu.Link);
                if (menu1 != null)
                {
                    ModelState.AddModelError("AddError", "This menu already exist");
                    return View();
                }
                menu1 = new Menu()
                {
                    Name = menu.Name,
                    Link = menu.Link
                };
                db.Menus.Add(menu1);
                db.SaveChanges();
                
            }
            return RedirectToAction("index","dashboard");
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TestProjectEntities db = new TestProjectEntities())
            {
                Menu menu = db.Menus.Find(id);
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TestProjectEntities db = new TestProjectEntities())
            {
                Menu menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                db.SaveChanges();
            }
            return RedirectToAction("index","edit");
        }
    }
}