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
using System.Text;

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
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
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
                Selected = string.Equals(currentCulture, lang.Key, StringComparison.CurrentCultureIgnoreCase)
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

        public static IEnumerable<ResPack> GetResPacks()
        {
            using (var connection = SqliteConn.GetPackDb())
            {
                return connection.Query<ResPack>("SELECT * FROM RESOURCE");
            }
        }

        public static IEnumerable<Method> GetMethods()
        {
            using (var connection = SqliteConn.GetCommonDb())
            {
                return connection.Query<Method>($"SELECT * FROM METHODS WHERE LANGUAGE='{Resources.Resource.lang}'");
            }
        }


        public static IEnumerable<Subject> GetSubjects()
        {
            using (var connection = SqliteConn.GetCommonDb())
            {
                
                return connection.Query<Subject>($"SELECT * FROM SUBJECT WHERE LANGUAGE='{Resources.Resource.lang}'");
            }
        }


        public static MvcHtmlString MVCImage(this HtmlHelper helper, string id,
                byte[] image, string attributes = "")
        {

            return MvcHtmlString.Create(Image(id, image, attributes));
        }

        public static string Image(string id,
               byte[] image, string attributes = "")
        {
            var img = "";
            if (image!=null)
            {
                  img = $"data:image/jpg;base64,{ Convert.ToBase64String(image)}";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"<img id='img-{id}' src='{img}' { attributes}>");
            return sb.ToString();
        }

    }
}