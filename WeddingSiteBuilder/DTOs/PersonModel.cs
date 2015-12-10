using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingSiteBuilder.ReadModel;

namespace WeddingSiteBuilder.DTOs
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public PersonModel(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Email = person.Email;
        }
    }
}