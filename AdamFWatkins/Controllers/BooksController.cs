using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;

namespace AdamFWatkins.Controllers
{
    public class BooksController : Controller
    {
        //
        // GET: /Books/
        dbContext db = new dbContext();

        public ActionResult Index()
        {
            return RedirectToRoute("Books", new { pageNum = 1 });
        }

        public ActionResult Page(string pageNum)
        {
            var allBooks = db.Books.OrderBy(b => b.displayOrder);
            IQueryable<Book> filteredBooks;

            int intPageNum; 
            int.TryParse(pageNum, out intPageNum);

            if (intPageNum == 0)
            {
                return RedirectToRoute("Books", new { pageNum = 1 });
            }

            ViewBag.CurrentPage = intPageNum;
            int numPerPage = 6;


            if (allBooks.Count() > numPerPage)
            {

                int numPages = (int)Math.Ceiling((double)allBooks.Count() / numPerPage);

                if (intPageNum > numPages)
                {
                    //Redirect them to a valid page number. 
                    return RedirectToRoute("Books", new { pageNum = 1 });
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

                    filteredBooks = allBooks.Skip((intPageNum - 1) * numPerPage).Take(numPerPage);

                }

            }
            else
            {
                ViewBag.ShowMoreButtonVisible = "none";
                ViewBag.ShowBackButtonVisible = "none";
                filteredBooks = allBooks.Take(numPerPage);


            }

            return View(filteredBooks);
        }

        public ActionResult View(string id)
        {
            int bookId; 
            int.TryParse(id, out bookId);

            if (bookId == 0)
            {
                return RedirectToRoute("Books", new { pageNum = 1 });

            }

            var book = db.Books.Where(b => b.id == bookId).FirstOrDefault();

            if (book != null)
            {
                return View(book);
            }
            else
            {
                return RedirectToRoute("Books", new { pageNum = 1 });
            }

            
        }







        //New stuff

        //public ActionResult Admin()
        //{
        //    return View(db.Books.Where(b => b.isDeleted == false).OrderBy(b => b.displayOrder).ToList());
        //}
 
        //public ActionResult Details(int id = 0)
        //{
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}
 
        //public ActionResult Create()
        //{
        //    return View();
        //}
 
        //[HttpPost]
        //public ActionResult Create([Bind(Exclude = "id,displayOrder")]Book book)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        book.coverImage = "";
        //        book.displayOrder = db.Books.Take(1).OrderByDescending(s => s.displayOrder).SingleOrDefault().displayOrder + 1;
        //        book.isDeleted = false;
        //        book.purchaseUrl = "";
        //        book.purchaseUrl2 = "";
        //        book.purchaseUrl3 = "";
        //        book.text = "";
        //        book.title = "";

        //        db.Books.Add(book);
        //        db.SaveChanges();
        //        return RedirectToAction("Admin");

        //    }

        //    return View(book);
        //}

        ////
        //// GET: /Sketches/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Sketch sketch = db.Sketches.Find(id);
        //    if (sketch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sketch);
        //}

        ////
        //// POST: /Sketches/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Sketch sketch, HttpPostedFileBase imageData, HttpPostedFileBase imageThumb, HttpPostedFileBase imageFull)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sketch).State = EntityState.Modified;
        //        List<string> validExt = new List<string>();
        //        validExt.Add(".gif");
        //        validExt.Add(".jpg");
        //        validExt.Add(".jpeg");
        //        validExt.Add(".png");

        //        // Process image
        //        if ((imageThumb != null && imageThumb.ContentLength > 0 && imageFull != null && imageFull.ContentLength > 0))
        //        {
        //            var thumbName = Path.GetFileName(imageThumb.FileName);
        //            var thumbPath = Path.Combine(Server.MapPath("~/Images/Sketches/Thumbs"), thumbName);

        //            var fullName = Path.GetFileName(imageFull.FileName);
        //            var fullPath = Path.Combine(Server.MapPath("~/Images/Sketches/Full"), fullName);

        //            if (validExt.Contains(Path.GetExtension(thumbName.ToLower())) && validExt.Contains(Path.GetExtension(fullName.ToLower())))
        //            {
        //                imageThumb.SaveAs(thumbPath);
        //                sketch.thumbnail = thumbName;

        //                imageFull.SaveAs(fullPath);
        //                sketch.fullImage = fullName;

        //                db.SaveChanges();
        //                return RedirectToAction("Admin");
        //            }
        //        }

        //        db.SaveChanges();

        //        return RedirectToAction("Admin");

        //    }
        //    return View(sketch);
        //}

        ////
        //// GET: /Sketches/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    Sketch sketch = db.Sketches.Find(id);
        //    if (sketch == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sketch);
        //}

        ////
        //// POST: /Sketches/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Sketch sketch = db.Sketches.Find(id);
        //    sketch.isDeleted = true;
        //    db.Entry(sketch).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Admin");
        //}

        //public void UpdateOrder(int id, int fromPosition, int toPosition, string direction)
        //{
        //    if (direction == "back")
        //    {
        //        var movedCompanies = db.Sketches
        //                    .Where(c => (toPosition <= c.displayOrder && c.displayOrder <= fromPosition))
        //                    .ToList();

        //        foreach (var company in movedCompanies)
        //        {
        //            company.displayOrder++;
        //        }

        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        var movedCompanies = db.Sketches
        //                    .Where(c => (fromPosition <= c.displayOrder && c.displayOrder <= toPosition))
        //                    .ToList();
        //        foreach (var company in movedCompanies)
        //        {
        //            company.displayOrder--;
        //        }
        //    }

        //    db.Sketches.First(c => c.id == id).displayOrder = toPosition;
        //    db.SaveChanges();
        //}


        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}





    }
}
