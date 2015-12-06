using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using WeddingSiteBuilder.Services.Interfaces;

namespace WeddingSiteBuilder.Services
{
    public class EmailService : IEmailService
    {
        public static IEmailService Instance;

        private static SmtpClient _client;

        static EmailService()
        {
            Instance = new EmailService();
        }

        private EmailService()
        {
            _client = new SmtpClient("smtp.gmail.com");
            _client.EnableSsl = true;
            _client.Port = 587;
            _client.Credentials = new NetworkCredential("weddingsitebuilder@gmail.com",
               ConfigurationManager.AppSettings["emailPassword"].ToString());
        }

        public void SendEmail(string emailAddress, string subject, string message)
        {
            _client.Send("donot-reply@weddingsitebuilder.com", emailAddress, subject, message);
        }
    }
}