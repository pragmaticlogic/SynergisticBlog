using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SynergisticBlog.Models
{
    public class Item : Entity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}