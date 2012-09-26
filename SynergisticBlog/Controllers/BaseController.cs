using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MongoDB.Driver;
using SynergisticBlog.Models;

namespace SynergisticBlog.Controllers
{
    public class BaseController : Controller
    {
        protected readonly MongoCollection<Post> _collection;

        public BaseController()
        {
            _collection = Database.GetCollection<Post>("Blogs");
        }

        public MongoDatabase Database
        {
            get
            {
                return MongoDatabase.Create(GetMongoDbConnectionString());
            }
        }

        private string GetMongoDbConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                ConfigurationManager.AppSettings.Get("MONGOLAB_URI") ??
                "mongodb://localhost/Things";
        }
    }
}
