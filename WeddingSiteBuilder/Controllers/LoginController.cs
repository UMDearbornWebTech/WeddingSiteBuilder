using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login?userName=dummy&password=pword
        public long Get(string Email, string Password)
        {
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                var existingLogin = dbContext.People.Where(p => p.Email == Email && p.Password == Password).FirstOrDefault();
                if(existingLogin != null)
                {
                    var attendee = dbContext.Attendees.Where(a => a.PersonID == existingLogin.PersonID).FirstOrDefault();
                    if(attendee != null)
                    {
                        return attendee.WeddingID;
                    }
                    return 0;
                }

                return 0;
            }
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
