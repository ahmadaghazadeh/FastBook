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
using Newtonsoft.Json.Linq;

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
                cookie.Value = culture; // update cookie value
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

        public ActionResult SaveUploadedFile(ResType resType)
        {
            var isSavedSuccessfully = false;
            var fName = "";
            var resList = new List<ResPack>();
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                if (file != null)
                {
                    fName = file.FileName;
                    if (file.ContentLength > 0)
                    {
                        var target = new MemoryStream();
                        file.InputStream.CopyTo(target);
                        var data = target.ToArray();
                        resList.Add(
                            new ResPack
                            {
                                NAME = fName,
                                DATA = data
                            }
                            );
                    }
                }
                isSavedSuccessfully = true;

            }

            var insertQuery = "";
            var result = 0;
            switch (resType.typeId)
            {
                case ResTypeID.Pack:
                    insertQuery = @"UPDATE PACKS set LOGO=@LOGO WHERE _id=@id";
                    result = SqliteConn.GetCommonDb()
                        .Execute(insertQuery, new { id = resType._id, LOGO = resList[0].DATA });
                    break;

                case ResTypeID.Item:
                case ResTypeID.ItemDetail:
                case ResTypeID.Lesson:
                case ResTypeID.Method:
                case ResTypeID.Page:
                    insertQuery = @"INSERT INTO [RESOURCE](NAME,DATA) VALUES (@NAME,@DATA) 
                                   SELECT CAST(SCOPE_IDENTITY() as long)";
                    result = SqliteConn.GetPackDb().Query<int>(insertQuery, resList).Single();
                    break;

            }

            return
                Json(isSavedSuccessfully
                    ? new { Message = fName, _id = result }
                    : new { Message = Resources.Resource.ErrorSaveImage, _id = result });
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