using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;
using Newtonsoft.Json.Linq;

namespace FastBookCreator.Controllers
{
    public class PackController : BaseController
    {


        // GET: Pack
        public ActionResult Index()
        {
            ViewBag.Title =Resources.Resource.PackList;
            return View("Index");
        }

        public ActionResult Insert()
        {
            ViewBag.IsEdit = 0;
            return View(new Pack() {COLOR = 3433410 });
        }

        public ActionResult Edit(Pack pack)
        {
            Pack result;
            using (var connection = SqliteConn.GetCommonDb())
            {
                  result= connection.Query<Pack>($"SELECT * FROM PACKS WHERE _id={pack._id}").Single();
            }
            ViewBag.IsEdit = 1;
            return View("Insert", result);
        }

        public ActionResult Delete(Pack pack)
        {
            using (var connection = SqliteConn.GetCommonDb())
            {
                 connection.Execute($"DELETE FROM PACKS WHERE _id={pack._id}");
            }
            return View("Index");
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
