using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYCAPP.Web.Models
{
    public class KYCModel
    {
    }
    public class ResponseModel
    {
        public string msg { get; set; }
        public bool is_success { get; set; }
    }
    public class ExportParaModel
    {
        public string format_types { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }
    public class KYC_DataModel
    {
        public string PRE_POST_2008 { get; set; }
        public int SRNO { get; set; }
        public int rownum { get; set; }
        public string SCHEME { get; set; }
        public string MF_CODE { get; set; }
        public string SCHEME_NAME { get; set; }
        public string PLAN { get; set; }
        public string ACNO { get; set; }
        public string FIRST_HOLDER_NAME { get; set; }
        public string FIRST_HOLDER_PAN { get; set; }
        public string SECOND_HOLDER_NAME { get; set; }
        public string SECOND_HOLDER_PAN { get; set; }
        public string THIRD_HOLDER_NAME { get; set; }
        public string THIRD_HOLDER_PAN { get; set; }
        public string DONOR_NAME { get; set; }
        public string DONOR_PAN { get; set; }
        public string GAURDIAN_NAME { get; set; }
        public string GAURDIAN_PAN { get; set; }
        public string ADDRESS_OF_FIRST_HOLDER { get; set; }
        public string AUM_OF_THE_FOLIO { get; set; }
        public string FIRST_HOLDER_KYC_STATUS { get; set; }
        public string SECOND_HOLDER_KYC_STATUS { get; set; }
        public string THIRD_HOLDER_KYC_STATUS { get; set; }
        public string DONOR_KYC_STATUS { get; set; }
        public string INVESTED_SCHEMES { get; set; }
        public string ARNCODE { get; set; }
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
        public bool SELECT_STATUS { get; set; }
        public string AGENTNAME { get; set; }
        public string LOCATION { get; set; }
        public string AUM_BRACKET { get; set; }
        public string BANK_FLAG { get; set; }
        public string KYCFLAG { get; set; }
        public string AADHARSEEDINGFLAG { get; set; }
        public string NOMINEEFLAG { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string NAME { get; set; }
        public string ARN_CODE { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string REMARK_CODE { get; set; }
        public string REMARK_DT { get; set; }
        public DateTime? REMARK_DATE { get; set; }
        public int Total_record_count { get; set; } = 0;
        public string ARN_NAME { get; set; }
        public string ARN_MOBILE { get; set; }
        public string ARN_EMAIL { get; set; }
        public bool is_disable_button { get; set; } = false;
        public string DAYS_OF_SELECTION { get; set; }
        public string FOLIO_STATUS { get; set; }
        public string SELECTION_DATE { get; set; }
        public string SELECTED_BY_UFC_EMP { get; set; }
        public string KYC_STATUS_YES { get; set; }
        public string NOMINEE_STATUS_YES { get; set; }
        public string AADHARSEEDING_STATUS_YES { get; set; }
        public string BANK_STATUS_YES { get; set; }
        public string FOLIO_COUNT { get; set; }

        //For CountWise Record
        public string ACTION_UPDATE { get; set; }
        public string LATEST_ACTION_NOTE { get; set; }
        public string DATE_OF_LATEST_ACTION { get; set; }
        public string REMARK_COMMENT { get; set; }

        public int TOTAL_RECORDS { get; set; }
    }

    public class UserDetailsModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public bool FullAccess { get; set; }
        public bool is_cm_login { get; set; } = false;
        public string emp_role { get; set; }
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
        //new property
        public string UFC_NAME { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string AUM_BRACKET { get; set; }
        public int FOLIO_COUNT { get; set; }
        public string SELECTED { get; set; }
        public string KYC_REMEDIATED { get; set; }
        public string NOMINEE_UPDATED { get; set; }
        public string AADHAR_SEEDING_DONE { get; set; }
        public string BANK_STATUS_VALIDATED { get; set; }

        //ADDED ON 08-08-2023
        public string FOLIO_STATUS { get; set; }
        public string INV_TYPE { get; set; }
        public string PRE_POST_2008 { get; set; }
        public string SELECTED_BY_UFC_EMP { get; set; }
        public string KYC_STATUS_YES { get; set; }
        public string NOMINEE_STATUS_YES { get; set; }
        public string AADHARSEEDING_STATUS_YES { get; set; }
        public string BANK_STATUS_YES { get; set; }

    }
    public class DataAcceptanceReportStaffModel
    {
        public int rownum { get; set; }
        public int SRNO { get; set; }
        public string FOLIO { get; set; }
        public string AUM { get; set; }
        public string SELECTED_EMPID { get; set; }
        public string NAME { get; set; }
        public string LOCATIONNAME { get; set; }
        public string DATE_ACCEPT_DT { get; set; }
        public string DATE_ACCEPT_END_DT { get; set; }

    }
    public class ZoneSummaryReportModel
    {

        public string AUM_BRACKET { get; set; }
        public string ZONE_UTI { get; set; }
        public string REGION_NAME_UTI { get; set; }
        public string UFC_NAME { get; set; }
        public string EMPLOYEEID { get; set; }
        public string NAME { get; set; }
        public string CNT_ZON_FA { get; set; }
        public string CNT_UFC_FA { get; set; }
        public string P_CNT_ZON { get; set; }

        //For Starting Count
        public string CNT_REG { get; set; }
        public string P_CNT_ZONE { get; set; }
        public string AUM_REG { get; set; }
        public string P_AUM_ZONE { get; set; }
        public string P_CNT_REG { get; set; }
        public string AUM_UFC { get; set; }
        public string P_AUM_REG { get; set; }
        public string CNT_UFC { get; set; }

        //For KYC Selection Status
        public string CNT_ZON_SEL { get; set; }
        public string P_CNT_ZONE_SEL { get; set; }
        public string AUM_ZON_SEL { get; set; }
        public string CNT_UFC_SEL { get; set; }
        public string P_AUM_ZONE_SEL { get; set; }
        public string P_CNT_REG_SEL { get; set; }
        public string AUM_UFC_SEL { get; set; }
        public string P_AUM_REG_SEL { get; set; }


        //For Remediation Completed
        public string CNT_ZON_REM { get; set; }
        public string P_CNT_ZONE_REM { get; set; }
        public string AUM_ZON_REM { get; set; }
        public string P_AUM_ZONE_REM { get; set; }
        public string CNT_UFC_REM { get; set; }
        public string P_CNT_REG_REM { get; set; }
        public string AUM_UFC_REM { get; set; }
        public string P_AUM_REG_REM { get; set; }

        //For KYC Completed 
        public string CNT_REG_KYC { get; set; }
        public string P_CNT_ZONE_KYC { get; set; }
        public string AUM_REG_KYC { get; set; }
        public string P_AUM_ZONE_KYC { get; set; }
        public string CNT_UFC_KYC { get; set; }
        public string P_CNT_REG_KYC { get; set; }
        public string AUM_UFC_KYC { get; set; }
        public string P_AUM_REG_KYC { get; set; }

        // for valid bank details
        public string CNT_REG_BANK { get; set; }
        public string P_CNT_ZONE_BANK { get; set; }
        public string AUM_REG_BANK { get; set; }
        public string P_AUM_ZONE_BANK { get; set; }
        public string CNT_UFC_BANK { get; set; }
        public string P_CNT_REG_BANK { get; set; }
        public string AUM_UFC_BANK { get; set; }
        public string P_AUM_REG_BANK { get; set; }


        // For Nominee details
        public string CNT_REG_NOM { get; set; }
        public string P_CNT_ZONE_NOM { get; set; }
        public string AUM_REG_NOM { get; set; }
        public string P_AUM_ZONE_NOM { get; set; }
        public string CNT_UFC_NOM { get; set; }
        public string P_CNT_REG_NOM { get; set; }
        public string AUM_UFC_NOM { get; set; }
        public string P_AUM_REG_NOM { get; set; }



        // For Adhar Sedded
        public string CNT_REG_ASEED { get; set; }
        public string P_CNT_ZONE_ASEED { get; set; }
        public string AUM_REG_ASEED { get; set; }
        public string P_AUM_ZONE_ASEED { get; set; }
        public string CNT_UFC_ASEED { get; set; }
        public string P_CNT_REG_ASEED { get; set; }
        public string AUM_UFC_ASEED { get; set; }
        public string P_AUM_REG_ASEED { get; set; }


        ////For Count Wise Records
        //public string ACTION_UPDATE { get; set; }
        //public string AUM_BRACKET { get; set; }
        //public string KYCFLAG { get; set; }
        //public string NOMINEEFLAG { get; set; }
        //public string AADHARSEEDINGFLAG { get; set; }
        //public string BANK_FLAG { get; set; }
        //public string SELECTED_EMPID { get; set; }
        //public string EMPLOYEENAME { get; set; }
        //public string SELECTION_DATE { get; set; }
        //public string REGIONDESC { get; set; }
        //public string ZONEDESC { get; set; }
        //public string FOLIO_STATUS { get; set; }
        //public string DAYS_OF_SELECTION { get; set; }
        //public string ACNO { get; set; }
        //public string FIRST_HOLDER_NAME { get; set; }
        //public string ADDRESS_OF_FIRST_HOLDER { get; set; }
        //public string ARN_CODE { get; set; }
        //public string ARN_NAME { get; set; }
        //public string ARN_MOBILE { get; set; }
        //public string ARN_EMAIL { get; set; }



    }

    public class KYC_REMEDIATED_LEADERBOARDModel
    {
        public int RANK { get; set; }
        public string EMPLOYEEID { get; set; }
        public string NAME { get; set; }
        public string LOCATIONCODE { get; set; }
        public string UFC_LOC { get; set; }
        public string REGION_NAME { get; set; }
        public string ZONE { get; set; }
        public string REMEDIATED_CNT { get; set; }

    }

    public class ReasonWiseReportModel
    {
        public string ZONE { get; set; }
        public string REGION { get; set; }
        public string UFC_NAME { get; set; }
        public string EMPLOYEEID { get; set; }
        public string EMPNAME { get; set; }
        public string REMARK_CODE { get; set; }
        public string REMARK_DESC { get; set; }
        public string FOLIO_COUNT { get; set; }
        public string REMARK_COUNT { get; set; }

        public int REASON1 { get; set; }
        public int REASON2 { get; set; }
        public int REASON3 { get; set; }
        public int REASON4 { get; set; }
        public int REASON5 { get; set; }
        public int REASON6 { get; set; }
        public int REASON7 { get; set; }
        public int REASON8 { get; set; }
        public int REASON9 { get; set; }
        public int REASON10 { get; set; }
        public int REASON11 { get; set; }
        public int REASON12 { get; set; }
    }
    public class DDLModel
    {
        public int ID { get; set; }
        public string EMAIL_ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SLNO { get; set; }
        public string ACNO { get; set; }
        public string Description { get; set; }
        public string INVNAME { get; set; }
        public string AUM_BRACKET { get; set; }
        public string ZONE_UTI { get; set; }
        public string REGION_NAME_UTI { get; set; }
        public string UFC_NAME { get; set; }
    }

    public class UpdateReallocationUFC_MODEL
    {
        public List<MAIL_DATA> MAIL_DATA { get; set; }
        public List<string> SRNO { get; set; }
        public List<string> ACNO { get; set; }
        public List<string> INVNAME { get; set; }
        public string ZONE_UTI { get; set; }
        public string REGION_NAME_UTI { get; set; }
        public string UFC_NAME { get; set; }
        public string JURISDICTION { get; set; }
        public string UFC_NAME_DNQ { get; set; }
        public string EMAIL_ID { get; set; }
        public string FromEmailId { get; set; }

    }
    public class MAIL_DATA
    {
        public string SRNO { get; set; }
        public string ACNO { get; set; }
        public string INVNAME { get; set; }
        public string CM_Name { get; set; }
    }

    public class KYC_ABRIDGED_SEARCH
    {

        public string NOMINEEFLAG_SEARCH { get; set; }
        public string KYCFLAG_SEARCH { get; set; }
        public List<string> AADHARSEEDINGFLAG_SEARCH { get; set; }
        public List<string> BANK_FLAG_SEARCH { get; set; }
        public List<string> FOLIO_STATUS { get; set; }
        public List<string> AUM_BRACKET_SEARCH { get; set; }
        public string UFC_NAME_SEARCH { get; set; }
        public string EMP_NAME_SEARCH { get; set; }
        public string P_SEARCH_TEXT { get; set; }
        public string FOLIONO_SEARCH { get; set; }
        public string PRE_POST_2008 { get; set; }
    }
    public class KYC_DETAILS_SEARCH
    {

        public string NOMINEEFLAG_SEARCH { get; set; }
        public string KYCFLAG_SEARCH { get; set; }
        public List<string> AADHARSEEDINGFLAG_SEARCH { get; set; }
        public List<string> BANK_FLAG_SEARCH { get; set; }
        public List<string> FOLIO_STATUS { get; set; }
        public List<string> AUM_BRACKET_SEARCH { get; set; }
        public string UFC_NAME_SEARCH { get; set; }
        public string EMP_NAME_SEARCH { get; set; }
        public string P_SEARCH_TEXT { get; set; }
        public string FOLIONO_SEARCH { get; set; }
        public string PRE_POST_2008 { get; set; }
    }
    public class GoogleMapModel
    {
        public int SRNO { get; set; }
        public string UFC_NAME { get; set; }
        public string ZONEDESC { get; set; }
        public string REGIONDESC { get; set; }
        public string SELECTED_REC { get; set; }
        public string ACNO { get; set; }
        public string INVNAME { get; set; }
        public string MOBILE { get; set; }
        public string CONCAT_ADD { get; set; }
    }
    public class GoogleMapSearchActivities
    {
        public string P_SEARCH_TEXT { get; set; }
        public string EMP_NAME_SEARCH { get; set; }
        public string FOLIONO_SEARCH { get; set; }
        public string UFC_NAME_SEARCH { get; set; }
    }


    //Contest Model
    public class ContestLeadershipModel
    {
        public int RANK { get; set; }
       // public int EMPLOYEEID { get; set; }

        public string EMPLOYEEID { get; set; }
        public string NAME { get; set; }
        public string LOCATIONCODE { get; set; }
        public string UFC_LOC { get; set; }
        public string REGION_NAME { get; set; }
        public string ZONE { get; set; }
        public string KYC_Complied_Count { get; set; }
        public string REMEDIATED_CNT { get; set; }
        public string CONTEST_QUALIFY_STATUS { get; set; }
    }
}