using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdamFWatkins
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //    "Books", // Route name
            //    "Books/Page/{pageNum}", // URL with parameters
            //    new { controller = "Books", action = "Page", pageNum = "" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "BooksView", // Route name
            //    "Books/View/{id}/{booktitle}", // URL with parameters
            //    new { controller = "Books", action = "View", id = "", booktitle = "" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Illustrations", // Route name
            //    "Illustrations/Page/{pageNum}", // URL with parameters
            //    new { controller = "Illustrations", action = "Page", pageNum = "" } // Parameter defaults
            //);
            //routes.MapRoute(
            //    "IllustrationsNoId", // Route name
            //    "Illustrations/Page/", // URL with parameters
            //    new { controller = "Illustrations", action = "Page", pageNum = "1" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Sketches", // Route name
            //    "Sketches/Page/{pageNum}", // URL with parameters
            //    new { controller = "Sketches", action = "Page", pageNum = "" } // Parameter defaults
            //);
            //routes.MapRoute(
            //    "SketchesNoId", // Route name
            //    "Sketches/Page/", // URL with parameters
            //    new { controller = "Sketches", action = "Page", pageNum = "1" } // Parameter defaults
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/",
                defaults: new { controller = "Home", action = "Index" }
            );


            //routes.MapRoute(
            //   "404-FancyPage",
            //   "{*url}",
            //   new { controller = "StaticContent", action = "PageNotFound" }
            //);
        }
    }
}