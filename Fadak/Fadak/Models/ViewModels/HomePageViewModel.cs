using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Products> ProductInMenu { get; set; }
        public TextItems AboutText { get; set; }
        public TextItems AboutTextView { get; set; }
        public IEnumerable<TextItems> Testimonial { get; set; }
        public IEnumerable<Products> ProductList { get; set; }
        public IEnumerable<Customers> CustomerList { get; set; }
        public IEnumerable<Certificates> CertificateList { get; set; }
        public IEnumerable<Certificates> Equipment { get; set; }
        public IEnumerable<Certificates> Acknowledgments { get; set; }
        public IEnumerable<TextItems> StatisticText { get; set; }
        public IEnumerable<Blogs> RecentBlog { get; set; }
        public List<Sliders> slider { get; set; }
    }
}