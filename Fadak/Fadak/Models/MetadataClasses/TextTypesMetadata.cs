using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fadak.Models.MetadataClasses
{
    public class TextTypesMetadata
    {
        public int TextTypeID { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}