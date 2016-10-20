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
            var pageTypeId = long.Parse(page.PAGE_TYPE_ID.ToString());
            ViewBag.Title = Resources.Resource.Page;
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            var userId = controller.GetUserId();
            var packId = controller.GetPackId();
            ViewBag.UserId = userId;
            ViewBag.PackId = packId;
            ViewBag.LessonId = pageId;
            IEnumerable<Item> items;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                items = connection.Query<Item>($"SELECT * FROM ITEM WHERE PAGE_ID={pageId}");
            }
            ViewBag.LESSON_ID = page.LESSON_ID;
            ViewBag.PAGE_ID = pageId;
            switch (pageTypeId)
            {
                case 1: return View("IndexDescription", (List<Item>)items);
                case 2: return View("IndexQuestion", (List<Item>)items);
                case 3: return View("IndexFlashCards", (List<Item>)items);
                default: return View("IndexDescription", (List<Item>)items);
            }

        }
        public ActionResult Insert(Item item)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.IsEdit = 0;
            var userId = controller.GetUserId();
            var packId = controller.GetPackId();
            ViewBag.UserId = userId;
            ViewBag.PackId = packId;
            ViewBag.PAGE_ID = item.PAGE_ID;
            var pageTypeId = long.Parse(item.ITEM_TYPE_ID.ToString());
            Item tItem =null;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                tItem = connection.Query<Item>($"SELECT * FROM ITEM_VIEW WHERE _id={item._id}").SingleOrDefault();
            }

            var regexrResource = new Regex(Shared.Utility.RegexrResource);
            if (tItem != null)
            {
                item = tItem;
                if (tItem.CONTENT!=null)
                {
                    var content = regexrResource.Matches(tItem.CONTENT);
                    var regexrNumber = new Regex(Shared.Utility.RegexrNumber);
                    foreach (Match resource in content)
                    {
                        var imgTag = regexrNumber.Matches(resource.ToString());
                        foreach (var num in imgTag)
                        {
                            item.CONTENT = item.CONTENT.Replace(resource.ToString(), num.ToString().ToImageFromDb(userId, packId));
                        }
                    }
                }
            }
            
            switch (pageTypeId)
            {
                case 1: return View("InsertHTML", item);
                case 2: return View("InsertImage", item);
                default: return View("InsertImage", item);
            }
        }

     
        public ActionResult Delete(Item item)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                connection.Execute($"DELETE FROM PAGE WHERE _id={item._id}");
            }
            return RedirectToAction("Index", "Item",item);
        }
    }
}
