using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KYCAPP.Web.Models;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Get_LeaderBoardData()
        {

            KYC_REMEDIATED_LEADERBOARD obj = new KYC_REMEDIATED_LEADERBOARD();

            return new JsonNetResult(obj.GetLeaderBoard());
        }
        public ActionResult Get_LeaderBoardData_all()
        {

            KYC_REMEDIATED_LEADERBOARD obj = new KYC_REMEDIATED_LEADERBOARD();

            return new JsonNetResult(obj.GetLeaderBoard_all());
        }
    }
}
