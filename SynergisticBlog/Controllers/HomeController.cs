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
        public ActionResult Index(string page)
        {
            ViewBag.Message = "Synergistic Studio";

            var filter = @"{'Page': '" + page + "'}";
            
            var mgCollection = _collection.Find(new QueryDocument(QueryDocument.Parse(filter)));            

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
