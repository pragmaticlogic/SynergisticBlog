using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using SynergisticBlog.Models;

namespace SynergisticBlog.Controllers
{
    public class HomeController : BaseController
    {
        private string DEFAULT_PAGE = "Blog";

        public ActionResult Index(string page)
        {
            ViewBag.Message = "Synergistic Studio";            

            if (string.IsNullOrEmpty(page))
            {
                page = DEFAULT_PAGE;
            }            

            var filter = @"{'Page': '" + page + "'}";
            
            //var mgCollection = _collection.Find(new QueryDocument(QueryDocument.Parse(filter)));
            var mgCollection = _collectionPost.Find(Query.EQ("Page", page));

            ViewData["Page"] = page;
            return View(mgCollection.ToList<Post>().OrderByDescending(p => p.TimeCreated));           
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(Post post)
        {
            _collectionPost.Insert(post);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            var query = Query.EQ("Key", "About");
            var collection = _collectionItem.Find(query);
            if (collection != null)
            {
                return View(collection.ToList<Item>().FirstOrDefault());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Contact()
        {
            string SessionKeyPrefix = "_Captcha";
            string challengeGuid = Guid.NewGuid().ToString();

            Session[SessionKeyPrefix + challengeGuid] = MakeRandomSolution();
            ViewData["challengeGuid"] = challengeGuid;
            return View();
        }

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
