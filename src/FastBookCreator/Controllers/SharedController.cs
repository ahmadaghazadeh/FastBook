﻿using System;
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
                Response.Cookies.Add(cookie);
            }
            var callerActionRout = Request.GetReferrerUrlByCulture(culture);

            return Redirect(callerActionRout);
        }

        public void SetCurrentPack(string packId)
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
        }

        public void SetUserId(string userId)
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
        }

        public void SetIsAfterCreate(bool isChecked)
        {
            var flag = isChecked.ToString().ToLower();
            // Save UserId in a cookie
            var cookie = Request.Cookies["_isAfterCreate"];
            if (cookie != null)
                cookie.Value = flag; // update cookie value
            else // create new cookie
            {
                cookie = new HttpCookie("_isAfterCreate")
                {
                    Value = flag,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
        }

        public string GetIsAfterCreate()
        {
            // Save UserId in a cookie
            var cookie = Request.Cookies["_isAfterCreate"];
            if (cookie != null) return  cookie.Value.ToLower();
            cookie = new HttpCookie("_isAfterCreate")
            {
                Value = "false",
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);
            return cookie.Value.ToLower();
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