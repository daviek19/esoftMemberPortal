using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new EsoftPortalMvc.Controllers.BaseController.BaseActionFilter());
            filters.Add(new EsoftPortalMvc.Controllers.BaseController.ForceUserToLogin());//force user to Login
        }
    }
}
