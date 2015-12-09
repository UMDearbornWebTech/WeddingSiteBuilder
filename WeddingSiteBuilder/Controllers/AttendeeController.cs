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
        public bool Post([FromBody]AttendeeDTO request)
        {
            using(var dbContext = new WeddingSiteBuilderEntities())
            {
                var wedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingID == request.WeddingId);
                if (wedding != null)
                {
                    if (request.AttendeeId.HasValue)
                    {
                        var existingAttendee = dbContext.Attendees.FirstOrDefault(a => a.AttendeeID == request.AttendeeId.Value);
                        if (existingAttendee != null)
                        {
                            if (!string.IsNullOrWhiteSpace(request.FirstName)) existingAttendee.Person.FirstName = request.FirstName;
                            if (!string.IsNullOrWhiteSpace(request.LastName)) existingAttendee.Person.LastName = request.LastName;
                            if (!string.IsNullOrWhiteSpace(request.Email)) existingAttendee.Person.Email = request.Email;
                            if (!string.IsNullOrWhiteSpace(request.Relationship)) existingAttendee.Relationship = request.Relationship;
                            if (!string.IsNullOrWhiteSpace(request.Role)) existingAttendee.WeddingRole = request.Role;
                            if (request.Role.ToLower() == "bride" || request.Role.ToLower() == "groom")
                            {
                                existingAttendee.Side = request.Role;
                            }
                            else if (request.Side.ToLower() == "bride" || request.Side.ToLower() == "groom")
                            {
                                existingAttendee.Side = request.Side;
                            }
                            if (!string.IsNullOrWhiteSpace(request.Blurb)) existingAttendee.PartyMemberBlurb = request.Blurb;
                            existingAttendee.PartyMember = request.PartyMember;

                            try
                            {
                                var changesSaved = dbContext.SaveChanges();
                                return true;
                            }
                            catch (Exception)
                            {
                                return false;
                            }
                            
                        }
                    }
                    else if (!(string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || (request.Role.ToLower() != "bride" && request.Role.ToLower() != "groom")))
                    {
                        if (request.Role.ToLower() == "bride" || request.Role.ToLower() == "groom")
                        {
                            request.Side = request.Role;
                        }
                        else if (!(request.Side.ToLower() == "bride" || request.Side.ToLower() == "groom"))
                        {
                            request.Side = "Bride";
                        }

                        var attendee = new Attendee()
                        {
                            Side = request.Side,
                            WeddingRole = request.Role,
                            Relationship = request.Relationship,
                            PartyMemberBlurb = request.Blurb,
                            PartyMember = request.PartyMember,
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
                        return (changesSaved == 2);
                    }
                }
                return false;
            }
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
