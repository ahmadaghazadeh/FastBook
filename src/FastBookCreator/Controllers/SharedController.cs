using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using FastBookCreator.Core;

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
    }
}