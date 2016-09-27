using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class PackController : BaseController
    {
        // GET: Pack
        public ActionResult Index()
        {
            var items = new List<Pack>();
            using (var connection = SqliteConn.GetConnection())
            {
                    connection.Open();
                    items = (List<Pack>) connection.Query<Pack>("SELECT * FROM Pack ");
                    connection.Dispose();
                    GC.Collect();
            }

            return View("Index", items);
        }

        public ActionResult Insert()
        {
           

            ViewBag.Title = "تولید سریع کتاب";
            ViewBag.Ahmad = "تولید سریع کتاب";
            ViewData["xx"] = "salam";

            return View();
        }



        //// GET: Pack/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Pack/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Pack/Create
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

        //// GET: Pack/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Pack/Edit/5
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

        //// GET: Pack/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Pack/Delete/5
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
