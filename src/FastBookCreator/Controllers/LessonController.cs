using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class LessonController : BaseController
    {


        // GET: Pack
        public ActionResult Index(Pack pack)
        {
            var packId = pack._id.ToString();
            ViewBag.Title =Resources.Resource.Lesson;

            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            controller.SetCurrentPack(packId);

            IEnumerable<Lesson> lessons;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), packId))
            {
                lessons= connection.Query<Lesson>("SELECT * FROM LESSON_VIEW");
            }
            return View("Index", (List<Lesson>) lessons);
        }

        public ActionResult Insert()
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            ViewBag.IsEdit = 0;
            ViewBag.UserId = controller.GetUserId();
            ViewBag.PackId = controller.GetPackId();
            return View(new Lesson() {COLOR = 3433410 });
        }

        public ActionResult Edit(Pack pack)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            Lesson result;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                  result= connection.Query<Lesson>($"SELECT * FROM LESSON_VIEW WHERE _id={pack._id}").Single();
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
                 connection.Execute($"DELETE FROM LESSON WHERE _id={pack._id}");
            }
            return View("Index");
        }

  
    }
}
