using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using SynergisticBlog.Models;

namespace SynergisticBlog.Controllers
{
    public class HomeController : BaseController
    {
        protected readonly MongoCollection<Post> _collection;

        public HomeController()
        {
            if (Database != null)
            {
                _collection = Database.GetCollection<Post>("Blogs");
            }
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var filter = @"{'Page': 'Blog'}";

            //return View(_collection.Find(new QueryDocument(QueryDocument.Parse(filter))).ToList<Post>());
            return View();
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
