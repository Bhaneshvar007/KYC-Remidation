using System.Web;
using System.Web.Mvc;
using KYCAPP.Web.Models;

namespace KYCAPP.Web
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
