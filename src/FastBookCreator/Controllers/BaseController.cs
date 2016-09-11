using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using FastBookCreator.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FastBookCreator.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set { _userManager = value; }
        }


        public ApplicationUser CurrentUser => UserManager.FindById(User.Identity.GetUserId());

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = Request.Cookies["_culture"]?.Value // Attempt to read the culture cookie from Request
                ?? (Request.UserLanguages?.Any() == true
                    ? Request.UserLanguages[0] // obtain it from HTTP header AcceptLanguages
                    : "en");

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(cultureName);

            return base.BeginExecuteCore(callback, state);
        }
    }
}