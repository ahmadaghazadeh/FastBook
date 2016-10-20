using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class PageController : BaseController
    {


        // GET: Pack
        public ActionResult Index(Lesson lesson)
        {
            var lessonId = lesson._id.ToString();
            ViewBag.Title =Resources.Resource.Page;

            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.userId = controller.GetUserId();
            ViewBag.packId = controller.GetPackId();
            ViewBag.LESSON_ID = lessonId;
            IEnumerable<Page> page;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                page = connection.Query<Page>($"SELECT * FROM PAGE WHERE LESSON_ID={lessonId}");
            }
            return View("Index", (List<Page>)page);
        }

        public ActionResult Insert()
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.IsEdit = 0;
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
          
            return View(new Page());
        }

        public ActionResult Edit(Pack pack)
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
