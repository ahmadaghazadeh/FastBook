using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HighlightCode.App_Start;
using Newtonsoft.Json.Linq;
using FastBookCreator.Core;
using FastBookCreator.Models;
using Dapper;

namespace FastBookCreator.Controllers
{
    public class SharedApiController : ApiController
    {
       

        
        // POST: api/Shared
        [HttpPost]
        [Route("Shared/GetHighLightAndroid")]
        public IHttpActionResult GetHighLightAndroid(JObject jsonData)
        {
            dynamic json = jsonData;
            string code = json.str;
            string lng = json.str;
            return Ok(code.ToHighLightAndroidFormat(lng));
        }

        [HttpPost]
        [Route("Shared/GetHighLight")]
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
            using (var connection = SqliteConn.GetPackDb())
            {
                var items = connection.Query<ResPack>($"SELECT * FROM RESOURCE {json.where}");
                var query = from i in items
                            select new {NAME= i.NAME, DATA= HelperExtensions.Image(i._id.ToString(), i.DATA, $"class='img-responsive' alt='{i._id.ToString()}'"),_id=i._id.ToString() };
                return Ok(query);
            }
         }

    }
}
