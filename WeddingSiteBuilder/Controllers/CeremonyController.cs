using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.ReadModel;
using WeddingSiteBuilder.DTOs;


namespace WeddingSiteBuilder.Controllers
{
    public class CeremonyController : ApiController
    {
        // GET: api/Ceremony
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ceremony/5
        public Ceremony Get(long WeddingId)
        {
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                if(WeddingId != 0)
                {
                    return dbContext.Ceremonies.FirstOrDefault(c => c.WeddingID == WeddingId);
                }
                return null;
            }
        }

        // POST: api/Ceremony
        public bool Post([FromBody]EventDTO request)
        {
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                var wedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingID == request.WeddingId);
                if(wedding != null)
                {
                    if (wedding.Ceremonies.Any())
                    {
                        var ceremony = wedding.Ceremonies.FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(request.LocationName)) ceremony.LocationName = request.LocationName;
                        if (!string.IsNullOrWhiteSpace(request.Address1)) ceremony.Address1 = request.Address1;
                        if (!string.IsNullOrWhiteSpace(request.City)) ceremony.City = request.City;
                        if (!string.IsNullOrWhiteSpace(request.State)) ceremony.StateProv = request.State;
                        if (!string.IsNullOrWhiteSpace(request.Zip)) ceremony.ZipCode = request.Zip;
                        if (request.Date != null) ceremony.CeremonyDateTime = request.Date.Value;

                        dbContext.SaveChanges();
                        return true;
                    }
                    wedding.Ceremonies.Add(new Ceremony()
                    {
                        LocationName = request.LocationName,
                        Address1 = request.Address1,
                        City = request.City,
                        StateProv = request.State,
                        ZipCode = request.Zip,
                        CeremonyDateTime = request.Date.HasValue ? request.Date.Value : DateTime.Now
                    });
                    var changesSaved = dbContext.SaveChanges();
                    return changesSaved == 1;
                }
                return false;
            }
        }

        // PUT: api/Ceremony/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ceremony/5
        public void Delete(int id)
        {
        }
    }
}
