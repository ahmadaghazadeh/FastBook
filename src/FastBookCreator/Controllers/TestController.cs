using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using FastBookCreator.Core;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            var items = new List<Foo>
            {
                new Foo { Name = "ahmad"},
                new Foo { Name = "ali"},
                new Foo { Name = "mahamad"},
                new Foo { Name = "ali"},
                new Foo { Name = "reza"},
                new Foo { Name = "beh"},
                new Foo { Name = "ad"},
                new Foo { Name = "hjgj"},
                new Foo { Name = "xcfs"},

            };


            ViewBag.Title = "تولید سریع کتاب";
            ViewBag.Ahmad = "تولید سریع کتاب";
            ViewData["xx"] = "salam";

            return View("Index", items);
        }

        // GET: Test/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Test/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Test/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Test/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Test/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Test/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Test/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
