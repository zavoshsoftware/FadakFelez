using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Fadak.Models.MetadataClasses
{
    [DisplayName("تصویر")]
    [DisplayPluralName("تصاویر")]
    public class GalleryImagesMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید")]
        public string Title { get; set; }
        [Display(Name ="عنوان انگلیسی")]
        public string Title_En { get; set; }
        [Display(Name ="تصویر")]
        public string ImageUrl { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
    }
}

namespace Fadak.Models
{
    public partial class GalleryImages
    {
        GetCulture oGetCulture = new GetCulture();
        public string TitleStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.Title_En;
                    case "fa-ir":
                        return this.Title;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}