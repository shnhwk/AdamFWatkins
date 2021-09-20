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
    public class IllustrationsController : Controller
    {
        //
        // GET: /Illustration/

        dbContext db = new dbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToRoute("Illustrations", new { pageNum = 1 });
        }
        [AllowAnonymous]
        public ActionResult Page(string pageNum)
        {
            var allIllustrations = db.Illustrations.Where(i => i.isDeleted == false).OrderBy(i => i.displayOrder);
            IQueryable<Illustration> filteredIllustrations;

            int intPageNum;
            int.TryParse(pageNum, out intPageNum);

            if (intPageNum == 0)
            {
                return RedirectToRoute("Illustrations", new { pageNum = 1 });
            }

            ViewBag.CurrentPage = intPageNum;
            int numPerPage = 18;


            if (allIllustrations.Count() > numPerPage)
            {

                int numPages = (int)Math.Ceiling((double)allIllustrations.Count() / numPerPage);

                if (intPageNum > numPages)
                {
                    //Redirect them to a valid page number. 
                    return RedirectToRoute("Illustrations", new { pageNum = 1 });
                }
                else
                {

                    if (intPageNum == 1)
                    {
                        ViewBag.ShowBackButtonVisible = "none";
                    }
                    else
                    {
                        ViewBag.ShowBackButtonVisible = "";
                        ViewBag.PreviousPage = intPageNum - 1;
                    }


                    if (intPageNum == numPages)
                    {
                        ViewBag.ShowMoreButtonVisible = "none";
                    }
                    else
                    {
                        ViewBag.ShowMoreButtonVisible = "";
                        ViewBag.NextPage = intPageNum + 1;
                    }

                    filteredIllustrations = allIllustrations.Skip((intPageNum - 1) * numPerPage).Take(numPerPage);

                }

            }
            else
            {
                ViewBag.ShowMoreButtonVisible = false;

                filteredIllustrations = allIllustrations.Take(numPerPage);


            }

            return View(filteredIllustrations);
        }



        public ActionResult Admin()
        {
            return View(db.Illustrations.Where(s => s.isDeleted == false).OrderBy(s => s.displayOrder).ToList());
        }

        //
        // GET: /Illustrations/Details/5

        public ActionResult Details(int id = 0)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return HttpNotFound();
            }
            return View(illustration);
        }

        //
        // GET: /Illustrations/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Illustrations/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "id,displayOrder")]Illustration illustration, HttpPostedFileBase imageThumb, HttpPostedFileBase imageFull)
        {
            if (ModelState.IsValid)
            {
                List<string> validExt = new List<string>();
                validExt.Add(".gif");
                validExt.Add(".jpg");
                validExt.Add(".jpeg");
                validExt.Add(".png");

                // Process image
                if ((imageThumb != null && imageThumb.ContentLength > 0 && imageFull != null && imageFull.ContentLength > 0))
                {
                    var thumbName = Path.GetFileName(imageThumb.FileName);
                    var thumbPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Thumbs"), thumbName);

                    var fullName = Path.GetFileName(imageFull.FileName);
                    var fullPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Full"), fullName);

                    if (validExt.Contains(Path.GetExtension(thumbName.ToLower())) && validExt.Contains(Path.GetExtension(fullName.ToLower())))
                    {
                        imageThumb.SaveAs(thumbPath);
                        illustration.thumbnail = thumbName;

                        imageFull.SaveAs(fullPath);
                        illustration.fullImage = fullName;

                        illustration.displayOrder = db.Illustrations.Take(1).OrderByDescending(s => s.displayOrder).SingleOrDefault().displayOrder + 1;

                        db.Illustrations.Add(illustration);
                        db.SaveChanges();
                        return RedirectToAction("Admin");
                    }
                }
            }

            return View(illustration);
        }

        //
        // GET: /Illustrations/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return HttpNotFound();
            }
            return View(illustration);
        }

        //
        // POST: /Illustrations/Edit/5

        [HttpPost]
        public ActionResult Edit(Illustration illustration, HttpPostedFileBase imageData, HttpPostedFileBase imageThumb, HttpPostedFileBase imageFull)
        {
            if (ModelState.IsValid)
            {
                db.Entry(illustration).State = EntityState.Modified;
                List<string> validExt = new List<string>();
                validExt.Add(".gif");
                validExt.Add(".jpg");
                validExt.Add(".jpeg");
                validExt.Add(".png");

                // Process image
                if ((imageThumb != null && imageThumb.ContentLength > 0 && imageFull != null && imageFull.ContentLength > 0))
                {
                    var thumbName = Path.GetFileName(imageThumb.FileName);
                    var thumbPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Thumbs"), thumbName);

                    var fullName = Path.GetFileName(imageFull.FileName);
                    var fullPath = Path.Combine(Server.MapPath("~/Images/Illustrations/Full"), fullName);

                    if (validExt.Contains(Path.GetExtension(thumbName.ToLower())) && validExt.Contains(Path.GetExtension(fullName.ToLower())))
                    {
                        imageThumb.SaveAs(thumbPath);
                        illustration.thumbnail = thumbName;

                        imageFull.SaveAs(fullPath);
                        illustration.fullImage = fullName;

                        db.SaveChanges();
                        return RedirectToAction("Admin");
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Admin");

            }
            return View(illustration);
        }

        //
        // GET: /Illustrations/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Illustration illustration = db.Illustrations.Find(id);
            if (illustration == null)
            {
                return HttpNotFound();
            }
            return View(illustration);
        }

        //
        // POST: /Illustrations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Illustration illustration = db.Illustrations.Find(id);
            illustration.isDeleted = true;
            db.Entry(illustration).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        public void UpdateOrder(int id, int fromPosition, int toPosition, string direction)
        {
            if (direction == "back")
            {
                var movedCompanies = db.Illustrations
                            .Where(c => (toPosition <= c.displayOrder && c.displayOrder <= fromPosition))
                            .ToList();

                foreach (var company in movedCompanies)
                {
                    company.displayOrder++;
                }

                db.SaveChanges();
            }
            else
            {
                var movedCompanies = db.Illustrations
                            .Where(c => (fromPosition <= c.displayOrder && c.displayOrder <= toPosition))
                            .ToList();
                foreach (var company in movedCompanies)
                {
                    company.displayOrder--;
                }
            }

            db.Illustrations.First(c => c.id == id).displayOrder = toPosition;
            db.SaveChanges();
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}