using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Fadak.Models.MetadataClasses
{
    [DisplayName("پیغام")]
    [DisplayPluralName("پیغام ها")]
    public class ContactFormMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Name { get; set; }
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
        [Display(Name ="تلفن")]
        public string Phone { get; set; }
        [Display(Name ="پیغام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Message { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}