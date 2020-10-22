using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Fadak.Models.MetadataClasses
{
    [DisplayName("کاربر")]
    [DisplayPluralName("کاربران")]
    public class UsersMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="نقش کاربر")]
        public int fk_RoleID { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public System.DateTime SubmitDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}