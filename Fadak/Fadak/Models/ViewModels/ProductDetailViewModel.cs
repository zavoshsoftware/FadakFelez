using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public Products CurrentProduct { get; set; }
        public IEnumerable<ProductImages> ProductImageList { get; set; }
        public IEnumerable<Products> SimilarProducts { get; set; }
    }
}