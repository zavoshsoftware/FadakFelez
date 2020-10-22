using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class AboutUsViewModel
    {
        public TextItems AboutText { get; set; }
        public IEnumerable<TextItems> WhyUs { get; set; }
    }
}