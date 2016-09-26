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
            ViewBag.Title = "";
            return View("Index");
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = "";
            return View("NotFound");
        }
    }
}