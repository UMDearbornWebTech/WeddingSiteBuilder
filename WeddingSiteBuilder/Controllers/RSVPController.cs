using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.DTOs;
using WeddingSiteBuilder.ReadModel;
using WeddingSiteBuilder.Services;

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
        public AttendeeWithRSVP Post([FromBody]AttendeeDTO request)
        {
            using(var dbContext = new WeddingSiteBuilderEntities())
            {
                var attendee = new Attendee()
                {
                    Side = request.Side,
                    WeddingID = request.WeddingId,
                    Person = new Person()
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email
                    }
                };

                dbContext.Attendees.Add(attendee);
                var changesSaved = dbContext.SaveChanges();

                var rsvp = new RSVPLink()
                {
                    AttendeeID = attendee.AttendeeID,
                    GuidToken = new Guid()
                };

                dbContext.RSVPLinks.Add(rsvp);
                changesSaved += dbContext.SaveChanges();

                var attendeeWithRSVP = (AttendeeWithRSVP)attendee;
                attendeeWithRSVP.RSVP = rsvp;

                SendRSVPEmail(attendeeWithRSVP, dbContext);

                return attendeeWithRSVP;
            }
        }

        // PUT: api/RSVP/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RSVP/5
        public void Delete(int id)
        {
        }

        private void SendRSVPEmail(AttendeeWithRSVP request, WeddingSiteBuilderEntities dbContext)
        {

            var couple = dbContext.Attendees.Where(a => a.WeddingID == request.WeddingID && (a.WeddingRole == "Bride" || a.WeddingRole == "Groom")).ToList();
            string subject;

            if(couple.Count() == 1)
            {
                var herOrHis = couple.FirstOrDefault().WeddingRole == "Bride" ? "her" : "his";
                subject = "Hey " + request.Person.FirstName + ", let {0} know if you'll be coming to " + herOrHis + " wedding";
            }
            else 
            {
                subject = "Hey " + request.Person.FirstName + ", let {0} and {1} know if you'll be coming to their wedding";
            }

            var message = new StringBuilder();
            message.AppendLine("RSVP for the wedding by clicking on the link below.");
            message.AppendLine(string.Format("http://localhost:59998/Views/sendviewrsvp.html?token={0}", request.RSVP.GuidToken));

            EmailService.Instance.SendEmail(
                request.Person.Email,
                couple.Count() == 1 ? string.Format(subject, couple.FirstOrDefault().Person.FirstName) : string.Format(subject, couple.LastOrDefault().Person.FirstName),
                message.ToString());
        }
    }
}
