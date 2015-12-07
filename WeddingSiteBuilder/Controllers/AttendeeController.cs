using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.Controllers
{
    public class AttendeeController : ApiController
    {
        // GET: api/Attendee
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Attendee/5
        public List<Attendee> Get(int WeddingId, string option)
        {
            var attendees = new List<Attendee>();

            using(var dbContext = new WeddingSiteBuilderEntities())
            {
                var retrievedAttendees = dbContext.Attendees.Include("Person").Where(a => a.WeddingID == WeddingId).ToList();

                if(option == "couple")
                {
                    retrievedAttendees = retrievedAttendees.Where(a => a.WeddingRole == "Bride" || a.WeddingRole == "Groom").ToList();
                }
                else if(option == "party")
                {
                    retrievedAttendees = retrievedAttendees.Where(a => a.PartyMember == true).ToList();
                }
                else
                {
                    retrievedAttendees = retrievedAttendees.Where(a => !(a.WeddingRole == "Bride" || a.WeddingRole == "Groom"  || a.PartyMember == true)).ToList();
                }

                attendees.AddRange(retrievedAttendees);
            }

            return attendees;
        }

        // POST: api/Attendee
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Attendee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Attendee/5
        public void Delete(int id)
        {
        }
    }
}
