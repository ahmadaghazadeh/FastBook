using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Dapper;
using FastBookCreator.Entities;
using FastBookCreator.Models;

namespace FastBookCreator.Controllers
{
    public class HomeController : BaseController
    {
       /* public ActionResult Index()
        {
            return View();
        }*/

        public JsonResult GetItems(string sidx, string sort, int page, int rows) //, bool search, string searchField, string searchOper, string searchString)
        {
            //var data = Connections.FastBook.SqlConn.Query<Item>("select * from Item");

            //sort = sort ?? "";
            //int pageIndex = Convert.ToInt32(page) - 1;
            //int pageSize = rows;
            //int totalRecords = data.Count();
            //var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //if (sort.ToUpper() == "DESC")
            //{
            //    data = data.OrderByDescending(t => t.Title);
            //    data = data.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //else
            //{
            //    data = data.OrderBy(t => t.Title);
            //    data = data.Skip(pageIndex * pageSize).Take(pageSize);
            //}


            //var jsonData = new
            //{
            //    total = totalPages,
            //    page,
            //    records = totalRecords,
            //    rows = data
            //};

            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            return Json("");
        }

        [System.Web.Mvc.HttpPost]
        public string Create([Bind(Exclude = "id")] Item model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Connections.FastBook.SqlConn.Query("Insert into Item values(@ItemTypeId, @PageId, @LessonId, @Title, @Content)", model);
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }

            return msg;
        }


        [System.Web.Mvc.HttpPost]
        public string Edit(Item model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    Connections.FastBook.SqlConn.Query(@"Update Item Set 
                        ItemTypeId = @ItemTypeId, PageId = @PageId, LessonId = @LessonId, Title = @Title, Contact = @Content
                        where id = @id", model);

                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string Delete(string id)
        {
            Connections.FastBook.SqlConn.Query(@"Delete from Item where id = @id", new { id });

            return "Deleted successfully";
        }

        public ActionResult Index()
        {
            

            
            return View("Index");
        }
    }
}