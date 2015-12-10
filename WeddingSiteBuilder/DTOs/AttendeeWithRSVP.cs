using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.DTOs
{
    public class AttendeeWithRSVP : Attendee 
    {
        public RSVPLink RSVP { get; set; }
    }
}