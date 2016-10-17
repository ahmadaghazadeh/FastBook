using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class ItemController : BaseController
    {
        public ActionResult Index(Page page)
        {
            var pageId = page._id.ToString();
            ViewBag.Title =Resources.Resource.Page;
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            ViewBag.LessonId = pageId;
            IEnumerable<Item> items;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                items = connection.Query<Item>($"SELECT * FROM ITEM WHERE PAGE_ID={pageId}");
            }
            return View("Index", (List<Item>)items);
        }
        public ActionResult InsertHTML()
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
