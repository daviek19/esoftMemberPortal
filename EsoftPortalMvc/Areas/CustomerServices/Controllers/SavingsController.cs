using ESoft.Web.Services.Registry;
using EsoftPortalMvc.Controllers;
using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstateManagementMvc.Areas.CustomerServices.Controllers
{
    public class SavingsController : BaseController
    {
        CustomerAccountsManager customerAccountsManager;
        CustomerManager customerManager;

        public SavingsController()
        {
            customerAccountsManager = new CustomerAccountsManager();
            customerManager = new CustomerManager();
        }

        public ActionResult Index()
        {
            SavingsViewModel savingsViewModel = new SavingsViewModel();

            string customerno = "4103";

            List<CustomerAccountsView> customerAccounts = new List<CustomerAccountsView>();

            CustomerDetailsView customer = new CustomerDetailsView();

            customerAccounts = customerAccountsManager.GetCustomerAccounts(customerno, false);

            List<CustomerDetailsView> customerDetailsView = customerManager.GetCustomerDetails(customerno);

            if (customerDetailsView.Count > 0)
            {
                customer = customerDetailsView[0];

                savingsViewModel.CustomerDetails = customer;
                savingsViewModel.SavingsAccounts = customerAccounts;
            }

            return View(savingsViewModel);
        }

    }
}