 
using System.Collections.Generic;
using System.Data;
 
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Core;
using FastBookCreator.Models;
using SqliteConnection = System.Data.SQLite.SQLiteConnection;
 

namespace FastBookCreator.Controllers
{
    public class TestController : BaseController
    {
        /*   // GET: Test
           public ActionResult Index()
           {
               var items = new List<Foo>
               {
                   new Foo { Name = "ahmad"},
                   new Foo { Name = "ali"},
                   new Foo { Name = "mahamad"},
                   new Foo { Name = "ali"},
                   new Foo { Name = "reza"},
                   new Foo { Name = "beh"},
                   new Foo { Name = "ad"},
                   new Foo { Name = "hjgj"},
                   new Foo { Name = "xcfs"},

               };


               ViewBag.Title = "تولید سریع کتاب";
               ViewBag.Ahmad = "تولید سریع کتاب";
               ViewData["xx"] = "salam";

               return View("Index", items);
           }*/


        // public static string ConnectionString => $"Filename={FileName};";
        //  public IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
        class Pack
        {
            public int PackId { get; set; }
            public string PackName { get; set; }
            public string Description { get; set; }
        }

        public ActionResult Index()
        {
            using (var connection = SqliteConn.GetConnection())
            {
                connection.Open();
                
                connection.Execute(@"insert Pack (PackName,Description) values(@PackName, @Description)", new List<Pack>
                {
                    new Pack{Description = "xxx", PackName= "sam"}
                });

            }

            var items = new List<Foo>
               {
                   new Foo { Name = "ahmad"},
                   new Foo { Name = "ali"},
                   new Foo { Name = "mahamad"},
                   new Foo { Name = "ali"},
                   new Foo { Name = "reza"},
                   new Foo { Name = "beh"},
                   new Foo { Name = "ad"},
                   new Foo { Name = "hjgj"},
                   new Foo { Name = "xcfs"},

               };


            ViewBag.Title = "تولید سریع کتاب";
            ViewBag.Ahmad = "تولید سریع کتاب";
            ViewData["xx"] = "salam";

            return View("Index", items);
        }

       
 
        /*  public ActionResult SaveUploadedFile()
          {
              bool isSavedSuccessfully = true;
              string fName = "";
              foreach (string fileName in Request.Files)
              {
                  HttpPostedFileBase file = Request.Files[fileName];
                  //Save file content goes here
                  fName = file.FileName;
                  if (file != null && file.ContentLength > 0)
                  {

                      var originalDirectory = new DirectoryInfo(string.Format("{0}Images", Server.MapPath(@"\")));

                      string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                      var fileName1 = Path.GetFileName(file.FileName);


                      bool isExists = System.IO.Directory.Exists(pathString);

                      if (!isExists)
                          System.IO.Directory.CreateDirectory(pathString);

                      var path = string.Format("{0}\\{1}", pathString, file.FileName);
                      file.SaveAs(path);

                  }

              }

              if (isSavedSuccessfully)
              {
                  return Json(new { Message = fName });
              }
              else
              {
                  return Json(new { Message = "Error in saving file" });
              }
          }


          public ActionResult Index()
          {
              return View();
          }

          public ActionResult GetAttachments()
          {
              //Get the images list from repository
              var attachmentsList = new List<AttachmentsModel>
              {
                  new AttachmentsModel {AttachmentID = 1, FileName = "/images/wallimages/dropzonelayout.png", Path = "/images/wallimages/dropzonelayout.png"},
                  new AttachmentsModel {AttachmentID = 1, FileName = "/images/wallimages/imageslider-3.png", Path = "/images/wallimages/imageslider-3.png"}
              }.ToList();

              return Json(new { Data = attachmentsList }, JsonRequestBehavior.AllowGet);
          }
      }

      namespace DropZoneFileUpload.Models
      {
          public class AttachmentsModel
          {
              public long AttachmentID { get; set; }
              public string FileName { get; set; }
              public string Path { get; set; }
          }
      }*/

        // GET: Test/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Test/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Test/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Test/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Test/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Test/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Test/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



    }
}
