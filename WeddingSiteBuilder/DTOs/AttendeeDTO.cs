namespace WeddingSiteBuilder.DTOs
{
    public class AttendeeDTO
    {
        public long WeddingId { get; set; }
        public long? AttendeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool PartyMember { get; set; }
        public string Email { get; set; }
        public string Side { get; set; }
        public string Role { get; set; }
        public string Blurb { get; set; }
    }
}