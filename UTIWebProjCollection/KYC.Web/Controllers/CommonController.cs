using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KYC.Web.Models;

namespace KYC.Web.Controllers
{
    [CustomAuthorize]
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult GetSecurityTypes()
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetSecurityTypes());
        }
        public ActionResult GetEmailFiles(DateTime from_date, DateTime to_date)
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetEmailfiles(from_date, to_date));
        }
        public ActionResult GetCommentTypes()
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetCommentTypes());
        }
        public ActionResult GetMenus()
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetMenus());
        }
        public ActionResult GetReportAccess()
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetReportAccessByEmployeeCode());
        }
        public ActionResult GetRoles()
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetRoles());
        }
        public ActionResult GetEmpList(string q)
        {
            Common2 bll = new Common2();
            return new JsonNetResult(bll.GetEmpList(q));
        }
        //public ActionResult GetBatchNumbers(string QueryType)
        //{
        //    Common2 bll = new Common2();
        //    return new JsonNetResult(bll.GetBatchNumbers(QueryType));
        //}
    }
}
