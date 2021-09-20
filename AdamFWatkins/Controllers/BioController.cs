using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;

namespace AdamFWatkins.Controllers
{
    public class BioController : Controller
    {
        //
        // GET: /About/

        dbContext db = new dbContext();

        public ActionResult Index()
        {
            SiteText st = db.SiteTexts.Where(s => s.key == "BioContent").FirstOrDefault();
            return View(st);
        }
 

    }
}
