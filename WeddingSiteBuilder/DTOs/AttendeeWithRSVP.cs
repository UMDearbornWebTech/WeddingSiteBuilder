using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.DTOs
{
    public class AttendeeWithRSVP 
    {
        public AttendeeModel Attendee { get; set; }
        public RSVPLinkModel RSVP { get; set; }
    }
}