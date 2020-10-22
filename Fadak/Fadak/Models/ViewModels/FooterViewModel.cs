using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class FooterViewModel
    {
        public string AboutText { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string newsletter { get; set; }
        public IEnumerable<Blogs> RecentBlog { get; set; }
    }
}