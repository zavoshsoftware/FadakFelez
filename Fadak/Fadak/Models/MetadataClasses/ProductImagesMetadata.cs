using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Fadak.Models.MetadataClasses
{
    [DisplayName("تصویر محصول")]
    [DisplayPluralName("تصاویر محصول")]
    public class ProductImagesMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public int fk_ProductId { get; set; }

    }
}