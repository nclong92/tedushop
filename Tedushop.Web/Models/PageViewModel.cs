using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tedushop.Web.Models
{
    public class PageViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Content { get; set; }
    }
}