using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TestProject.Models;
using RestSharp;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using TestProject.Areas.Client.Wheatheritems;


namespace TestProject.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Client/Home
        public ActionResult Index()
        {
            using (TestProjectEntities db = new TestProjectEntities())
            {

                AdPanel ad = new AdPanel();
                //AdText adText=db.AdPanels.Single(a=>a.Text==ad.Text)
               
            }

            //Weatherinfo weather = new Weatherinfo();
            string link = string.Format("https://samples.openweathermap.org/data/2.5/weather");
            var client = new RestClient(link);
            var request = new RestRequest("?q=London&appid=439d4b804bc8187953eb36d2a8c26a02", Method.GET);
            List<Weatherinfo> wheathers = client.Execute<List<Weatherinfo>>(request).Data;
            return View(wheathers);
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
       
    }
}