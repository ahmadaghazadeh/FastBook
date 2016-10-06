using System;
using System.Globalization;
using System.Web;

using System.Web.Mvc;
using FastBookCreator.Core;
using System.Collections.Generic;
using FastBookCreator.Models;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using Dapper;
using System.Threading.Tasks;

namespace FastBookCreator.Controllers
{
    [System.Web.Mvc.AllowAnonymous]
    public class SharedController : BaseController
    {
        public ActionResult SetCulture(string culture)
        {
            new CultureInfo(culture).SetAppCulture();

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else // create new cookie
            {
                cookie = new HttpCookie("_culture")
                {
                    Value = culture,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);

            var callerActionRout = Request.GetReferrerUrlByCulture(culture);

            return Redirect(callerActionRout);
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            var resList = new List<ResPack>();
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    resList.Add(
                        new ResPack
                        {
                            NAME = fName,
                            DATA = data
                        }
                        );
                  }

            }
            using (var db = SqliteConn.GetPackDb())
            {
                string insertQuery = @"INSERT INTO [RESOURCE](NAME,DATA) VALUES (@NAME,@DATA)";
                var result = db.Execute(insertQuery, resList);
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = Resources.Resource.ErrorSaveImage });
            }
        }
        public ActionResult UploadImages()
        {
            return View();
        }

        // GET: Pack
        public ActionResult ShowImages()
        {
            ViewBag.Title = Resources.Resource.ShowImage;
            return View();
        }
 

      

    }

}