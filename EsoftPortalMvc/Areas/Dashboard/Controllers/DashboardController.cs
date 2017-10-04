using ESoft.Web.Services.Registry;
using EsoftPortalMvc.Areas.Dashboard.Models;
using EsoftPortalMvc.Controllers;
using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Areas.Dashboard.Controllers
{
    public class DashboardController : BaseController
    {
        private CustomerManager customerManager;

        public DashboardController()
        {
            customerManager = new CustomerManager();
        }

        public ActionResult Index()
        {
            string customerNumber = "004103";

            CustomerDetailsView customer = new CustomerDetailsView();

            List<CustomerDetailsView> customerDetailsView = customerManager.GetCustomerDetails(customerNumber);

            if (customerDetailsView.Count > 0)
            {
                customer = customerDetailsView[0];
            }

            return View(customer);
        }
    }
}