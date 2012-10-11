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
        public JsonResult SendMail()
        {
            string smtpHost = ConfigurationManager.AppSettings["MAILGUN_SMTP_SERVER"];
            string smtpLogin = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];

            var GuestName = Request["GuestName"];
            var GuestEmail = Request["GuestEmail"];
            var MsgContent = Request["MsgContent"];
            var challengeGuid = Request["challengeGuid"];
            var attemptedText = Request["attemptedText"];

            if (MsgContent.Count() == 0)
            {
                
            }

            MsgContent = "This message has no content";

            var sb = new StringBuilder();
            sb.Append(MsgContent);            
            sb.Append(Environment.NewLine);
            sb.Append(GuestEmail);            
            sb.Append(Environment.NewLine);
            sb.Append(GuestName);
            sb.Append(Environment.NewLine);
            var bodyEmail = string.Format("{0}\r\n{1}\r\n{2}", MsgContent, GuestEmail, GuestName);

            string SessionKeyPrefix = "_Captcha";            
            string solution = (string) Session[SessionKeyPrefix + challengeGuid];
            Session.Remove(SessionKeyPrefix + challengeGuid);

            if (!string.IsNullOrEmpty(solution) && solution.Equals(attemptedText))
            {

                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("pragmaticobjects@gmail.com"));
                mail.From = new MailAddress(smtpLogin);
                mail.Subject = "SynergisticStudio Contact Message";
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
