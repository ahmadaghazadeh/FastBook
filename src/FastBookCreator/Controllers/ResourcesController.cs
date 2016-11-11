using System;
using System.Web.Mvc;
using FastBookCreator.Core;
using System.Collections.Generic;
using FastBookCreator.Models;
using System.IO;
using Dapper;
using Resources;

namespace FastBookCreator.Controllers
{
    [AllowAnonymous]
    public class ResourcesController : BaseController
    {
        public ActionResult SaveUploadedFile(ResType resType)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            var userId = controller.GetUserId();
            var packId = controller.GetPackId();
            var isSavedSuccessfully = false;
            var fName = "";
            var resList = new List<ResPack>();
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                if (file != null)
                {
                    fName = file.FileName;
                    if (file.ContentLength > 0)
                    {
                        var target = new MemoryStream();
                        file.InputStream.CopyTo(target);
                        var data = target.ToArray();
                        resList.Add(
                            new ResPack
                            {
                                NAME = fName,
                                DATA = data,
                                RESOURCE_TYPE_ID= (long) resType.typeId
                            }
                            );
                    }
                }
                isSavedSuccessfully = true;

            }

            var insertQuery = "";
            var result = 0;
            try
            {
                switch (resType.typeId)
                {
                    case ResTypeID.Common:
                        insertQuery = @"UPDATE PACKS set LOGO=@LOGO WHERE _id=@id";
                        result = SqliteConn.GetCommonDb()
                            .Execute(insertQuery, new { id = resType._id, LOGO = resList[0].DATA });
                        break;

                    case ResTypeID.PACK_IMAGE:
                    case ResTypeID.PACK_SOUND:
                        insertQuery = @"INSERT INTO [RESOURCE](NAME,DATA,RESOURCE_TYPE_ID) VALUES (@NAME,@DATA,@RESOURCE_TYPE_ID)";
                        result = SqliteConn.GetPackDb(userId, packId).Execute(insertQuery, resList);
                        break;
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
           
            return
                Json(isSavedSuccessfully
                    ? new { Message = fName, _id = result }
                    : new { Message = Resource.ErrorSaveImage, _id = result });
        }

        public ActionResult UploadImages()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View("Index");
        }


        public ActionResult ShowImages()
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            var userId = controller.GetUserId();
            var packId = controller.GetPackId();
            ViewBag.Title = Resource.ShowFiles;
            ViewBag.userId = userId;
            ViewBag.packId = packId;
            return View();
        }

        public ActionResult UploadSound()
        {
            return View();
        }

        public ActionResult ShowSound()
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            var userId = controller.GetUserId();
            var packId = controller.GetPackId();
            ViewBag.Title = Resource.ShowFiles;
            ViewBag.userId = userId;
            ViewBag.packId = packId;
            return View();
        }


        public ActionResult Delete(ResType resType)
        {
            var controller = DependencyResolver.Current.GetService<SharedController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            using (var connection = SqliteConn.GetPackDb(controller.GetUserId(), controller.GetPackId()))
            {
                connection.Execute($"DELETE FROM RESOURCE WHERE _id={resType._id}");
            }

            return Redirect(Request.UrlReferrer?.ToString());
        }


    }

}