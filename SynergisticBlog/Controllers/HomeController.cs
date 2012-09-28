using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using SynergisticBlog.Models;
using MongoDB.Driver;

namespace SynergisticBlog.Controllers
{
    public class HomeController : BaseController
    {
        private string DEFAULT_PAGE = "Blog";

        public ActionResult Index(string page)
        {
            ViewBag.Message = "Synergistic Studio";

            string requestedPage = Request["page"];

            if (string.IsNullOrEmpty(requestedPage))
            {
                page = DEFAULT_PAGE;
            }
            else
            {
                page = requestedPage;
            }

            var filter = @"{'Page': '" + page + "'}";
            
            //var mgCollection = _collection.Find(new QueryDocument(QueryDocument.Parse(filter)));
            var mgCollection = _collection.Find(MongoDB.Driver.Builders.Query.EQ("Page", page));

            ViewData["Page"] = page;
            return View(mgCollection.ToList<Post>().OrderByDescending(p => p.TimeCreated));           
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Create(Post post)
        {
            _collection.Insert(post);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }        
    }
}
