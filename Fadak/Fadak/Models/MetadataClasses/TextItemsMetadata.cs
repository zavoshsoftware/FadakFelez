using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fadak.Models.MetadataClasses
{
    public class TextItemsMetadata
    {
        [Key]
        public int TextItemID { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "متن")]
        [UIHint("RichText")]
        [AllowHtml]
        public string TextBody { get; set; }
        [Display(Name = "تصویر")]
        public string TextImageUrl { get; set; }
        [Display(Name = "عنوان انگلیسی")]
        public string Title_En { get; set; }
        [Display(Name = "متن انگلیسی")]
        [UIHint("RichText")]
        [AllowHtml]
        public string TextBody_En { get; set; }
        public Nullable<int> fk_TextTypeID { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

    }
}



namespace Fadak.Models
{
    public partial class TextItems
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

        public string TextBodyStr
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TextBody_En;
                    case "fa-ir":
                        return this.TextBody;
                    default:
                        return String.Empty;
                }
            }
        }
        
    }
}