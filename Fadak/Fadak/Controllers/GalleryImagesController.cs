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

namespace Fadak.Controllers
{
    public class GalleryImagesController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: GalleryImages
    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.GalleryImages.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: GalleryImages/Details/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // GET: GalleryImages/Create
    [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleryImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Create(GalleryImages galleryImages,HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/GalleryImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);
                    galleryImages.ImageUrl = newFilenameUrl;
                }
                #endregion
                galleryImages.IsDelete=false;
				galleryImages.SubmitDate= DateTime.Now; 
                db.GalleryImages.Add(galleryImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galleryImages);
        }

        // GET: GalleryImages/Edit/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // POST: GalleryImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(GalleryImages galleryImages, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/GalleryImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);
                    galleryImages.ImageUrl = newFilenameUrl;
                }
                #endregion
                galleryImages.IsDelete=false;
                db.Entry(galleryImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryImages);
        }

        // GET: GalleryImages/Delete/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            GalleryImages galleryImages = db.GalleryImages.Find(id);
			galleryImages.IsDelete=true;
			galleryImages.DeleteDate=DateTime.Now;
 
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
        [Route("gallery")]
        public ActionResult List()
        {
            return View(db.GalleryImages.Where(a => a.IsDelete == false).OrderByDescending(a => a.SubmitDate).ToList());
        }
    }
}
