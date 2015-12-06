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
            int changesSaved = 0;
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

                    var wedding = new Wedding();

                    dbContext.Weddings.Add(wedding);

                    changesSaved = dbContext.SaveChanges();

                    var attendee = new Attendee()
                    {
                        WeddingID = wedding.WeddingID,
                        PersonID = person.PersonID,
                        WeddingRole = request.BrideOrGroom == "Bride" ? "Bride" : "Groom",
                        Side = request.BrideOrGroom == "Bride" ? "Bride" : "Groom"
                    };

                    dbContext.Attendees.Add(attendee);

                    changesSaved += dbContext.SaveChanges();

                    if(changesSaved == 3)
                    {
                        return wedding.WeddingID;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch(Exception)
            {
                return 0;
            }
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
