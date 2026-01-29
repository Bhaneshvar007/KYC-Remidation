using KYCAPP.Web.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Claims;
using System.Linq;
using System.Net;
using System.Web;



[assembly: OwinStartup(typeof(KYCAPP.Web.Startup))]

namespace KYCAPP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string tenantId = ConfigurationManager.AppSettings["TenantId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];
                
            // Log startup
            CommonHelper.WriteLog("ConfigureAuth started.");

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/HomePage")
            });

            object value = app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientId,
                Authority = $"https://login.microsoftonline.com/{tenantId}",
                RedirectUri = redirectUrl,
                ClientSecret = clientSecret,
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                Scope = "openid profile email",
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        var identity = n.AuthenticationTicket.Identity;
                        var idToken = n.ProtocolMessage.IdToken;

                        if (!string.IsNullOrEmpty(idToken))
                        {
                            identity.AddClaim(new System.Security.Claims.Claim("id_token", idToken));
                        }

                        string email = identity.FindFirst(ClaimTypes.Email)?.Value ??
                                       identity.FindFirst("preferred_username")?.Value;

                        string name = identity.FindFirst(ClaimTypes.Name)?.Value;

                        // Save to session
                        HttpContext.Current.Session["EMAIL_ID"] = email;
                        HttpContext.Current.Session["LoggedInUser"] = name;

                        // Log success
                        CommonHelper.WriteLog($"SSO login successful. Name: {name}, Email: {email}");

                        return System.Threading.Tasks.Task.FromResult(0);
                    },

                    AuthenticationFailed = n =>
                    {
                        n.HandleResponse();

                        // Log failure
                        CommonHelper.WriteLog($"SSO login failed: {n.Exception?.Message}");

                        // Optional: redirect to error page
                        // n.Response.Redirect("/Error/Index");

                        return System.Threading.Tasks.Task.FromResult(0);
                    }
                }
            });

            // Log end
            CommonHelper.WriteLog("ConfigureAuth finished.");
        }

    }
}
