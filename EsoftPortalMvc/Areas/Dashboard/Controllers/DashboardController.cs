using EsoftPortalMvc.Areas.Dashboard.Models;
using EsoftPortalMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Areas.Dashboard.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            List<DashboardViewModel> data = new List<DashboardViewModel>();
            return View(data);
        }
    }
}