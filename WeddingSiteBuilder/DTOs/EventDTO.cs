using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingSiteBuilder.DTOs
{
    public class EventDTO
    {
        public long WeddingId { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public DateTime? Date { get; set; }
    }
}