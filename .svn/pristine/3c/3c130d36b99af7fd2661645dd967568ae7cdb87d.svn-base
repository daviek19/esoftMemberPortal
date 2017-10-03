using System.Web.Mvc;

namespace EstateManagementMvc.Areas.GeneralSettings
{
    public class GeneralSettingsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GeneralSettings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GeneralSettings_default",
                "GeneralSettings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}