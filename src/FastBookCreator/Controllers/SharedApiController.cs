using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}
