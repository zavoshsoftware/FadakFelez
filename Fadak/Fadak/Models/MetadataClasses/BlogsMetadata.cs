using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Fadak.Models.MetadataClasses
{
    [DisplayName("مطلب وبلاگ")]
    [DisplayPluralName("مطالب وبلاگ")]
    public class BlogsMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        public int fk_BlogCategoryId { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید")]
        public string Title { get; set; }
        [Display(Name ="توضیحات")]
        [UIHint("RichText")]
        [AllowHtml]
        public string Detail { get; set; }
        [Display(Name ="تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name ="تعداد بازدید")]
        public int VisitNumber { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string Title_En { get; set; }
        [Display(Name = "توضیحات انگلیسی")]
        [UIHint("RichText")]
        [AllowHtml]
        public string Detail_En { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات کوتاه")]
        public string Summery { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات کوتاه انگلیسی")]
        public string Summery_En { get; set; }
    }
}



namespace Fadak.Models
{
    public partial class Blogs
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

        public string DetailStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.Detail_En;
                    case "fa-ir":
                        return this.Detail;
                    default:
                        return String.Empty;
                }
            }
        }

        public string SummeryStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.Summery_En;
                    case "fa-ir":
                        return this.Summery;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}