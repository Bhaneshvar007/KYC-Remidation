using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KYCAPP.Web.Models;

namespace KYCAPP.Web.Controllers
{
    public class ContestLeadershipController : Controller
    {
        // GET: ContestLeadership
        public ActionResult ContestLeadership()
        {
            return View();
        }


        public ActionResult Get_LeadershipBoard()
        {

            ContestLeadership contest = new ContestLeadership();

            //return new JsonNetResult(contest.Get_ContestLeadershipBoardRpt());


            var data = contest.Get_ContestLeadershipBoardRpt();

            //CommonHelper.WriteLog($"Data count: {data.Count()}"); 

            return new JsonNetResult(data);
            // var data = contest.Get_ContestLeadershipBoardRpt();
            // return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}