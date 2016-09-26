using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastBookCreator.Controllers
{
    [AllowAnonymous]
    public class ErrorsController : BaseController
    {
        // GET: Errors
        public ActionResult Index()
        {
            ViewBag.Title = $"{Localization.Xomorod} - {Localization.Error}";
            return View("Index");
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = $"{Localization.Xomorod} - {Localization.PageNotFound}";
            return View("NotFound");
        }
    }
}