using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoodLocWeb.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodLocWeb.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        [Route("Contact/SendEmail")]
        public string SendEmail(Contact contact)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Arginine Software", "argininesoftware@gmail.com"));
            message.To.Add(new MailboxAddress("Food Loc", "argininesoftware@gmail.com"));
            message.Subject = contact.Subject;
            message.Body = new TextPart("plain")
            {
                Text = "Contacted By: " + contact.Name + " For " + contact.Subject + "\n" + "Message: " + contact.Message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("argininesoftware@gmail.com", "Building@12");
                client.Send(message);
                client.Disconnect(true);
            }
            return "Success";
            // client.Credentials = new NetworkCredential("argininesoftware@gmail.com", "Building@12");
        }
        [Route("Contact/Suscribe/{email}")]
        public string Suscribe(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Arginine Software", "argininesoftware@gmail.com"));
            message.To.Add(new MailboxAddress("Hi Food Lover,", email));
            message.Subject = "Hello from Food Loc";
            message.Body = new TextPart("html")
            {
                Text = "Hello from FoodLoc. \n" +
                       "Keep posting pictures and help people discovering food.:)"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("argininesoftware@gmail.com", "Building@12");
                client.Send(message);
                client.Disconnect(true);
            }
            return "Suscribed Successfully!!";
            // client.Credentials = new NetworkCredential("argininesoftware@gmail.com", "Building@12");
        }
    }
}
