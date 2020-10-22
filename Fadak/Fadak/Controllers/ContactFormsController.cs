using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fadak.Models;

namespace Fadak.Controllers
{
    public class ContactFormsController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: ContactForms
    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.ContactForm.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }
         
        // GET: ContactForms/Delete/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = db.ContactForm.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactForm contactForm = db.ContactForm.Find(id);
			contactForm.IsDelete=true;
			contactForm.DeleteDate=DateTime.Now;
 
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

        public ActionResult SubmitContactForm(string Name,string Email,string Phone,string Message)
        {
            ContactForm cf = new ContactForm();
            cf.Email = Email;
            cf.IsDelete = false;
            cf.Message = Message;
            cf.Name = Name;
            cf.Phone = Phone;
            cf.SubmitDate = DateTime.Now;

            db.ContactForm.Add(cf);
            db.SaveChanges();

            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}
