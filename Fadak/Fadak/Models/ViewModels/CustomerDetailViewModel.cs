using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.ViewModels
{
    public class CustomerDetailViewModel
    {
        public Customers CurrentCustomer { get; set; }
        public IEnumerable<Customers> SimilarCustomers { get; set; }
    }
}