using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestProject.Models;
using TestProject.Areas.Client.Data;
using RestSharp;

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
            List<Menu> menulist = new List<Menu>();
            using(TestProjectEntities db=new TestProjectEntities())
            {
                menulist = db.Menus.ToList();
            }
            return PartialView(menulist);
        }
        public ActionResult TestApi()
        {
            int a = 1;
            var client = new RestClient("http://samples.openweathermap.org/data/2.5/box/city?bbox=12,32,15,37,10&appid=439d4b804bc8187953eb36d2a8c26a02");
            var request = new RestRequest($"posts/token={a}", Method.GET);
            List<Wheather> cats = client.Execute<List<Wheather>>(request).Data;
            return View(cats);
        }
    }
}