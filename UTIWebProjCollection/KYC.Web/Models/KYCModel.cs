using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYC.Web.Models
{
    public class KYCModel
    {
    }
    public class KYC_DataModel
    {
        public int rownum { get; set; }
        public string SCHEME { get; set; }
        public string MF_CODE { get; set; }
        public string SCHEME_NAME { get; set; }
        public string PLAN { get; set; }
        public string ACNO { get; set; }
        public string PLAN_MODE { get; set; }
        public string PAN { get; set; }
        public string JT1PAN { get; set; }
        public string JT2PAN { get; set; }
        public string GPAN { get; set; }
        public string KRA_PAN { get; set; }
        public string KRA_PAN1 { get; set; }
        public string KRA_PAN2 { get; set; }
        public string KRA_GPAN { get; set; }
        public string DNR_PAN { get; set; }
        public string PAN_KYCSTATUS { get; set; }
        public string PAN1_KYCSTATUS { get; set; }
        public string PAN2_KYCSTATUS { get; set; }
        public string GPAN_KYCSTATUS { get; set; }
        public string KRA_PAN_KYCSTATUS { get; set; }
        public string KRA_PAN1_KYCSTATUS { get; set; }
        public string KRA_PAN2_KYCSTATUS { get; set; }
        public string KRA_GPAN_KYCSTATUS { get; set; }
        public string DNR_PAN_KYCSTATUS { get; set; }
        public string INVNAME { get; set; }
        public string JT1 { get; set; }
        public string JT2 { get; set; }
        public string FLAG { get; set; }
        public string DNR_NAME { get; set; }
        public string UFCCODE { get; set; }
        public string UFC_NAME { get; set; }
        public string PIN1 { get; set; }
        public string CITY1 { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string REGIONDESC { get; set; }
        public string ZONEDESC { get; set; }
        public string INVDIVSTATUS { get; set; }
        public string MOHDESC { get; set; }
        public string FOLIO_MIN_BRANCH { get; set; }
        public string FOLIO_MIN_DATE { get; set; }
        public string ADD1 { get; set; }
        public string ADD2 { get; set; }
        public string ADD3 { get; set; }
        public string CITY2 { get; set; }
        public string STATE1 { get; set; }
        public string COUNTRY { get; set; }
        public string PIN2 { get; set; }
        public string EMAILID { get; set; }
        public string MOBILENO { get; set; }
        public string PHONE { get; set; }
        public string RESIPHONE { get; set; }
        public string CONCAT_ADD { get; set; }
        public string SELECTED_REC { get; set; }
        public string SELECTED_EMPID { get; set; }
        public string SELECTED_DT { get; set; }
        public string UPDATED_DT { get; set; }
        public string SELECT_STATUS { get; set; }

    }
    public class UserDetailsModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public bool FullAccess { get; set; }
    }
    public class MenusModel
    {
        public int SeqID { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string css_class { get; set; }
        public int is_active { get; set; }
    }
    public class SummaryRegionZoneWiseModel
    {
        public string ZONE { get; set; }
        public string REGION { get; set; }
        public string BRANCH { get; set; }
        public string TOTAL_CASE_ACCEPTED { get; set; }
        public string PENDING_FOR_ACTION { get; set; }
        public string MOVED_TO_POOL { get; set; }
    }
    public class DataAcceptanceReportStaffModel
    {
        public int rownum { get; set; }
        public string folio { get; set; }
        public string selected_empid { get; set; }
        public string NAME { get; set; }
        public string LOCATIONNAME { get; set; }
        public string date_accept_dt { get; set; }
        public string date_Accept_end_dt { get; set; }

    }
}