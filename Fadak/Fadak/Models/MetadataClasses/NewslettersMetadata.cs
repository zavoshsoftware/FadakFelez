using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fadak.Models.MetadataClasses
{
    public class NewslettersMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
        [Display(Name ="آدرس صفحه")]
        public string PageUrl { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public System.DateTime SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}