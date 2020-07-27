using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Areas.Admin.Controllers
{
    [AdminLevel]
    public class SlideController : Controller
    {
        //GET: Admin/Slide
        public ActionResult Index()
        {
            List<AdPanel> adPanels = new List<AdPanel>();
            using (TestProjectEntities db = new TestProjectEntities())
            {
                adPanels = db.AdPanels.OrderBy(a => a.Id).ToList();
            }

            return View(adPanels);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdPanel adpanel)
        {

            using (TestProjectEntities db = new TestProjectEntities())
            {
                AdPanel adpanel1 = db.AdPanels.FirstOrDefault(a => a.Id == adpanel.Id && a.Name == adpanel.Name && a.Text == adpanel.Text);
                if (adpanel1 != null)
                {
                    return Content("This item already exist");
                }
                adpanel1 = new AdPanel()
                {
                    Id = adpanel.Id,
                    Name = adpanel.Name,
                    Text = adpanel.Text
                };
                db.AdPanels.Add(adpanel1);
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(AdPanel adpanel)
        {
            if (adpanel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TestProjectEntities db = new TestProjectEntities())
            {
                AdPanel edited = db.AdPanels.Find(adpanel.Id);
                edited.Id = adpanel.Id;
                edited.Name = adpanel.Name;
                edited.Text = adpanel.Text;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (TestProjectEntities db = new TestProjectEntities())
            {
                AdPanel AdPanel = db.AdPanels.Find(id);
                db.AdPanels.Remove(AdPanel);
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