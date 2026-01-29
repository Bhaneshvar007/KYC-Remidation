using System.Web;
using System.Web.Mvc;
using KYC.Web.Models;

namespace KYC.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
