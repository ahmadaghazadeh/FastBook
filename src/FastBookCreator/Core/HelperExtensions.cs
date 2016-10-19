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
using System.Text;
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

        public static string ToImageFromDb(this string num,string userId,string packId)
        {
          
            using (var connection = SqliteConn.GetPackDb(userId, packId))
            {
                return Convert.ToBase64String(connection.Query($"SELECT DATA from RESOURCE WHERE _id={num}").Single());
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"<img id='{id}' src='{img}' { attributes}>");
            return sb.ToString();
        }


        public static IEnumerable<IconHtml> ItemIcons()
        {
            IEnumerable<IconHtml> icons=new[]
            {
                new IconHtml()
                {
                    PageName = "InsertHTML",
                    Name = Resource.ItemHTML,
                    Icon = "<i class=\"fa fa-html5\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                  PageName = "InserPicture",
                    Name = Resource.ItemPicture,
                    Icon = "<i class=\"fa fa-picture-o\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                   PageName = "InserSound",
                    Name = Resource.ItemSound,
                    Icon = "<i class=\"fa fa-file-audio-o\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                   PageName = "InserVideo",
                    Name = Resource.ItemVideo,
                    Icon = "<i class=\"fa fa-file-video-o\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                  PageName = "InserTTS",
                    Name = Resource.ItemTTS,
                    Icon = "<span class=\"glyphicon glyphicon-bullhorn\" aria-hidden=\"true\"></span>"
                },
                new IconHtml()
                {
                   PageName = "InserMultipleChoice",
                    Name = Resource.ItemMultipleChoice,
                    Icon = "<i class=\"fa fa-check-square\" aria-hidden=\"true\"></i>"
                },
                new IconHtml()
                {
                   PageName = "InserPlacement",
                    Name = Resource.ItemPlacement,
                    Icon = "<i class=\"material-icons\">space_bar</i>"
                },

                new IconHtml()
                {
                 PageName = "InserOrdering",
                    Name = Resource.ItemOrdering,
                    Icon = "<span class=\"glyphicon glyphicon-sort\" aria-hidden=\"true\"></span>"
                },
                new IconHtml()
                {
                 PageName = "InserFill",
                    Name = Resource.ItemFill,
                    Icon = "<i class=\"material-icons\">mode_edit</i>"
                },
                new IconHtml()
                {
                 PageName = "InserOnechoice",
                    Name = Resource.ItemOnechoice,
                    Icon = "<i class=\"fa fa-dot-circle-o\" aria-hidden=\"true\"></i>"
                },
            };
            return icons;
        }
    }
}