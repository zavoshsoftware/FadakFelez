using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fadak.Models;
using System.IO;
using Fadak.Models.ViewModels;

namespace Fadak.Controllers
{
    public class CertificatesController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: Certificates
    [Authorize(Roles = "Administrator")]
        public ActionResult Index(int id)
        {
            ViewBag.title = db.Types.Where(a => a.Id == id).Select(a => a.Title).FirstOrDefault();
            return View(db.Certificates.Where(a => a.IsDelete == false && a.fk_TypeId == id).OrderByDescending(a => a.SubmitDate).ToList());
        }



        // GET: Certificates/Create
    [Authorize(Roles = "Administrator")]
        public ActionResult Create(int id)
        {
            ViewBag.fk_TypeId = id;

            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Create(Certificates certificates, int id, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;

                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Certificate/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    certificates.ImageUrl = newFilenameUrl;
                }
                #endregion
                certificates.fk_TypeId = id;
                certificates.IsDelete = false;
                certificates.SubmitDate = DateTime.Now;
                db.Certificates.Add(certificates);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }
            ViewBag.fk_TypeId = id;
            return View(certificates);
        }

        // GET: Certificates/Edit/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = db.Certificates.Find(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_TypeId = certificates.fk_TypeId;
            return View(certificates);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Certificates certificates, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;

                if (fileupload != null)
                {
                    string filename = Path.GetFileName(fileupload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Certificate/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    certificates.ImageUrl = newFilenameUrl;
                }
                #endregion
                certificates.IsDelete = false;
                db.Entry(certificates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = certificates.fk_TypeId });
            }
            ViewBag.fk_TypeId = certificates.fk_TypeId;
            return View(certificates);
        }

        // GET: Certificates/Delete/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = db.Certificates.Find(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            return View(certificates);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Certificates certificates = db.Certificates.Find(id);
            certificates.IsDelete = true;
            certificates.DeleteDate = DateTime.Now;

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

        [Route("cer/{id:int?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificates certificates = db.Certificates.Find(id);
            if (certificates == null)
            {
                return HttpNotFound();
            }
            CertificateDetailViewModel cdVM = new CertificateDetailViewModel();
            cdVM.CurrentCertificate = certificates;
            cdVM.SimilarCertificates = db.Certificates.Where(a => a.IsDelete == false && a.fk_TypeId == certificates.fk_TypeId && a.Id != id).OrderByDescending(a => a.SubmitDate).ToList();

            return View(cdVM);
        }
    }
}
