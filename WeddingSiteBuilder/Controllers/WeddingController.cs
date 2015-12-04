using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeddingSiteBuilder.Controllers
{
    public class WeddingController : ApiController
    {
        // GET: api/Wedding
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Wedding/5
        public string Get(int WeddingId)
        {
            return "hey";
        }

        // POST: api/Wedding
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Wedding/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Wedding/5
        public void Delete(int id)
        {
        }
    }
}
