using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace AdamFWatkins.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Index()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult Oops(int id)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = id;
            return View();
        }
    }
}
 