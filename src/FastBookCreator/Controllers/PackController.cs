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

  
    }
}
