using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using WeddingSiteBuilder.ReadModel;
using WeddingSiteBuilder.DTOs;
using System;

namespace WeddingSiteBuilder.Controllers
{
    public class ReceptionController : ApiController
    {
        // GET: api/Reception
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reception/5
        public Reception Get(int WeddingId)
        {
            using(var dbContext = new WeddingSiteBuilderEntities())
            {
                var wedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingID == WeddingId);
                if(wedding != null)
                {
                    return wedding.Receptions.FirstOrDefault();
                }

                return null;
            }
        }

        // POST: api/Reception
        public bool Post([FromBody]EventDTO request)
        {
            using (var dbContext = new WeddingSiteBuilderEntities())
            {
                var wedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingID == request.WeddingId);
                if (wedding != null)
                {
                    if (wedding.Receptions.Any())
                    {
                        var reception = wedding.Receptions.FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(request.LocationName)) reception.LocationName = request.LocationName;
                        if (!string.IsNullOrWhiteSpace(request.Address1)) reception.Address1 = request.Address1;
                        if (!string.IsNullOrWhiteSpace(request.City)) reception.City = request.City;
                        if (!string.IsNullOrWhiteSpace(request.Zip)) reception.ZipCode = request.Zip;
                        if (request.Date != null) reception.ReceptionDateTime = request.Date.Value;

                        dbContext.SaveChanges();
                        return true;
                    }
                    wedding.Receptions.Add(new Reception()
                    {
                        LocationName = request.LocationName,
                        Address1 = request.Address1,
                        City = request.City,
                        ZipCode = request.Zip,
                        ReceptionDateTime = request.Date.HasValue ? request.Date.Value : DateTime.Now
                    });
                    var changesSaved = dbContext.SaveChanges();
                    return changesSaved == 1;
                }
                return false;
            }
        }

        // PUT: api/Reception/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reception/5
        public void Delete(int id)
        {
        }
    }
}
