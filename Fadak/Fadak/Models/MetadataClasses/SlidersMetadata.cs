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
    [DisplayName("تصویر اسلایدر")]
    [DisplayPluralName("تصاویر اسلایدر")]
    public class SlidersMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان اول")]
        public string BigTitle { get; set; }
        [Display(Name = "عنوان اول انگلیسی")]
        public string BigTitle_En { get; set; }
        [Display(Name = "عنوان دوم")]
        public string SmallTitle { get; set; }
        [Display(Name = "عنوان دوم انگلیسی")]
        public string SmallTitle_En { get; set; }
        [Display(Name = "عنوان لینک")]
        public string FirstLinkText { get; set; }
        [Display(Name = "عنوان لینک انگلیسی")]
        public string FirstLinkText_En { get; set; }
        [Display(Name = "آدرس لینک")]
        public string FirsLinkAddress { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}


namespace Fadak.Models
{
    public partial class Sliders
    {
        GetCulture oGetCulture = new GetCulture();
        public string BigTitleStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.BigTitle_En;
                    case "fa-ir":
                        return this.BigTitle;
                    default:
                        return String.Empty;
                }
            }
        }
        public string SmallTitleStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.SmallTitle_En;
                    case "fa-ir":
                        return this.SmallTitle;
                    default:
                        return String.Empty;
                }
            }
        }

        public string FirstLinkTextStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.FirstLinkText_En;
                    case "fa-ir":
                        return this.FirstLinkText;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}