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
    public class SocialMediaLinkController : Controller
    {
        private dbContext db = new dbContext();

        //
        // GET: /SocialMediaLink/

        public ActionResult Index()
        {
            return View(db.SocialMediaLinks.ToList());
        }

        //
        // GET: /SocialMediaLink/Details/5

        public ActionResult Details(int id = 0)
        {
            SocialMediaLink socialmedialink = db.SocialMediaLinks.Find(id);
            if (socialmedialink == null)
            {
                return HttpNotFound();
            }
            return View(socialmedialink);
        }

        //
        // GET: /SocialMediaLink/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SocialMediaLink/Create

        [HttpPost]
        public ActionResult Create(SocialMediaLink socialmedialink)
        {
            if (ModelState.IsValid)
            {
                db.SocialMediaLinks.Add(socialmedialink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialmedialink);
        }

        //
        // GET: /SocialMediaLink/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SocialMediaLink socialmedialink = db.SocialMediaLinks.Find(id);
            if (socialmedialink == null)
            {
                return HttpNotFound();
            }
            return View(socialmedialink);
        }

        //
        // POST: /SocialMediaLink/Edit/5

        [HttpPost]
        public ActionResult Edit(SocialMediaLink socialmedialink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialmedialink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialmedialink);
        }

        //
        // GET: /SocialMediaLink/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SocialMediaLink socialmedialink = db.SocialMediaLinks.Find(id);
            if (socialmedialink == null)
            {
                return HttpNotFound();
            }
            return View(socialmedialink);
        }

        //
        // POST: /SocialMediaLink/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMediaLink socialmedialink = db.SocialMediaLinks.Find(id);
            db.SocialMediaLinks.Remove(socialmedialink);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}