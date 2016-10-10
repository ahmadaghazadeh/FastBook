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
        /*// GET: api/SharedApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SharedApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SharedApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SharedApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SharedApi/5
        public void Delete(int id)
        {
        }*/

        
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
            using (var connection = SqliteConn.GetPackDb())
            {
                var items = connection.Query<ResPack>($"SELECT * FROM RESOURCE {json.where}");
                var query = from i in items
                            select new { NAME = i.NAME, DATA = HelperExtensions.Image(i._id.ToString(), i.DATA, $"class='img-responsive' alt='{i._id.ToString()}'"), _id = i._id.ToString() };
                return Ok(query);
            }
        }
    }
}
