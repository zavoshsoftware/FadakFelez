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
    public class BlogsController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: Blogs
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogCategories).Where(b => b.IsDelete == false).OrderByDescending(b => b.SubmitDate);
            return View(blogs.ToList());
        }


        // GET: Blogs/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.fk_BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Title");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Blogs blogs, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    blogs.ImageUrl = newFilenameUrl;
                }
                #endregion

                blogs.VisitNumber = 0;
                blogs.IsDelete = false;
                blogs.SubmitDate = DateTime.Now;
                db.Blogs.Add(blogs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Title", blogs.fk_BlogCategoryId);
            return View(blogs);
        }

        // GET: Blogs/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Title", blogs.fk_BlogCategoryId);
            return View(blogs);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Blogs blogs, HttpPostedFileBase fileupload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileupload.SaveAs(physicalFilename);

                    blogs.ImageUrl = newFilenameUrl;
                }
                #endregion
                blogs.IsDelete = false;
                db.Entry(blogs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Title", blogs.fk_BlogCategoryId);
            return View(blogs);
        }

        // GET: Blogs/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blogs blogs = db.Blogs.Find(id);
            blogs.IsDelete = true;
            blogs.DeleteDate = DateTime.Now;

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
        [Route("blog/{id:int?}")]
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                var blogs = db.Blogs.Include(b => b.BlogCategories).Where(b => b.IsDelete == false).OrderByDescending(b => b.SubmitDate);
                return View(blogs.ToList());
            }
            else
            {
                var blogs = db.Blogs.Include(b => b.BlogCategories).Where(b => b.IsDelete == false && b.fk_BlogCategoryId == id).OrderByDescending(b => b.SubmitDate);
                return View(blogs.ToList());
            }
        }
        [Route("blog/{BlogCatid:int}/{id:int?}")]
        public ActionResult Details(int BlogCatid, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            blogs.VisitNumber++;
            db.SaveChanges();
            return View(blogs);
        }
        public ActionResult Partial_LatestBlog()
        {
            var blogs = db.Blogs.Where(q => q.IsDelete == false).OrderByDescending(a => a.SubmitDate).Take(3).ToList();
            return View(blogs);
        }
    }
}
