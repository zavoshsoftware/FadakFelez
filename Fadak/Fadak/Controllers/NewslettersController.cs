using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fadak.Models;
using System.Text.RegularExpressions;

namespace Fadak.Controllers
{
    public class NewslettersController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: Newsletters
    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Newsletters.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: Newsletters/Details/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletters newsletters = db.Newsletters.Find(id);
            if (newsletters == null)
            {
                return HttpNotFound();
            }
            return View(newsletters);
        }
 
        // GET: Newsletters/Delete/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletters newsletters = db.Newsletters.Find(id);
            if (newsletters == null)
            {
                return HttpNotFound();
            }
            return View(newsletters);
        }

        // POST: Newsletters/Delete/5
        [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newsletters newsletters = db.Newsletters.Find(id);
			newsletters.IsDelete=true;
			newsletters.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult NewsletterSubmit(string email)
        {
          Newsletters  nl = new Newsletters();

            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isEmail)
            {
                try
                {
                    if (!db.Newsletters.Where(a => a.IsDelete == false && a.Email == email).Any())
                    {
                        nl.Email = email;
                        nl.IsDelete = false;
                        nl.SubmitDate = DateTime.Now;
                      //  nl.PageUrl = HttpContext.Request.Url.AbsoluteUri;

                        db.Newsletters.Add(nl);
                        db.SaveChanges();
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json("repeat", JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                    throw;
                }
            }
            else
                return Json("false", JsonRequestBehavior.AllowGet);


        }
    }
}
