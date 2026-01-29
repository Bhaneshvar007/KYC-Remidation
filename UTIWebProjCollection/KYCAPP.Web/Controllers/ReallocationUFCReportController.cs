using KYCAPP.Web.CustomHelper;
using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    [CustomAuthorize]
    public class ReallocationUFCReportController : Controller
    {
        // GET: ReallocationUFCReport
        public ActionResult Index()
        {
            return View();
        }


        ReallocationUFCReport re = new ReallocationUFCReport();
        public ActionResult GetGridDetail(string Search_Text, string p_aum_bracket)
        {
           
            CommonHelper.WriteLog("ReallocationUFCReportController " + "Search_Text :" + Search_Text + " | p_aum_bracket: " + p_aum_bracket);

            return new JsonNetResult(re.GetGridDetailReallocation(Search_Text, p_aum_bracket));
        }

        public ActionResult GetZone_dropdown()
        {
            return new JsonNetResult(re.GetZone_List());
        }

        public ActionResult Get_region_dropdown(string p_zone)
        {
            return new JsonNetResult(re.GetRegion_List(p_zone));
        }

        public ActionResult Get_ufc_dropdown(string p_zone, string p_region)
        {
            return new JsonNetResult(re.GetUFC_List_reg_Zonewise(p_zone, p_region));
        }

        public ActionResult UpdateReallocation(UpdateReallocationUFC_MODEL model, string selected_employee_code, string selected_email_id)
        {
            model.MAIL_DATA[0].CM_Name = UserManager.User.Name;
            model.FromEmailId = UserManager.User.Email;

           // CommonHelper.WriteLog("ReallocationUFCReportController> UpdateReallocation() " + "selected_employee_code :" + selected_employee_code);

            return new JsonNetResult(re.Save(model, selected_employee_code, selected_email_id));
        }

    }
}