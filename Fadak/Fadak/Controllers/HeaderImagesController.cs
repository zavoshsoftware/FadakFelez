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
    [Authorize(Roles = "Administrator")]
    public class HeaderImagesController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: HeaderImages
        public ActionResult Index()
        {
            return View(db.HeaderImages.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

       

    //    // GET: HeaderImages/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: HeaderImages/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(HeaderImages headerImages, HttpPostedFileBase fileupload)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            #region Upload and resize image if needed
    //            string newFilenameUrl = string.Empty;

    //            if (fileupload != null)
    //            {
    //                string filename = Path.GetFileName(fileupload.FileName);
    //                string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
    //                                     + Path.GetExtension(filename);

    //                newFilenameUrl = "/Uploads/Slider/" + newFilename;
    //                string physicalFilename = Server.MapPath(newFilenameUrl);

    //                fileupload.SaveAs(physicalFilename);

    //                headerImages.ImageUrl = newFilenameUrl;
    //            }
    //            #endregion
    //            headerImages.IsDelete=false;
				//headerImages.SubmitDate= DateTime.Now; 
    //            db.HeaderImages.Add(headerImages);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(headerImages);
    //    }

        // GET: HeaderImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeaderImages headerImages = db.HeaderImages.Find(id);
            if (headerImages == null)
            {
                return HttpNotFound();
            }
            return View(headerImages);
        }

        // POST: HeaderImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HeaderImages headerImages, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Slider/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    headerImages.ImageUrl = newFilenameUrl;
                }
                #endregion
                headerImages.IsDelete=false;
                db.Entry(headerImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(headerImages);
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
