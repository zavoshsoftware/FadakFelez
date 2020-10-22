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
    public class BlogCategoriesController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();

        // GET: BlogCategories
    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.BlogCategories.Where(a=>a.IsDelete==false).OrderByDescending(a=>a.SubmitDate).ToList());
        }

        // GET: BlogCategories/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategories blogCategories = db.BlogCategories.Find(id);
            if (blogCategories == null)
            {
                return HttpNotFound();
            }
            return View(blogCategories);
        }

        // GET: BlogCategories/Create
    [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Title,SubmitDate,IsDelete,DeleteDate,Title_En")] BlogCategories blogCategories)
        {
            if (ModelState.IsValid)
            {
				blogCategories.IsDelete=false;
				blogCategories.SubmitDate= DateTime.Now; 
                db.BlogCategories.Add(blogCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogCategories);
        }

        // GET: BlogCategories/Edit/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategories blogCategories = db.BlogCategories.Find(id);
            if (blogCategories == null)
            {
                return HttpNotFound();
            }
            return View(blogCategories);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Title,SubmitDate,IsDelete,DeleteDate,Title_En")] BlogCategories blogCategories)
        {
            if (ModelState.IsValid)
            {
				blogCategories.IsDelete=false;
                db.Entry(blogCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogCategories);
        }

        // GET: BlogCategories/Delete/5
    [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategories blogCategories = db.BlogCategories.Find(id);
            if (blogCategories == null)
            {
                return HttpNotFound();
            }
            return View(blogCategories);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogCategories blogCategories = db.BlogCategories.Find(id);
			blogCategories.IsDelete=true;
			blogCategories.DeleteDate=DateTime.Now;
 
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
       
        public ActionResult Partial_BlogCatList()
        {
            var blogCat = db.BlogCategories.Where(q => q.IsDelete == false).OrderByDescending(a => a.SubmitDate).ToList();
            return View(blogCat);
        }
    }
}
