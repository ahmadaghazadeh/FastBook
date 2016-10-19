using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;
using System.Text.RegularExpressions;

namespace FastBookCreator.Controllers
{
    public class ItemController : BaseController
    {
        public ActionResult Index(Page page)
        {
            var pageId = page._id.ToString();
            var pageTypeId =long.Parse( page.PAGE_TYPE_ID.ToString());
            ViewBag.Title =Resources.Resource.Page;
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            string userId= controller.GetUserId();
            string packId = controller.GetPackId();
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            ViewBag.LessonId = pageId;
            IEnumerable<Item> items;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                items = connection.Query<Item>($"SELECT * FROM ITEM WHERE PAGE_ID={pageId}");
            }
            foreach (var item in items)
            {
                MatchCollection regexrResource = Regex.Matches(Shared.Utility.RegexrResource, item.CONTENT);
                foreach (Match resource in regexrResource)
                {
                    MatchCollection number = Regex.Matches(Shared.Utility.RegexrNumber, resource.ToString());
                    foreach (var num in number)
                    {
                        item.CONTENT = item.CONTENT.Replace(resource.ToString(), num.ToString().ToImageFromDb(userId, packId));
                    }
                }
            }
            switch (pageTypeId)
            {
                case 1: return View("IndexDescription", (List<Item>)items);  
                case 2: return View("IndexQuestion", (List<Item>)items); 
                case 3: return View("IndexFlashCards", (List<Item>)items);
                default: return View("IndexDescription", (List<Item>)items);
            }

        }
        public ActionResult InsertHTML(Item item)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.IsEdit = 0;
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            return View(new Item());
        }

        public ActionResult InsertImage()
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.IsEdit = 0;
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            return View(new Item());
        }

        public ActionResult EditText(Pack pack)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            Page result;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                  result= connection.Query<Page>($"SELECT * FROM PAGE WHERE _id={pack._id}").Single();
            }
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            ViewBag.IsEdit = 1;
            return View("Insert", result);
        }
        public ActionResult Delete(Pack pack)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                 connection.Execute($"DELETE FROM PAGE WHERE _id={pack._id}");
            }
            return View("Index");
        }

  
    }
}
