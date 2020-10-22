using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Fadak.Models;
using Fadak.Models.ViewModels;
using System.Web.Security;

namespace Fadak.Controllers
{
    public class AccountController : Controller
    {
        FadakDBEntities db = new FadakDBEntities();

        [Route("Login")]
        public ActionResult Login(string ReturnUrl = "")
        {
            ViewBag.Message = "";
            ViewBag.ReturnUrl = ReturnUrl;
            //if (User.Identity.IsAuthenticated)
            //{
            //    int useid = Convert.ToInt32(User.Identity.Name);
            //    var n = db.Users.Where(a => a.Id == useid).FirstOrDefault();
            //    var m = (db.Roles.Where(a => a.Id == n.fk_RoleID)).FirstOrDefault().Name;
            //}
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public ActionResult Login(LoginModel login, string ReturnUrl)
        {
            //bool loggedIn = User.Identity.IsAuthenticated;
            //string username = User.Identity.Name;

            //// How to MD5 Hash password:
            //string source = "mkamkadmvvkd";
            //string hashed = FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");

            //string ReturnUrl = Request.QueryString["ReturnUrl"];
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Email == login.Username && u.Password == login.Password))
                {
                    //     var n = db.Users.Include(a => a.RoleID).Where(a => a.Email == login.Username).FirstOrDefault();
                    var n = (from a in db.Users
                             where a.Email == login.Username
                             select a).FirstOrDefault();
                    if (n != null)
                    {
                        //FormsAuthentication.SetAuthCookie(storetitle, login.RememberMe);
                        FormsAuthentication.RedirectFromLoginPage(n.Id.ToString(), false);
                        if (!string.IsNullOrEmpty(ReturnUrl))
                        {
                            //LoginLogs loginlogs = new LoginLogs();
                            //loginlogs.fk_UserID = n.UserID;
                            //loginlogs.InputPass = n.Password;
                            //loginlogs.InputUsername = n.Username;
                            //loginlogs.IsSuccess = true;
                            //loginlogs.LogDate = DateTime.Now;

                            //db.LoginLogs.Add(loginlogs);
                            //db.SaveChanges();
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            //LoginLogs loginlogs = new LoginLogs();
                            //loginlogs.fk_UserID = n.UserID;
                            //loginlogs.InputPass = n.Password;
                            //loginlogs.InputUsername = n.Username;
                            //loginlogs.IsSuccess = true;
                            //loginlogs.LogDate = DateTime.Now;

                            //db.LoginLogs.Add(loginlogs);
                            //db.SaveChanges();
                            var m = db.Roles.Where(a => a.Id == n.fk_RoleID).FirstOrDefault();
                            if (m.Name == "Administrator")
                            {
                                return Redirect(("/products"));
                            }
                            return Redirect(FormsAuthentication.DefaultUrl);
                        }


                    }
                }
            }
            ModelState.AddModelError("", "نام کاربر یا کلمه عبور صحیح نیست");
            //LoginLogs Falseloginlogs = new LoginLogs();

            //Falseloginlogs.InputPass = login.Password;
            //Falseloginlogs.InputUsername = login.Username;
            //Falseloginlogs.IsSuccess = false;
            //Falseloginlogs.LogDate = DateTime.Now;

            //db.LoginLogs.Add(Falseloginlogs);
            //db.SaveChanges();

            return View(login);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        //[Authorize(Roles = "staff")]
        //public ActionResult ChangePass(string PrevPassword, string NewPassword)
        //{
        //    try
        //    {
        //        int UserID = Convert.ToInt32(User.Identity.Name);

        //        var n = (db.Users.Where(a => a.UserID == UserID)).FirstOrDefault();

        //        if (n.Password == PrevPassword)
        //        {
        //            n.LastPassword = PrevPassword;
        //            n.Password = NewPassword;
        //            db.SaveChanges();

        //            return Json("<p class='alert alert-success'>کلمه عبور شما با موفقیت تغییر یافت</ p >", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json("<p class='alert alert-danger'>کلمه عبور پیشین شما صحیح نمی باشد. دوباره اطلاعات را وراد نمایید</ p >", JsonRequestBehavior.AllowGet);

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return Json("<p class='alert alert-danger'>هنگام پردازش درخواست شما مشکلی رخ داده است، لطفا صفحه را بسته و مجدداً تلاش نمایید</p>", JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}