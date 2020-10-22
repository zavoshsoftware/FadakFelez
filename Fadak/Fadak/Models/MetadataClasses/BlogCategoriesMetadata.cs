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
    [DisplayName("گروه مطلب وبلاگ")]
    [DisplayPluralName("گروه های مطالب وبلاگ")]
    public class BlogCategoriesMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Title { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string Title_En { get; set; }
    }
}


namespace Fadak.Models
{
    public partial class BlogCategories
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