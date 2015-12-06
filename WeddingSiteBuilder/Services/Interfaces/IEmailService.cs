using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingSiteBuilder.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string emailAddress, string subject, string message);
    }
}