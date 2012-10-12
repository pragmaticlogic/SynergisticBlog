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
        [HttpPost]
        public JsonResult SendMail()
        {
            string smtpHost = ConfigurationManager.AppSettings["MAILGUN_SMTP_SERVER"];
            string smtpLogin = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];

            var GuestName = Request["GuestName"];
            var GuestEmail = Request["GuestEmail"];
            var MsgContent = Request["MsgContent"];
            var challengeGuid = Request["challengeGuid"];
            var attemptedText = Request["attemptedText"];

            if (string.IsNullOrEmpty(GuestName))
            {
                GuestName = "Anonymous";
            }

            if (string.IsNullOrEmpty(GuestEmail))
            {
                GuestName = "Email not revealed";
            }

            if (string.IsNullOrEmpty(MsgContent))
            {
                MsgContent = "This message has no content";
            }            
            
            var bodyEmail = string.Format("From {0} ({1})\r\n\r\n{2}", GuestName, GuestEmail, MsgContent);

            string SessionKeyPrefix = "_Captcha";            
            string solution = (string) Session[SessionKeyPrefix + challengeGuid];
            Session.Remove(SessionKeyPrefix + challengeGuid);

            if (!string.IsNullOrEmpty(solution) && solution.Equals(attemptedText))
            {

                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["CONTACT_EMAIL"]));
                mail.From = new MailAddress(smtpLogin);
                mail.Subject = ConfigurationManager.AppSettings["CONTACT_EMAIL_SUBJECT"];
                mail.Body = bodyEmail;
                

                SmtpClient SMTPServer = new SmtpClient(smtpHost);
                try
                {
                    SMTPServer.Send(mail);
                }
                catch (Exception ex)
                {
                    return Json(new { status = ex.Message });
                }
                return Json(new { status = "Sent" });
            }
            else
            {
                return Json(new { status = "Failed" });
            }
        }
    }
}
