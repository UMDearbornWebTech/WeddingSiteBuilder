using System;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.DTOs
{
    public class AttendeeModel
    {
        public long AttendeeID { get; set; }
        public long PersonID { get; set; }
        public long WeddingID { get; set; }
        public string WeddingRole { get; set; }
        public string Relationship { get; set; }
        public string Side { get; set; }
        public bool? Attending { get; set; }
        public bool? PartyMember { get; set; }
        public string PartyMemberBlurb { get; set; }
        public bool? RSVPStatus { get; set; }
        public int? NumberofRSVPs { get; set; }
        public PersonModel Person { get; set; }

        public AttendeeModel(Attendee att)
        {
            AttendeeID = att.AttendeeID;
            PersonID = att.PersonID;
            WeddingID = att.WeddingID;
            WeddingRole = att.WeddingRole;
            Relationship = att.Relationship;
            Side = att.Side;
            Attending = att.Attending;
            PartyMember = att.PartyMember;
            PartyMemberBlurb = att.PartyMemberBlurb;
            RSVPStatus = att.RSVPStatus;
            NumberofRSVPs = att.NumberofRSVPs;
            Person = new PersonModel(att.Person);
        }
    }
}