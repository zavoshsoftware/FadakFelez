using Classes;
using Fadak.Models;
using Fadak.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fadak.Controllers
{
    public class HomeController : Controller
    {
        private FadakDBEntities db = new FadakDBEntities();
  
        public ActionResult Index()
        {
            HomePageViewModel hpVM = new HomePageViewModel();

            List<Products> products = db.Products.Where(a => a.IsDelete == false).ToList();

            hpVM.ProductInMenu = products.OrderByDescending(q=>q.SubmitDate).ToList();
            hpVM.AboutText = db.TextItems.Where(a => a.TextItemID == 1).FirstOrDefault();
            hpVM.AboutTextView = db.TextItems.Where(a => a.TextItemID == 2).FirstOrDefault();
            hpVM.Testimonial = db.TextItems.Where(a => a.fk_TextTypeID == 6&&a.IsDelete==false).ToList();
            hpVM.ProductList = products;
            hpVM.CustomerList = db.Customers.Where(a => a.IsDelete == false).ToList();
            hpVM.CertificateList = GetCertificateByType(1);
            hpVM.Equipment = GetCertificateByType(2);
            hpVM.Acknowledgments = GetCertificateByType(3);
            hpVM.StatisticText = db.TextItems.Where(a => a.fk_TextTypeID == 2).ToList();
            hpVM.RecentBlog=db.Blogs.Where(q=>q.IsDelete==false).OrderByDescending(q => q.SubmitDate).Take(3).ToList();
            hpVM.slider=db.Sliders.Where(q => q.IsDelete == false).OrderBy(q => q.SubmitDate).ToList();
            return View(hpVM);
        }

        public List<Certificates> GetCertificateByType(int typeId)
        {
            List<Certificates> certificates =
                db.Certificates.Where(a => a.IsDelete == false && a.fk_TypeId == typeId).ToList();

            return certificates;
        }

        [Route("About")]
        public ActionResult About()
        {
            AboutUsViewModel auVM = new AboutUsViewModel();
            auVM.AboutText = (db.TextItems.Where(a => a.fk_TextTypeID == 3)).FirstOrDefault();
            auVM.WhyUs = (db.TextItems.Where(a => a.fk_TextTypeID == 4)).ToList();

            return View(auVM);
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ContactUsViewModel cuVM = new ContactUsViewModel();
            var text= (db.TextItems.Where(a => a.fk_TextTypeID == 5)).ToList();
            cuVM.Address = ReturnValue(14);
            cuVM.Phone = ReturnValue(15);
            cuVM.Email = ReturnValue(16);
            cuVM.Hours = ReturnValue(17);
            cuVM.HourImage= db.TextItems.Where(a => a.TextItemID == 17).Select(a => a.TextImageUrl).FirstOrDefault();

            return View(cuVM);
        }
        public string ReturnValue(int id)
        {
            GetCulture oGetCulture = new GetCulture();
            string currentCulture = oGetCulture.CurrentLang();

            if (currentCulture == "en-us")
            {
                return db.TextItems.Where(a => a.TextItemID == id).Select(a => a.TextBody_En).FirstOrDefault();
            }
            else
            {
                return db.TextItems.Where(a => a.TextItemID == id).Select(a => a.TextBody).FirstOrDefault();
            }
        }
        public ActionResult Partial_Footer()
        {
            FooterViewModel fVM = new FooterViewModel();

            GetCulture oGetCulture = new GetCulture();
            string currentCulture = oGetCulture.CurrentLang();

            if (currentCulture == "en-us")
            {
                fVM.AboutText = db.TextItems.Where(a => a.TextItemID == 7).Select(a => a.TextBody_En).FirstOrDefault();
                fVM.address = db.TextItems.Where(a => a.TextItemID == 14).Select(a => a.TextBody_En).FirstOrDefault();
                fVM.phone = db.TextItems.Where(a => a.TextItemID == 15).Select(a => a.TextBody_En).FirstOrDefault();
                fVM.email = db.TextItems.Where(a => a.TextItemID == 16).Select(a => a.TextBody_En).FirstOrDefault();
                fVM.newsletter = db.TextItems.Where(a => a.TextItemID == 8).Select(a => a.TextBody_En).FirstOrDefault();
            }
            else
            {
                fVM.AboutText = db.TextItems.Where(a => a.TextItemID == 7).Select(a => a.TextBody).FirstOrDefault();
                fVM.address = db.TextItems.Where(a => a.TextItemID == 14).Select(a => a.TextBody).FirstOrDefault();
                fVM.phone = db.TextItems.Where(a => a.TextItemID == 15).Select(a => a.TextBody).FirstOrDefault();
                fVM.email = db.TextItems.Where(a => a.TextItemID == 16).Select(a => a.TextBody).FirstOrDefault();
                fVM.newsletter = db.TextItems.Where(a => a.TextItemID == 8).Select(a => a.TextBody).FirstOrDefault();
            }


           
            fVM.RecentBlog = db.Blogs.Where(a => a.IsDelete == false).OrderByDescending(a=>a.SubmitDate).Take(3).ToList();
            return View(fVM);
        }
        public ActionResult Partial_HeaderPhone()
        {
            FooterViewModel fVM = new FooterViewModel();

            GetCulture oGetCulture = new GetCulture();
            string currentCulture = oGetCulture.CurrentLang();

            if (currentCulture == "en-us")
            {
                fVM.address = db.TextItems.Where(a => a.TextItemID == 14).Select(a => a.TextBody_En).FirstOrDefault();
                fVM.phone = db.TextItems.Where(a => a.TextItemID == 15).Select(a => a.TextBody_En).FirstOrDefault();
            }
            else
            {
                fVM.address = db.TextItems.Where(a => a.TextItemID == 14).Select(a => a.TextBody).FirstOrDefault();
                fVM.phone = db.TextItems.Where(a => a.TextItemID == 15).Select(a => a.TextBody).FirstOrDefault();
            }

            return View(fVM);
        }


    }
}