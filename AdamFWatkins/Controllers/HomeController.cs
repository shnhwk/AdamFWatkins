using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;

namespace AdamFWatkins.Controllers
{
    public class HomeController : Controller
    {
        dbContext db = new dbContext();

        public ActionResult Index()
        {
            //var st = db.SocialMediaLinks;
            //return View(st);

            return View();
 
        }

 
    }
}
