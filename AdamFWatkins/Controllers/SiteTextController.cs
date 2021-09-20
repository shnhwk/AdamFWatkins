using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;

namespace AdamFWatkins.Controllers
{
    [Authorize]
    public class SiteTextController : Controller
    {
        private dbContext db = new dbContext();

        //
        // GET: /SiteText/

        public ActionResult Index()
        {
            return View(db.SiteTexts.ToList());
        }
 

        //
        // GET: /SiteText/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SiteText sitetext = db.SiteTexts.Find(id);
            if (sitetext == null)
            {
                return HttpNotFound();
            }
            return View(sitetext);
        }

        //
        // POST: /SiteText/Edit/5

        [HttpPost]
        public ActionResult Edit(SiteText sitetext)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sitetext).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sitetext);
        }
 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}