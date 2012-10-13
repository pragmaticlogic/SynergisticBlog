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
        protected readonly MongoCollection<Post> _collectionPost;
        protected readonly MongoCollection<Item> _collectionItem;

        public BaseController()
        {
            _collectionPost = Database.GetCollection<Post>("Posts");
            _collectionItem = Database.GetCollection<Item>("Items");
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
