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
    public class ProductImagesController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: ProductImages
        public ActionResult Index(int id)
        {
            var productImages = db.ProductImages.Include(p => p.Products).Where(p=>p.IsDelete==false&&p.fk_ProductId==id).OrderByDescending(p=>p.SubmitDate);

            return View(productImages.ToList());
        }
         
        // GET: ProductImages/Create
        public ActionResult Create(int id)
        {
            ViewBag.fk_ProductId =id;
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ProductImages productImages,int id,HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/ProductImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    productImages.ImageUrl = newFilenameUrl;
                }
                #endregion
                productImages.fk_ProductId = id;
				productImages.IsDelete=false;
				productImages.SubmitDate= DateTime.Now; 
                db.ProductImages.Add(productImages);
                db.SaveChanges();
                return RedirectToAction("Index",new {id=id });
            }

            ViewBag.fk_ProductId = id;
            return View(productImages);
        }

        // GET: ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = db.ProductImages.Find(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_ProductId =  productImages.fk_ProductId;
            return View(productImages);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImages productImages,HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/ProductImage/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    productImages.ImageUrl = newFilenameUrl;
                }
                #endregion
                productImages.IsDelete=false;
                db.Entry(productImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = productImages.fk_ProductId });
            }
            ViewBag.fk_ProductId = new SelectList(db.Products, "Id", "Title", productImages.fk_ProductId);
            return View(productImages);
        }

        // GET: ProductImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = db.ProductImages.Find(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }
            return View(productImages);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImages productImages = db.ProductImages.Find(id);
			productImages.IsDelete=true;
			productImages.DeleteDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index", new { id = productImages.fk_ProductId });
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
