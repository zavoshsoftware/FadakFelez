//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fadak.Models
{
    using Fadak.Models.MetadataClasses;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    
    [MetadataType(typeof(GalleryImagesMetadata))]
    public partial class GalleryImages
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Title_En { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
    }
}
