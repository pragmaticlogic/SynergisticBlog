using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SynergisticBlog.Security
{
    public static class CaptchaHelper
    {
        internal const string SessionKeyPrefix = "_Captcha";
        private const string ImgFormat = "<img src=\"{0}\" />";
        
        //public static string Captcha(this HtmlHelper html, string name)
        /*
        public static string Captcha(ViewContext viewContext, string name)
        {
            string challengeGuid = Guid.NewGuid().ToString();

            var session = viewContext.HttpContext.Session;
            session[SessionKeyPrefix + challengeGuid] = MakeRandomSolution();

            var urlHelper = new UrlHelper(viewContext.RequestContext);
            string url = urlHelper.Action("Render", "CaptchaImage", new { challengeGuid });
            return string.Format(ImgFormat, url) + Html.Hidden(name, challengeGuid);
        }*/

        private static string MakeRandomSolution()
        {
            Random rng = new Random();
            int length = rng.Next(5, 7);
            char[] buf = new char[length];

            for (int i = 0; i < length; i++)
            {
                buf[i] = (char)('a' + rng.Next(26));
            }

            return new string(buf);
        }
    }
}