using GSD.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fadak
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
   



        protected void Application_BeginRequest()
        {
            ExecuteCore();
        }
        private const string LanguageCookieName = "MyLanguageCookieName";
        protected void ExecuteCore()
        {
            var cookie = Request.Cookies[LanguageCookieName];
            string lang;
            if (cookie != null)
            {
                lang = "fa-IR";
            }
            else
            {
                lang = "fa-IR";
                var httpCookie = new HttpCookie(LanguageCookieName, lang) { Expires = DateTime.Now.AddYears(1) };
                Response.SetCookie(httpCookie);
            }
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

            //var persianCulture = new PersianCulture();
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        protected string StyleSheetPathFa
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/rightStyle.css";
            }
        }
        protected string StyleSheetPath
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/leftStyle.css";
            }
        }

        private string ApplicationPath
        {
            get
            {
                string result = Request.ApplicationPath;
                if (!result.EndsWith("/"))
                {
                    result += "/";
                }
                return result;
            }
        }
    }
}
