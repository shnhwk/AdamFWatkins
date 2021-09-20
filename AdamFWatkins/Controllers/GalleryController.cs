using AdamFWatkins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;

namespace AdamFWatkins.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {

 
        dbContext db = new dbContext();

        public ActionResult Admin()
        {
            return View(db.Galleries.Where(s => s.isDeleted == false).OrderBy(s => s.displayOrder).ToList());
        }
 
        public ActionResult Details(int id = 0)
        {
            Gallery g = db.Galleries.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

 
        public ActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "id,displayOrder")]Gallery gallery, HttpPostedFileBase imageFull)
        {
            if (ModelState.IsValid) 
            {
                List<string> validExt = new List<string>();
                validExt.Add(".gif");
                validExt.Add(".jpg");
                validExt.Add(".jpeg");
                validExt.Add(".png");

                // Process image
                if ((imageFull != null && imageFull.ContentLength > 0))
                {
               
                    var fullName = Path.GetFileName(imageFull.FileName);
                    var fullPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Full"), fullName);

                    if (validExt.Contains(Path.GetExtension(fullName.ToLower())))
                    {
               
                        imageFull.SaveAs(fullPath);
                        gallery.fullImage = fullName;

                        gallery.displayOrder = db.Galleries.Take(1).OrderByDescending(s => s.displayOrder).SingleOrDefault().displayOrder + 1;

                        db.Galleries.Add(gallery);
                        db.SaveChanges();
                        return RedirectToAction("Admin");
                    }
                }
            }

            return View(gallery);
        }

        //
        // GET: /Illustrations/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        //
        // POST: /Illustrations/Edit/5     HttpPostedFileBase imageData,

        [HttpPost]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase imageFull)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                List<string> validExt = new List<string>();
                validExt.Add(".gif");
                validExt.Add(".jpg");
                validExt.Add(".jpeg");
                validExt.Add(".png");

                // Process image
                if ((imageFull != null && imageFull.ContentLength > 0))
                { 
                    var fullName = Path.GetFileName(imageFull.FileName);
                    var fullPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Full"), fullName);

                    if (validExt.Contains(Path.GetExtension(fullName.ToLower())))
                    { 
                        imageFull.SaveAs(fullPath);
                        gallery.fullImage = fullName;

                        db.SaveChanges();
                        return RedirectToAction("Admin");
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Admin");

            }
            return View(gallery);
        }
 
        public ActionResult Delete(int id = 0)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

 

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            gallery.isDeleted = true;
            db.Entry(gallery).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        public void UpdateOrder(int id, int fromPosition, int toPosition, string direction)
        {
            if (direction == "back")
            {
                var movedGalleries = db.Galleries
                            .Where(c => (toPosition <= c.displayOrder && c.displayOrder <= fromPosition))
                            .ToList();

                foreach (var g in movedGalleries)
                {
                    g.displayOrder++;
                }

                db.SaveChanges();
            }
            else
            {
                var movedGalleries = db.Galleries
                            .Where(c => (fromPosition <= c.displayOrder && c.displayOrder <= toPosition))
                            .ToList();
                foreach (var g in movedGalleries)
                {
                    g.displayOrder--;
                }
            }

            db.Galleries.First(c => c.id == id).displayOrder = toPosition;
            db.SaveChanges();
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}