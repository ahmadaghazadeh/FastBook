﻿
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Dapper;

using FastBookCreator.Core;
using FastBookCreator.Models;
using HighlightCode.App_Start;
using Newtonsoft.Json.Linq;

namespace FastBookCreator.Controllers
{
    public class SharedApiController : ApiController
    {

        // POST: api/Shared
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Shared/GetHighLightAndroid")]
        public IHttpActionResult GetHighLightAndroid(JObject jsonData)
        {
            dynamic json = jsonData;
            string code = json.code;
            string lang = json.lang;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(code);
            var pTags = doc.DocumentNode.Descendants("code");
            foreach (var tag in pTags)
            {
                tag.InnerHtml = tag.InnerHtml.ToHighLightAndroidFormat(lang);
            }
            return Ok(doc.DocumentNode.OuterHtml);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Shared/GetHighLight")]
        public IHttpActionResult GetHighLight(JObject jsonData)
        {
            dynamic json = jsonData;
            string code = json.str;
            string lng = json.str;
            return Ok(code.ToHighLightFormat(lng));
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Shared/ResPacks")]
        public IHttpActionResult ResPacks(JObject jsonData)
        {
            dynamic json = jsonData;
            var userId = json.userId.ToString();
            var packId = json.packId.ToString();
            var where = json.where.ToString();
            using (SQLiteConnection connection = SqliteConn.GetPackDb(userId, packId))
            {
                var items = connection.Query<ResPack>($"SELECT * FROM RESOURCE {where}");
                var query = from i in items
                            select new { NAME = i.NAME, DATA = HelperExtensions.Image("img-" + i._id.ToString(), i.DATA, $"class='img-responsive' alt='{i._id.ToString()}'"), _id = i._id.ToString() };
                return Ok(query);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Pack/SavePack")]
        public IHttpActionResult SavePack(JObject jObject)
        {
            dynamic json = jObject;
            var pack = new Pack()
            {
                COLOR = int.Parse(json.COLOR.ToString(), System.Globalization.NumberStyles.HexNumber),
                PACK_NAME = json.PACK_NAME,
                METHOD_ID = json.METHOD_ID,
                SUBJECT_ID = json.SUBJECT_ID,
                DESCRIPTION = json.DESCRIPTION,
            };

            int result;
            if (json._id.ToObject<int>() == 0)
            {
                var insertQuery = @"INSERT INTO [PACKS](PACK_NAME,METHOD_ID,SUBJECT_ID,COLOR,DESCRIPTION) VALUES (@PACK_NAME,@METHOD_ID,@SUBJECT_ID,@COLOR,@DESCRIPTION);
                                    select last_insert_rowid();";
                result = SqliteConn.GetCommonDb().Query<int>(insertQuery, pack).SingleOrDefault();
            }
            else
            {
                var updateQuery = $"UPDATE [PACKS] SET PACK_NAME=@PACK_NAME ,METHOD_ID = @METHOD_ID,SUBJECT_ID = @SUBJECT_ID,COLOR=@COLOR,DESCRIPTION=@DESCRIPTION WHERE _id={json._id} ";
                SqliteConn.GetCommonDb().Execute(updateQuery, pack);
                result = json._id.ToObject<int>();
            }
            return Ok(result);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Lesson/SaveLesson")]
        public IHttpActionResult SaveLesson(JObject jObject)
        {

            dynamic json = jObject;
            string userId = json.userId.ToString();
            string packId = json.packId.ToString();
            string resourceId = json.resourceId.ToString();
            var lesson = new Lesson()
            {
                COLOR = int.Parse(json.COLOR.ToString(), System.Globalization.NumberStyles.HexNumber),
                LESSON_TITLE = json.LESSON_TITLE,
                RESOURCE_ID = long.Parse(resourceId)
            };

            int result;
            if (json._id.ToObject<int>() == 0)
            {
                var insertQuery = @"INSERT INTO [Lesson](LESSON_TITLE,COLOR,RESOURCE_ID) VALUES (@LESSON_TITLE,@COLOR,@RESOURCE_ID);
                                    select last_insert_rowid();";
                result = SqliteConn.GetPackDb(userId, packId).Query<int>(insertQuery, lesson).SingleOrDefault();
            }
            else
            {
                var updateQuery = $"UPDATE [Lesson] SET LESSON_TITLE=@LESSON_TITLE  ,COLOR=@COLOR,RESOURCE_ID,@RESOURCE_ID WHERE _id={json._id} ";
                SqliteConn.GetPackDb(userId, packId).Execute(updateQuery, lesson);
                result = json._id.ToObject<int>();
            }
            return Ok(result);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Page/SavePage")]
        public IHttpActionResult SavePage(JObject jObject)
        {
            dynamic json = jObject;
            string userId = json.userId.ToString();
            string packId = json.packId.ToString();
            string lessonId = json.lessonId.ToString();
            string pageTypeId = json.PAGE_TYPE_ID.ToString();
            var page = new Page()
            {
                LESSON_ID = long.Parse(lessonId),
                PAGE_TYPE_ID = long.Parse(pageTypeId),
            };

            int result;
            if (json._id.ToObject<int>() == 0)
            {
                var insertQuery = @"INSERT INTO [PAGE](LESSON_ID,PAGE_TYPE_ID) VALUES (@LESSON_ID,@PAGE_TYPE_ID);
                                    select last_insert_rowid();";
                result = SqliteConn.GetPackDb(userId, packId).Query<int>(insertQuery, page).SingleOrDefault();
            }
            else
            {
                var updateQuery = $"UPDATE [PAGE] SET LESSON_ID=@LESSON_ID  ,PAGE_TYPE_ID=@PAGE_TYPE_ID  WHERE _id={json._id} ";
                SqliteConn.GetPackDb(userId, packId).Execute(updateQuery, page);
                result = json._id.ToObject<int>();
            }
            return Ok(result);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Item/SaveItem")]
        public IHttpActionResult SaveItemHtml(JObject jObject)
        {
            dynamic json = jObject;
            string userId = json.userId.ToString();
            string packId = json.packId.ToString();
            string lessonId = json.LESSON_ID.ToString();
            string pageId = json.PAGE_ID.ToString();
            string itemTypeId = json.ITEM_TYPE_ID.ToString();
            long id = json._id.ToObject<int>();
            var item = new Item()
            {
                LESSON_ID = long.Parse(lessonId),
                ITEM_TYPE_ID = long.Parse(itemTypeId),
                PAGE_ID = long.Parse(pageId),
                ITEM_TITLE = json.ITEM_TITLE,
                CONTENT = json.content,
                RESOURCE_ID = json.RESOURCE_ID ?? 0,
                _id = id
            };

            long result;
            if (id == 0)
            {
                var insertQuery = @"INSERT INTO [ITEM](ITEM_TYPE_ID,PAGE_ID,LESSON_ID,ITEM_TITLE,CONTENT,RESOURCE_ID) VALUES (@ITEM_TYPE_ID,@PAGE_ID,@LESSON_ID,@ITEM_TITLE,@CONTENT,@RESOURCE_ID);
                                    select last_insert_rowid();";
                result = SqliteConn.GetPackDb(userId, packId).Query<int>(insertQuery, item).SingleOrDefault();
            }
            else
            {
                var updateQuery = $"UPDATE [ITEM] SET ITEM_TYPE_ID=@ITEM_TYPE_ID,PAGE_ID=@PAGE_ID,LESSON_ID=LESSON_ID,ITEM_TITLE=@ITEM_TITLE,CONTENT=@CONTENT ,RESOURCE_ID=@RESOURCE_ID WHERE _id=@_id ";
                SqliteConn.GetPackDb(userId, packId).Execute(updateQuery, item);
                result = id;
            }
            return Ok(result);
        }

     
    }
}
