using KYCAPP.Web.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KYCAPP.Web.Controllers
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

            if (Session["sessionCaptcha"].ToString() == model.CaptchaInput)
            {
                UserDetailsModel objmodel = bll.GetUserDetail(model.UserID, model.Password , false);

                //if (objmodel != null && model.Password == "JMXw@C6xPv")//uat  model.password
                if (objmodel != null)
                {       
                    Session["UserDetails"] = objmodel;
                    Session["login_code"] = objmodel.Code;
                    Session["LoggedInUser"] = objmodel.Name;
                    Session["is_cm_login"] = objmodel.is_cm_login;
                    Session["emp_role"] = objmodel.emp_role;
                    Session["EMAIL_ID"] = objmodel.Email;
                    //Common2 cm2 = new Common2();
                    //List<MenusModel> menus = cm2.GetMenus();
                    //Session["MenuList"] = menus;


                    Session["Session_id"] = HttpContext.Session.SessionID;


                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(Request.Url.AbsoluteUri + returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.errorMessage = "Invalid Credentials.";
                }
            }
            else
            {
                ViewBag.errorMessage = "Invalid captcha";
                if (model.CaptchaInput == null || model.CaptchaInput == "")
                {
                    ViewBag.errorMessage = "Please enter captcha";
                }

            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

             
            Request.GetOwinContext().Authentication.SignOut(
                CookieAuthenticationDefaults.AuthenticationType 
            );
            return View();
            //return RedirectToAction("Index", "HomePage");
        }

        public ActionResult GenerateCaptcha()
        {
            Random random = new Random();
            Bitmap bitmap = new Bitmap(150, 90);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.DrawLine(Pens.Black, random.Next(0, 50), random.Next(10, 30), random.Next(0, 200), random.Next(0, 50));
            graphics.DrawRectangle(Pens.Blue, random.Next(0, 20), random.Next(0, 20), random.Next(50, 80), random.Next(0, 20));
            graphics.DrawLine(Pens.Blue, random.Next(0, 20), random.Next(10, 50), random.Next(100, 200), random.Next(0, 80));
            Brush disignBrush = default(Brush);
            //captcha background style  
            HatchStyle[] bkgStyle = new HatchStyle[]
            {
            HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
            HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
            HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
            };
            //create captcha rectangular area for ui 
            RectangleF rectagleArea = new RectangleF(0, 0, 250, 250);
            disignBrush = new HatchBrush(bkgStyle[random.Next(bkgStyle.Length - 3)], Color.FromArgb((random.Next(100, 255)), (random.Next(100, 255)), (random.Next(100, 255))), Color.White);
            graphics.FillRectangle(disignBrush, rectagleArea);
            //generate captcha code with random code
            string captchaCode = string.Format("{0:X}", random.Next(1000000, 9999999));
            //add catcha code into session for use
            Session["sessionCaptcha"] = captchaCode;
            Font objFont = new Font("Times New Roman", 25, FontStyle.Bold);
            //create image for captcha
            graphics.DrawString(captchaCode, objFont, Brushes.Black, 20, 20);
            //Save the image 
            bitmap.Save(Response.OutputStream, ImageFormat.Gif);
            byte[] byteArray;
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byteArray = stream.ToArray();
            }

            return File(byteArray, "image/png");

        }
    }
}