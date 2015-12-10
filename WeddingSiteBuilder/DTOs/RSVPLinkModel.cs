using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.DTOs
{
    public class RSVPLinkModel
    {
        public long? RSVPLinkID { get; set; }
        public long? AttendeeID { get; set; }
        public string RSVPNameBlub { get; set; }
        public Guid GuidToken { get; set; }
        public bool IsAnswered { get; set; }

        public RSVPLinkModel(RSVPLink rsvp)
        {
            RSVPLinkID = rsvp.RSVPLinkID;
            AttendeeID = rsvp.AttendeeID;
            RSVPNameBlub = rsvp.RSVPNameBlub;
            GuidToken = rsvp.GuidToken;
            IsAnswered = rsvp.IsAnswered;
        }
    }
}