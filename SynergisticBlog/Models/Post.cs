using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergisticBlog.Models
{
    public class Post : Entity
    {
        public string Page { get; set; }
        public string Title { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Content { get; set; }
        public string UUID { get; set; }
    }
}