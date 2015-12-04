using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.Controllers
{
    public class PersonController : ApiController
    {
        // GET: api/Person
        public string Get()
        {
            return "hey";
        }

        // GET: api/Person/5
        public Person Get(int personId)
        {
            var person = new Person()
            {
                PersonID = personId,
                FirstName = "Amir",
                LastName = "Riwes"
            };
            return person;
        }

        // POST: api/Person
        public Person Post([FromBody]Person newPerson)
        {
            //var existingPerson = Persons.where(personid == newPerson.PersonId);

            //if(existingPerson != null)
            //{
            //    existingPerson.FirstName = newPerson.FirstName;
            //    existingPerson.LastName = newPerson.LastName;
            //    existingPerson.FirstName = newPerson.FirstName;
            //    existingPerson.FirstName = newPerson.FirstName;
            //}
            return newPerson;
            
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {


        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}
