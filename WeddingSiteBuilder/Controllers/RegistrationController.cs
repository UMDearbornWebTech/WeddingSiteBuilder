using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.DTOs;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.Controllers
{
    public class RegistrationController : ApiController
    {
        // GET: api/Registration
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Registration/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Registration
        public long Post([FromBody]RegistrationDTO request)
        {
            long changesSaved = 0;
            try
            {
                using (var dbContext = new WeddingSiteBuilderEntities())
                {
                    var person = new Person()
                    {
                        Email = request.Email,
                        Password = request.Password,
                        FirstName = request.FirstName,
                        LastName = request.LastName
                    };
                    dbContext.People.Add(person);
                    changesSaved = dbContext.SaveChanges();


                    person = dbContext.People.Where(p => p.Email == request.Email).FirstOrDefault();

                    changesSaved = person.PersonID;
                }
            }
            catch(Exception e)
            {
                var a = e;
            }
            
            return changesSaved;
        }

        // PUT: api/Registration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Registration/5
        public void Delete(int id)
        {
        }
    }
}
