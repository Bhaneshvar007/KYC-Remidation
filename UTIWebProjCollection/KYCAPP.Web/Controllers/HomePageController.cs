using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using KYCAPP.Web.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace KYCAPP.Web.Controllers
{
 
    public class HomepageController : Controller
    {



         
        public ActionResult Index()
        {
            CommonHelper.WriteLog("Step 1.");
            ClaimsIdentity identity = this.User.Identity as ClaimsIdentity;
            Login bll = new Login();
            string email = string.Empty;

            if (string.IsNullOrEmpty(this.Request.QueryString["code"]))
                CommonHelper.WriteLog("Authorization Code is missing. Redirecting to login.");

            if (identity != null && !string.IsNullOrEmpty(identity.Name))
            {
                CommonHelper.WriteLog("Step 2.");
                CommonHelper.WriteLog("User.Identity.Name: " + identity.Name);

                ValidateMicrosoftSession();

                //email = identity.FindFirst(ClaimTypes.Email)?.Value
                //             ?? identity.FindFirst("preferred_username")?.Value;
                email = identity.Name;

                UserDetailsModel objmodel = bll.GetUserDetail(email, null,true);


                if (objmodel == null)
                {
                    CommonHelper.WriteLog("User not authorized.");
                    return RedirectToAction("ErrorPage", "HomePage");
                }

                // Store all session variables exactly like in Login method
                Session["UserDetails"] = objmodel;
                Session["login_code"] = objmodel.Code;
                Session["LoggedInUser"] = objmodel.Name;
                Session["is_cm_login"] = objmodel.is_cm_login;
                Session["emp_role"] = objmodel.emp_role;
                Session["EMAIL_ID"] = objmodel.Email;

                Session["Session_id"] = HttpContext.Session.SessionID;

               
                return RedirectToAction("Index", "Home");
            }

            if (Session["userClaims"] != null)
            {
                if (bll.GetUserDetail(email, null,true) != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                //return RedirectToAction("ErrorPage", "HomePage");
            }

            CommonHelper.WriteLog("Step 4.");

            if (!Request.IsAuthenticated)
            {
                return new ChallengeResult("OpenIdConnect", "/");
            }

            // authenticated but not authorized
            return RedirectToAction("ErrorPage", "HomePage");
        }
        
        
        
        
        private bool ValidateMicrosoftSession()
        {
            string token = (this.User.Identity as ClaimsIdentity)?.FindFirst("id_token")?.Value;
            if (string.IsNullOrEmpty(token))
            {
                CommonHelper.WriteLog("ID Token is missing. Microsoft session is not valid.");
                return false;
            }

            try
            {
                string expiry = new JwtSecurityTokenHandler().ReadJwtToken(token)
                                .Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

                if (!string.IsNullOrEmpty(expiry) &&
                    DateTime.UtcNow > FromUnixTimeSeconds(long.Parse(expiry)).UtcDateTime)
                {
                    CommonHelper.WriteLog("ID Token is expired.");
                    return false;
                }

                CommonHelper.WriteLog("ID Token is valid.");
                return true;
            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog($"Error validating ID Token: {ex.Message}");
                return false;
            }
        }
        private DateTimeOffset FromUnixTimeSeconds(long unixTimeSeconds)
        {
            return (DateTimeOffset)new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)unixTimeSeconds);
        }

        public class ChallengeResult : HttpUnauthorizedResult
        {
            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public ChallengeResult(string provider, string redirectUri)
            {
                this.LoginProvider = provider;
                this.RedirectUri = redirectUri;
            }


            public override void ExecuteResult(ControllerContext context)
            {
                CommonHelper.WriteLog(string.Format("Executing ChallengeResult: Provider={0}, RedirectUri={1}", (object)this.LoginProvider, (object)this.RedirectUri));
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    RedirectUri = this.RedirectUri
                };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, this.LoginProvider);
            }

            
        }




        public ActionResult ErrorPage()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut(
            CookieAuthenticationDefaults.AuthenticationType
        );


            return View();
        }
    }





}
