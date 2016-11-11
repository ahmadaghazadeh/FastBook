using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dapper;
using FastBookCreator.Models;
using System.Linq.Expressions;
using System.Resources;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Resources;
using FastBookCreator.Controllers;

namespace FastBookCreator.Core
{
    public static class HelperExtensions
    {
        public static readonly Dictionary<string, string> Languages;


        static HelperExtensions()
        {
            Languages = new Dictionary<string, string>()
            {
                {"fa","فارسی" },
                {"en","English" }
            };
        }



        public static void SetAppCulture(this CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static string GetCurrentCultureTwoLetter()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        }

        public static List<SelectListItem> LanguageSelectListItems()
        {
            var currentCulture = GetCurrentCultureTwoLetter();

            var res = Languages.Select(lang => new SelectListItem()
            {
                Text = lang.Value,
                Value = lang.Key,
                Selected = String.Equals(currentCulture, lang.Key, StringComparison.CurrentCultureIgnoreCase)
            }).ToList();

            return res;
        }

        public static string GetReferrerUrlByCulture(this HttpRequestBase request, string culture)
        {
            // Split the url to url + query string
            var fullUrl = request.UrlReferrer?.ToString();
            var questionMarkIndex = fullUrl?.IndexOf('?');
            string queryString = null;
            string url = fullUrl ?? "";
            if (questionMarkIndex != -1 && questionMarkIndex != null) // There is a QueryString
            {
                url = fullUrl.Substring(0, (int)questionMarkIndex);
                queryString = fullUrl.Substring((int)questionMarkIndex + 1);
            }

            // Arranges
            var req = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(req, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            // Extract the data    
            if (routeData != null)
            {
                var values = routeData.Values;
                var controllerName = values["controller"].ToString().ToLower().Equals("home") ? "" : values["controller"] + "/";
                var actionName = values["action"].ToString().ToLower().Equals("index") ? "" : values["action"] + "/";
                var routValues = request.UrlReferrer?.Query;
                //culture += string.IsNullOrEmpty(culture) ? "" : "/";
                //var areaName = values["area"];

                var newRout = $"/{controllerName}{actionName}{routValues}";
                return newRout;
            }

            return "/";
        }


        public static IEnumerable<Pack> GetPacks()
        {
            using (var connection = SqliteConn.GetCommonDb())
            {
                return connection.Query<Pack>("SELECT * FROM PACKS");
            }
        }



        public static IEnumerable<Method> GetMethods()
        {
            using (var connection = SqliteConn.GetCommonDb())
            {
                return connection.Query<Method>($"SELECT * FROM METHODS WHERE LANGUAGE='{Resource.lang}'");
            }
        }


        public static IEnumerable<Subject> GetSubjects()
        {
            using (var connection = SqliteConn.GetCommonDb())
            {

                return connection.Query<Subject>($"SELECT * FROM SUBJECT WHERE LANGUAGE='{Resource.lang}'");
            }
        }


        public static MvcHtmlString MVCImage(this HtmlHelper helper, string id,
                byte[] image, string attributes = "")
        {

            return MvcHtmlString.Create(Image(id, image, attributes));
        }

        public static MvcHtmlString MVCSound(this HtmlHelper helper, string id,
                byte[] sound, string attributes = "")
        {

            return MvcHtmlString.Create(Sound(id, sound, attributes));
        }

        public static string ToImageFromDb(this string num, string userId, string packId)
        {

            using (var connection = SqliteConn.GetPackDb(userId, packId))
            {
                var singleOrDefault = connection.Query<ResPack>($"SELECT * from RESOURCE WHERE _id={num}").SingleOrDefault();
                if (singleOrDefault != null)
                    return $"data:image/jpg;base64,{  Convert.ToBase64String(singleOrDefault.DATA)}";
                return "";
            }
        }

        public static string Image(string id,
               byte[] image, string attributes = "")
        {
            var img = "";
            if (image != null)
            {
                img = $"data:image/jpg;base64,{ Convert.ToBase64String(image)}";
            }
            var sb = new StringBuilder();
            sb.AppendFormat($"<img id='{id}' src='{img}' { attributes}>");
            return sb.ToString();
        }

        public static string Sound(string id,
              byte[] sound, string attributes = "")
        {
            var son = "";
            if (sound != null)
            {
                son = $"data:audio/mp3;base64,{ Convert.ToBase64String(sound)}";
            }
            var sb = new StringBuilder();
            sb.AppendFormat($"<audio controls id='{id}' src='{son}' { attributes}>");
            return sb.ToString();
        }


        public static IEnumerable<IconHtml> ItemIcons()
        {
            IEnumerable<IconHtml> icons = new[]
            {
                new IconHtml()
                {
                    ItemTypeId=0,
                    Name ="",
                    Icon = ""
                },
                new IconHtml()
                {
                    ItemTypeId=1,
                    Name = Resource.ItemHTML,
                    Icon = "<i class=\"fa fa-html5 FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=2,
                    Name = Resource.ItemPicture,
                    Icon = "<i class=\"fa fa-picture-o FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=3,
                    Name = Resource.ItemSound,
                    Icon = "<i class=\"fa fa-file-audio-o FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=4,
                    Name = Resource.ItemVideo,
                    Icon = "<i class=\"fa fa-file-video-o FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=5,
                    Name = Resource.ItemTTS,
                    Icon = "<i class=\"fa fa-volume-up FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=6,
                    Name = Resource.ItemMultipleChoice,
                    Icon = "<i class=\"fa fa-check-square FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=7,
                    Name = Resource.ItemPlacement,
                    Icon = "<i class=\"fa fa-arrows FontAwesome\" aria-hidden=\"true\"></i>"
                },

                new IconHtml()
                {
                    ItemTypeId=8,
                    Name = Resource.ItemOrdering,
                    Icon = "<i class=\"fa fa-sort FontAwesome \" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=9,
                    Name = Resource.ItemFill,
                    Icon = "<i class=\"fa fa-pencil-square-o FontAwesome\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                    ItemTypeId=10,
                    Name = Resource.ItemOnechoice,
                    Icon = "<i class=\"fa fa-dot-circle-o FontAwesome\" aria-hidden=\"true\"></i>"
                },
            };
            return icons;
        }

        public static long InsertResourceUrl(string userId, string packId, string url)
        {
            const string insertQuery = @" INSERT INTO RESOURCE(RESOURCE_TYPE_ID,URL) SELECT 4, @URL
                                 WHERE NOT EXISTS(SELECT 1 FROM RESOURCE WHERE URL = @URL );          
                                    SELECT _id FROM RESOURCE WHERE URL = @URL;";
            return SqliteConn.GetPackDb(userId, packId).Query<int>(insertQuery, new { URL = url }).SingleOrDefault();
        }

        public static void InserItemDetail(string userId, string packId, ItemDetail itemDetail)
        {
            const string insertQuery = @" INSERT INTO ITEM_DETAIL(ITEM_ID,ANSWER,IS_ANSWER,ORDER_NUMBER) VALUES(@ITEM_ID,@ANSWER,@IS_ANSWER,@ORDER_NUMBER)";
            SqliteConn.GetPackDb(userId, packId).Query(insertQuery, new { itemDetail.ITEM_ID, itemDetail.ANSWER, itemDetail.IS_ANSWER, itemDetail.ORDER_NUMBER });
        }

 

        public static string ToResourceString(this string content,string userId,string packId)
        {
            var regexrResource = new Regex(Shared.Utility.RegexrResource);
            if (content == null) return null;
            var con = regexrResource.Matches(content);
            var regexrNumber = new Regex(Shared.Utility.RegexrNumber);
            foreach (Match resource in con)
            {
                var imgTag = regexrNumber.Matches(resource.ToString());
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var num in imgTag)
                {
                    content = content.Replace(resource.ToString(), num.ToString().ToImageFromDb(userId, packId));
                }
            }
            return content;
        }

    }
}