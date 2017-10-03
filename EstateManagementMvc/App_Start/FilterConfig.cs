using System.Web;
using System.Web.Mvc;

namespace EstateManagementMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new EstateManagementMvc.Controllers.BaseController.BaseActionFilter());
            filters.Add(new EstateManagementMvc.Controllers.BaseController.ForceUserToLogin());//force user to Login
        }
    }
}
