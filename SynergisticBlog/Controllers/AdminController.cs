using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SynergisticBlog.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace SynergisticBlog.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        public ActionResult Index(string page, bool update, string id)
        {            
            Post post = null;

            if (update)
            {
                post = _collection.FindOneById(ObjectId.Parse(id));
            }
            else
            {
                post = new Post()
                {
                    Page = page,
                    TimeCreated = DateTime.MinValue,
                };
            }
            
            return View(post);
        }

        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Post post)
        {
            var query = Query.EQ("UUID", post.UUID);
            var update = Update.Set("Title", post.Title)
                .Set("Content", post.Content);

            if (post.TimeCreated == DateTime.MinValue)
            {                
                post.TimeCreated = DateTime.Now;
                post.UUID = System.Guid.NewGuid().ToString();
                //_collection.Insert(post);
                _collection.Save(post);
            }
            else
            {
                _collection.Update(query, update);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeletePost(string id)
        {
            var query = Query.EQ("_id", ObjectId.Parse("\"" + id + "\""));
            _collection.Remove(query);
            //return RedirectToAction("Index", "Home");
            return Json(new { success = "true" });
        }
    }
}
