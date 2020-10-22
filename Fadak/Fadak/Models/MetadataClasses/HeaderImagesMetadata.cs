using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fadak.Models.MetadataClasses
{
    public class HeaderImagesMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}