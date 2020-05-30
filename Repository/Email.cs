using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class Email : IEmail
    {
        public void SendEmail(string to, string subject, string emailBody)
        {
             
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("m4m4esam9397@gmail.com");
            msg.To.Add(to);
            msg.Subject = subject;
            msg.Body = emailBody;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("m4m4esam9397@gmail.com","..123456");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }



        }

    }
}