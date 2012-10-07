using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Security;
using System.Configuration;

namespace SynergisticBlog.Controllers
{
    public class ContactController : Controller
    {        
        public ActionResult SendMail()
        {
            string smtpHost = ConfigurationManager.AppSettings["MAILGUN_SMTP_SERVER"];
            string smtpLogin = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];
            

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("pragmaticobjects@gmail.com"));
            mail.From = new MailAddress(smtpLogin);
            mail.Subject = "Test";

            SmtpClient SMTPServer = new SmtpClient(smtpHost);
            try
            {                
                SMTPServer.Send(mail);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View("Your message has been sent.");
        }
    }
}
