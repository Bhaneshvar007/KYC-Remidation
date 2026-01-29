using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cylsys.Common;
using Newtonsoft.Json;

namespace KYC.Web.Models
{
    public class UserManager
    {

        public static UserDetailsModel User
        {
            get
            {
                return (UserDetailsModel)HttpContext.Current.Session["UserDetails"];
            }
        }
        public static string GetUserID
        {
            get
            {
                return User.Code;
            }
        }

    }
}