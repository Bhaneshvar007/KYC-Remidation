using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KYC.Web.Models
{
    public class QueryMaster
    {
        public static string KYC_REMEDIATION_GRID
        {
            get
            {
                return @"SELECT 
                                KR.ADD2,KR.ADD3,KR.CITY2,KR.STATE1,KR.COUNTRY,PIN2,KR.EMAILID,KR.MOBILE,KR.PHONE,KR.RESIPHONE
                         FROM KYC_REMED_BASE_DAT KR
                         WHERE KR.PIN2=@PIN2";
            }
        }
        public static string KYC_REMEDIATION_UPDATE_CMD
        {
            get
            {
                return @"UPDATE kyc_remed_base_dat SET selected_rec='S',selected_dt=systemdate ,selected_empid=@employee_code where ";
            }
        }
    }
}