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
    public class TextItemsController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: TextItems
    [Authorize(Roles = "Administrator")]
        public ActionResult Index(int id)
        {
            var textItems = db.TextItems.Include(t => t.TextTypes).Where(t => t.IsDelete == false && t.fk_TextTypeID == id).OrderByDescending(t => t.SubmitDate);
            return View(textItems.ToList());
        }
        // GET: TextItems/Edit/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItems textItems = db.TextItems.Find(id);
            if (textItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk = textItems.fk_TextTypeID;
            return View(textItems);
        }

        // POST: TextItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(TextItems textItems, HttpPostedFileBase fileupload)
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
                    newFilenameUrl = "/Uploads/Text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    fileupload.SaveAs(physicalFilename);
                    textItems.TextImageUrl = newFilenameUrl;
                }

                #endregion
                textItems.IsDelete = false;
                db.Entry(textItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @id = textItems.fk_TextTypeID });
            }
            return View(textItems);
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
