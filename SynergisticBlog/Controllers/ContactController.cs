using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Security;
using System.Configuration;
using System.Text;

namespace SynergisticBlog.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        public JsonResult SendMail(string GuestName, string GuestEmail, string MsgContent)
        {
            string smtpHost = ConfigurationManager.AppSettings["MAILGUN_SMTP_SERVER"];
            string smtpLogin = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];

            //var GuestName = Request["GuestName"];
            //var GuestEmail = Request["GuestEmail"];
            //var MsgContent = Request["MsgContent"];

            var sb = new StringBuilder();
            sb.Append(GuestName);
            sb.Append(Environment.NewLine);
            sb.Append(GuestEmail);
            sb.Append(Environment.NewLine);
            sb.Append(MsgContent);

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("pragmaticobjects@gmail.com"));
            mail.From = new MailAddress(smtpLogin);
            mail.Subject = "SynergisticStudio Contact Message";
            mail.Body = sb.ToString();

            SmtpClient SMTPServer = new SmtpClient(smtpHost);
            try
            {                
                SMTPServer.Send(mail);
            }
            catch (Exception ex)
            {
                return Json(new { status = ex.Message });
            }
            return Json(new { status = "Your message has been sent." });
        }
    }
}
