using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;
using System.Web.Security;
using EsoftPortalMvc.Services.UserAdministration;
using System.Collections.Generic;
using ESoft.Web.Services.Registry;

namespace EsoftPortalMvc.Controllers
{
    public class AccountController : BaseController
    {
        Esoft_EstateEntities db = new Esoft_EstateEntities();
        CustomerManager customerManager = new CustomerManager();
        // GET: Account
        public ActionResult Login()
        {
            //Utility.WriteErrorLog("Testing Login");
            LoginViewModel login = new LoginViewModel();

            login.OrganisationName = SessionVariables.CompanyName;

            FormsAuthentication.SignOut();
            //Utility.WriteErrorLog("Proceed to Login Page");
            return View(login);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAdministrationManager userMgr = new UserAdministrationManager();
                string machineName = GetIpAddress();
                string loginResult = userMgr.Login(model.Email, model.Password, machineName);
                if (ValueConverters.IsStringEmpty(loginResult) == false)
                {
                    if (loginResult == "CHANGEPASSWORD")
                    {
                        return RedirectToAction("Index", "ChangeUserPassword", new { Area = "SystemSettings" });
                    }
                    TempData["Message"] = loginResult;
                    return View();
                }
                else
                {
                    MenuManager menuManger = new MenuManager();

                    menuManger.GetUserMenuItems(UserSession.Current.userDetails.AccessRights);

                    FormsAuthentication.SetAuthCookie(UserSession.Current.userDetails.FullName, false);
                    this.AddToastMessage("Esoft Financials ", "Welcome " + UserSession.Current.userDetails.LoginName, ToastType.Success);

                    Utility.WriteErrorLog("Login into System : Welcome " + UserSession.Current.userDetails.LoginName);
                    return RedirectToAction("Index", "Dashboard", new { Area = "Dashboard" });
                }
            }
            else
            {
                return View();
            }
        }

        //ToDo: Test Login Timeout Page
        public ActionResult LoginTimeout(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //ToDo: Test for user re-login after timeout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginTimeout(LoginViewModel usermodel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserAdministrationManager userMgr = new UserAdministrationManager();

                string loginResult = userMgr.Login(usermodel.Email, usermodel.Password, GetIpAddress());
                if (ValueConverters.IsStringEmpty(loginResult) == false)
                {
                    TempData["Message"] = loginResult;
                    return View();
                }
                else
                {
                    MenuManager menuManger = new MenuManager();

                    menuManger.GetUserMenuItems(UserSession.Current.userDetails.AccessRights);


                    this.AddToastMessage("Esoft Financials ", "Welcome " + UserSession.Current.userDetails.LoginName, ToastType.Success);

                    // return RedirectToAction("Default", "Home");
                    return RedirectToLocal(returnUrl);
                }
            }
            else
            {
                return View();
            }

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard", new { Area = "Dashboard" });
        }

        public ActionResult Register()
        {
            PortalRegisterViewModel portalRegisterModel = new PortalRegisterViewModel();

            portalRegisterModel.OrganisationName = SessionVariables.CompanyName;

            return View(portalRegisterModel);
        }

        [HttpPost]
        public ActionResult Register(PortalRegisterViewModel portalRegisterModel)
        {
            if (ModelState.IsValid)
            {
                List<CustomerDetailsView> customerDetailsView = customerManager.GetCustomerDetails(portalRegisterModel.MemberNo);

                if (customerDetailsView.Count > 0)
                {
                    if (customerDetailsView[0].CustomerIdNo.Trim() == portalRegisterModel.IdNo.Trim())
                    {
                        //Everything is matching
                        //1) Generate pass-code
                        //2) Create the account under tbl_PortalMembers
                        //3) Send Email and SMS for OTP
                        //4) Show success and redirect to login page
                    }
                    else
                    {
                        //The ID does not match the account
                        ModelState.AddModelError("IdNo", "The id number does not match, contact our customer service for assistance.");
                    }
                }
            }


            return View();
        }

        [HttpPost]
        public ActionResult AccountExists(string memberNo)
        {
            CustomerDetailsView customerDetails = new CustomerDetailsView();

            customerDetails.CustomerName = "Account Not Found";

            if (memberNo != null)
            {
                List<CustomerDetailsView> customerDetailsView = customerManager.GetCustomerDetails(memberNo);

                if (customerDetailsView.Count > 0)
                {
                    //has an account
                    PortalMembers portalMember = customerManager.GetPortalCustomer(memberNo);

                    if (!string.IsNullOrWhiteSpace(portalMember.CustomerNo))
                    {
                        customerDetails.CustomerName = "Account already registered for web portal services";
                    }
                    else
                    {
                        customerDetails = customerDetailsView[0];
                    }
                }
            }

            return Json(new { customerData = customerDetails }, JsonRequestBehavior.AllowGet);
        }
    }
}