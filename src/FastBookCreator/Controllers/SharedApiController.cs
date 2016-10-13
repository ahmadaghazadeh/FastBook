using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Shared/GetHighLight")]
        public IHttpActionResult GetHighLightAndroid(JObject jsonData)
        {
            dynamic json = jsonData;
            string code = json.str;
            string lng = json.str;
            return Ok(code.ToHighLightAndroidFormat(lng));
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Shared/GetHighLight")]
        public IHttpActionResult GetHighLight(JObject jsonData)
        {
            dynamic json = jsonData;
            string code = json.str;
            string lng = json.str;
            return Ok(code.ToHighLightFormat(lng));
        }

        [HttpPost]
        [Route("Shared/ResPacks")]
        public IHttpActionResult ResPacks(JObject jsonData)
        {
            dynamic json = jsonData;
            var userId = json.userId;
            var packId = json.packId;
            var where = json.where;
            using (var connection = SqliteConn.GetPackDb(userId, packId))
            {
                List<ResPack> items = connection.Query<ResPack>($"SELECT * FROM RESOURCE {where}");
                var query = from i in items
                            select new { NAME = i.NAME, DATA = HelperExtensions.Image(i._id.ToString(), i.DATA, $"class='img-responsive' alt='{i._id.ToString()}'"), _id = i._id.ToString() };
                return Ok(query);
            }
        }


        [HttpPost]
        [Route("Pack/SavePack")]
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
                result = SqliteConn.GetCommonDb().Query<int>(insertQuery, pack).Single();
            }
            else
            {
                var updateQuery = $"UPDATE [PACKS] SET PACK_NAME=PACK_NAME ,METHOD_ID = @METHOD_ID,SUBJECT_ID = @SUBJECT_ID,COLOR=@COLOR,DESCRIPTION=@DESCRIPTION WHERE _id={json._id} ";
                SqliteConn.GetCommonDb().Execute(updateQuery, pack);
                result = json._id.ToObject<int>();
            }
            return Ok(result);
        }
    }
}
