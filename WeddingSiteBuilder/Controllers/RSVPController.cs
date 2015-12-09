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
    public class RSVPController : ApiController
    {
        // GET: api/RSVP
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RSVP/5
        public List<AttendeeWithRSVP> Get(long WeddingId)
        {
            if (WeddingId != 0)
            {
                using (var dbContext = new WeddingSiteBuilderEntities())
                {
                    var attendees = new List<AttendeeWithRSVP>();

                    dbContext.Attendees
                        .Where(a => a.WeddingID == WeddingId && !(a.WeddingRole == "Bride" || a.WeddingRole == "Groom" || a.PartyMember == true))
                        .ToList()
                        .ForEach(a =>
                        {
                            var attendee = (AttendeeWithRSVP)a;
                            attendee.RSVP = dbContext.RSVPLinks.FirstOrDefault(r => r.AttendeeID == a.AttendeeID);
                            attendees.Add(attendee);
                        });
                    return attendees;
                }
            }
            else
            {
                return new List<AttendeeWithRSVP>();
            }
        }

        // POST: api/RSVP
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/RSVP/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RSVP/5
        public void Delete(int id)
        {
        }
    }
}
