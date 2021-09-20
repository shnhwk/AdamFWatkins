using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace AdamFWatkins.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
 
                // ModelState.AddModelError("UserName", "Please enter your name");

      
            return View();
        }
            

    }
}
