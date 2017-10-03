using EsoftPortalMvc.App_Start;
using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EsoftPortalMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

            ModelBinders.Binders.Add(typeof(DateTime), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateModelBinder());

            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());



            AreaRegistration.RegisterAllAreas();
            //IdentityConfig.RegisterIdentities();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperWebConfiguration.Configure();
            ModelBinders.Binders.Add(typeof(JqueryDataTablesModel), new DataTablesCustomModelBinder());
        }
    }
}
