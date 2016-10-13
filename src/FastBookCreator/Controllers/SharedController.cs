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
using Resources;

namespace FastBookCreator.Controllers
{
    [AllowAnonymous]
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
                case ResTypeID.Common:
                    insertQuery = @"UPDATE PACKS set LOGO=@LOGO WHERE _id=@id";
                    result = SqliteConn.GetCommonDb()
                        .Execute(insertQuery, new { id = resType._id, LOGO = resList[0].DATA });
                    break;

                case ResTypeID.Pack: 
                    insertQuery = @"INSERT INTO [RESOURCE](NAME,DATA) VALUES (@NAME,@DATA)";
                    result = SqliteConn.GetPackDb(GetUserId(), GetPackId()).Execute(insertQuery, resList);
                    break;

            }

            return
                Json(isSavedSuccessfully
                    ? new { Message = fName, _id = result }
                    : new { Message = Resource.ErrorSaveImage, _id = result });
        }


        public ActionResult UploadImages()
        {
            return View();
        }

        // GET: Pack
        public ActionResult ShowImages()
        {
            ViewBag.Title = Resource.ShowImage;
            return View();
        }

        public ActionResult SetCurrentPack(string packId)
        {
            // Save PackId in a cookie
            var cookie = Request.Cookies["_packId"];
            if (cookie != null)
                cookie.Value = packId; // update cookie value
            else // create new cookie
            {
                cookie = new HttpCookie("_packId")
                {
                    Value = packId,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);

            var callerActionRout = Request.GetReferrerUrlByCulture(packId);

            return Redirect(callerActionRout);
        }

        public ActionResult SetUserId(string userId)
        {
            // Save UserId in a cookie
            var cookie = Request.Cookies["_userId"];
            if (cookie != null)
                cookie.Value = userId; // update cookie value
            else // create new cookie
            {
                cookie = new HttpCookie("_userId")
                {
                    Value = userId,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);

            var callerActionRout = Request.GetReferrerUrlByCulture(userId);

            return Redirect(callerActionRout);
        }

        public string GetUserId()
        {
            // Save UserId in a cookie
            var cookie = Request.Cookies["_userId"];
            if (cookie != null) return cookie.Value;
            cookie = new HttpCookie("_userId")
            {
                Value = "0",
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);
            return cookie.Value;
        }

        public string GetPackId()
        {
            var cookiePack = Request.Cookies["_packId"];
            if (cookiePack == null)
                throw new Exception(Resource.SelectPack);
            return cookiePack.Value;
        }


        public ActionResult GetResPacks()
        {

            using (var connection = SqliteConn.GetPackDb(GetUserId(), GetPackId()))
            {
                return Json(connection.Query<ResPack>("SELECT * FROM RESOURCE"));
            }
        }
    }

}