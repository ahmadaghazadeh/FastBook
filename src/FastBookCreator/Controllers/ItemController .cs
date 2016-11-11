using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
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
           
            Item tItem =null;
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                tItem = connection.Query<Item>($"SELECT * FROM ITEM_VIEW WHERE _id={item._id}").SingleOrDefault();
            }

            
            if (tItem != null)
            {
                item = tItem;
                item.CONTENT = item.CONTENT.ToResourceString(userId, packId);
            }
            
            switch (item.ITEM_TYPE_ID)
            {
                case 1:
                    ViewBag.Title = Resources.Resource.ItemHTML;
                    return View("InsertHTML", item);
                case 2:
                    ViewBag.Title = Resources.Resource.ItemPicture;
                    return View("InsertImage", item);
                case 3:
                    ViewBag.Title = Resources.Resource.ItemSound;
                    return View("InsertSound", item);
                case 4:
                    ViewBag.Title = Resources.Resource.ItemVideo;
                    return View("InsertVideo", item);
                case 5:
                    ViewBag.Title = Resources.Resource.ItemTTS;
                    return View("InsertTTS", item);
                case 6:
                    ViewBag.Title = Resources.Resource.ItemMultipleChoice;
                    return View("InsertMultipleChoice", item);

                case 10:
                    ViewBag.Title = Resources.Resource.ItemMultipleChoice;
                    return View("InsertOneChoice", item);

                default: return View("InsertImage", item);
            }
        }

     
        public ActionResult Delete(Item item)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                connection.Execute($"DELETE FROM ITEM WHERE _id={item._id}");
            }
           
            return Redirect(HttpContext.Request.UrlReferrer?.AbsoluteUri);
        }

        public ActionResult Speak(string text)
        {
            var speech = new SpeechSynthesizer();
            speech.Speak(text);

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                speech.SetOutputToWaveStream(stream);
                bytes = stream.ToArray();
            }
            return File(bytes, "audio/x-wav");
        }

 
    }
}
