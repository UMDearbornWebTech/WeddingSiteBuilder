using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using WeddingSiteBuilder.DTOs;
using WeddingSiteBuilder.ReadModel;
using WeddingSiteBuilder.Services;

namespace WeddingSiteBuilder.Controllers
{
    public class RSVPController : ApiController
    {
        // GET: api/RSVP/yeah
        public AttendeeWithRSVP Get(Guid Id)
        {
            using(var dbContext = new WeddingSiteBuilderEntities())
            {
                var rsvp = dbContext.RSVPLinks.FirstOrDefault(r => r.GuidToken == Id);
                var att = dbContext.Attendees.FirstOrDefault(a => a.AttendeeID == rsvp.AttendeeID);
                if (rsvp == null || att == null) return null;
                var attendee = new AttendeeWithRSVP
                {
                    Attendee = new AttendeeModel(att),
                    RSVP = new RSVPLinkModel(rsvp)
                };

                return attendee;
            }
        }

        public bool Post(long RSVPLinkId, int Count, bool Accepted)
        {
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                var rsvp = dbContext.RSVPLinks.FirstOrDefault(r => r.RSVPLinkID == RSVPLinkId);
                if (rsvp == null) return false;

                var attendee = dbContext.Attendees.FirstOrDefault(a => a.AttendeeID == rsvp.AttendeeID);
                attendee.NumberofRSVPs = Count;
                attendee.Attending = Accepted;
                rsvp.IsAnswered = true;

                dbContext.SaveChanges();
                return true;
            }
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
                            var rsvp = dbContext.RSVPLinks.FirstOrDefault(r => r.AttendeeID == a.AttendeeID);
                            if (rsvp != null)
                            {
                                var attendee = new AttendeeWithRSVP
                                {
                                    Attendee = new AttendeeModel(a),
                                    RSVP = new RSVPLinkModel(rsvp)
                                };
                                attendees.Add(attendee);
                            }
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
                try
                {
                    dbContext.Attendees.Add(attendee);
                    var changesSaved = dbContext.SaveChanges();

                    var rsvp = new RSVPLink()
                    {
                        AttendeeID = attendee.AttendeeID,
                        GuidToken = Guid.NewGuid(),
                        RSVPNameBlub = string.Empty,
                        IsAnswered = false
                    };

                    dbContext.RSVPLinks.Add(rsvp);
                    changesSaved += dbContext.SaveChanges();

                    var attendeeWithRSVP = new AttendeeWithRSVP
                    {
                        Attendee = new AttendeeModel(attendee),
                        RSVP = new RSVPLinkModel(rsvp)
                    };

                    SendRSVPEmail(attendee, rsvp, dbContext);

                    return attendeeWithRSVP;
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        // PUT: api/RSVP/5
        public bool Put(int id)
        {
            try
            {
                using (var dbContext = new WeddingSiteBuilderEntities())
                {
                    var rsvp = dbContext.RSVPLinks.FirstOrDefault(r => r.RSVPLinkID == id);
                    var att = dbContext.Attendees.FirstOrDefault(a => a.AttendeeID == rsvp.AttendeeID);
                    if (rsvp == null || att == null) return false;

                    SendRSVPEmail(att, rsvp, dbContext);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        // DELETE: api/RSVP/5
        public void Delete(int id)
        {
        }

        private void SendRSVPEmail(Attendee attendee, RSVPLink rsvp, WeddingSiteBuilderEntities dbContext)
        {
            var couple = dbContext.Attendees.Where(a => a.WeddingID == attendee.WeddingID && (a.WeddingRole == "Bride" || a.WeddingRole == "Groom")).ToList();
            string subject;

            if(couple.Count() == 1)
            {
                var herOrHis = couple.FirstOrDefault().WeddingRole == "Bride" ? "her" : "his";
                subject = "Hey " + attendee.Person.FirstName + ", let {0} know if you'll be coming to " + herOrHis + " wedding";
            }
            else 
            {
                subject = "Hey " + attendee.Person.FirstName + ", let {0} and {1} know if you'll be coming to their wedding";
            }

            var message = new StringBuilder();
            message.AppendLine("RSVP for the wedding by clicking on the link below.");
            message.AppendLine(string.Format("http://localhost:59998/Views/sendviewrsvp.html?token={0}", rsvp.GuidToken));

            EmailService.Instance.SendEmail(
                attendee.Person.Email,
                couple.Count() == 1 ? string.Format(subject, couple.FirstOrDefault().Person.FirstName) : string.Format(subject, couple.FirstOrDefault().Person.FirstName, couple.LastOrDefault().Person.FirstName),
                message.ToString());
        }
    }
}
