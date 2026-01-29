using KYCAPP.Web.CustomHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast;

namespace KYCAPP.Web.Models
{

    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {

        //public void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string buttonName = filterContext.HttpContext.Request.Form["button1"];
        //}
        public void OnAuthorization(AuthorizationContext filterContext)
        {


            string returnURL = filterContext.HttpContext.Request.RawUrl;


            var user = UserManager.User;
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            { "action", "Index" },
                            //{ "controller", "Login" }
                            { "controller", "HomePage" }
                        });
            }
            else
            {
                if (HttpContext.Current.Request.HttpMethod == "POST")
                {

                    string strSessionId = HttpContext.Current.Session.SessionID;
                    string eventName = HttpContext.Current.Request.Headers["X-Button-Name"];
                    string strPageName = HttpContext.Current.Request.Url.AbsolutePath;
                    if (strPageName.Contains("/Download"))
                    {
                        eventName = "Download";
                    }
                    
                    LogHelper.InsertInLogger(strSessionId, strPageName, eventName);

                }
                //string strSessionId = HttpContext.Current.Session.SessionID;
                //LogHelper.InsertInLogger(strSessionId, returnURL);
                if (user.FullAccess)
                {
                    if ((returnURL.ToLower().Contains("/menu") || returnURL.ToLower().Contains("/user") || returnURL.ToLower().Contains("/menucreation") || returnURL.ToLower().Contains("/role") || returnURL.ToLower().Contains("/menurolemap")))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                        {
                            { "action", "Index" },
                            { "controller", "AccessDenied" }
                        });
                    }
                }

            }
        }
    }
}
