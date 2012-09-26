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
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var filter = @"{'Page': 'Blog'}";
            
            var postCollection = _collection.Find(new QueryDocument(QueryDocument.Parse(filter)));
            //var a = _collection.FindAll();

            return View(postCollection.ToList<Post>());           
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
