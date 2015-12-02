using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.Controllers
{
    public class WeddingCoupleController : ApiController
    {
        // GET: api/WeddingCouple
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WeddingCouple/5
        public bool Get(int id)
        {
            int changesSaved = 0;
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                var wedding = new Wedding()
                {
                    WeddingID = id,
                    CoupleStory = "Oh My So, romantic",
                    CreateDate = DateTime.Now,
                    LastUpdated = DateTime.Now
                };
                dbContext.Weddings.Add(wedding);
                changesSaved = dbContext.SaveChanges();
            }
            return changesSaved > 0;
        }

        /*
        Try making this call for the post below
        $.ajax({
            type: "POST",
            data :JSON.stringify(body),
            url: "api/WeddingCouple",
            contentType: "application/json"
        });
        */

        // POST: api/WeddingCouple
        public dto Post([FromBody]dto value)
        {
            return value;
        }

        // PUT: api/WeddingCouple/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WeddingCouple/5
        public void Delete(int id)
        {
        }
    }

    public class dto
    {
        public string Key;
        public string Value;
    }
}
