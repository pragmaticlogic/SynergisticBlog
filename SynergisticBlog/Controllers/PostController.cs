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
    public class PostController : BaseController
    {
        public ActionResult Index(string id, string page)
        {
            var post = _collection.FindOneById(ObjectId.Parse(id));
            return View(post);
        }
    }
}
