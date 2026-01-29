using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KYC.Web.Models;


namespace KYC.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {

            Login bll = new Login();
            UserDetailsModel objmodel = bll.GetUserDetail(model.UserID, model.Password);

            if (objmodel != null)
            {
                Session["UserDetails"] = objmodel;
                Session["LoggedInUser"] = objmodel.Name;
                Common2 cm2 = new Common2();
                List<MenusModel> menus = cm2.GetMenus();
                Session["MenuList"] = menus;

                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(Request.Url.AbsoluteUri + returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.errorMessage = "Invalid User & Password.";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index");
        }
    }
}