using KYCAPP.Web.CustomHelper;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI.WebControls;
using System.Windows;

namespace KYCAPP.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        string sessionId = "";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            sessionId = HttpContext.Current.Session.SessionID;
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{



        //    //if (Request.HttpMethod == "POST")
        //    //{

        //    //    string strSessionId = sessionId;
        //    //    string buttonName = Request.Headers["X-Button-Name"];
        //    //    string strPageName = HttpContext.Current.Request.Url.AbsolutePath;
        //    //    LogHelper.InsertInLogger(strSessionId, strPageName);

        //    //}

        //}


    }

}