using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;
using EsoftPortalMvc.Services.UserAdministration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Filters;
using System.Drawing.Printing;
using System.Drawing;
using EstateManagementMvc;
using Microsoft.Reporting.WebForms;


namespace EsoftPortalMvc.Controllers
{

    public class BaseController : Controller
    {
        public BaseController()
        {
            Toastr = new Toastr();
            if (Debugger.IsAttached == true)
            { // Development Mode
                DebugModeUser();
            }
        }

        private void DebugModeUser()
        {
            if (UserSession.Current.userDetails == null)
            {
                UserSession.Current.userDetails = new tbl_PortalMembers();
            }
            if ( UserSession.Current.userDetails.tbl_CustomerId == null ||  UserSession.Current.userDetails.tbl_CustomerId == Guid.Empty)
            {
                EsoftPortalEntities mainDb = new EsoftPortalEntities();
                var userTest = mainDb.tbl_PortalMembers.FirstOrDefault();

                SessionVariables.SetUserDetails(userTest, mainDb, GetIpAddress());

                UserSession.Current.userDetails.CustomerNo = "XXX";
                //UserSession.Current.userDetails.CustomerNo = "Development";

            }
        }

        public Toastr Toastr { get; set; }

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            return Toastr.AddToastMessage(title, message, toastType);
        }

        public ToastMessage AccessDeniedToastMessage(string module)
        {
            ToastType toastType = ToastType.Error;
            return Toastr.AddToastMessage("Esoft Financials", "You Are Not Authorised to Access" + module, toastType);
        }

        public class BaseActionFilter : ActionFilterAttribute
        {
            // This method is called BEFORE the action method is executed
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                GetUserIpAddress(filterContext);

                // Check for incoming Toastr objects, in case we've been redirected here

                BaseController controller = filterContext.Controller as BaseController;
                if (controller != null)
                    controller.Toastr = (controller.TempData["Toastr"] as Toastr)
                                         ?? new Toastr();

                base.OnActionExecuting(filterContext);
            }

            private void GetUserIpAddress(ActionExecutingContext filterContext)
            {
                string visitorIpAddress = filterContext.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrWhiteSpace(visitorIpAddress))
                {
                    visitorIpAddress = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrWhiteSpace(visitorIpAddress))
                {
                    visitorIpAddress = filterContext.HttpContext.Request.UserHostAddress;
                }
                //UserSession.Current.MachineName = filterContext.HttpContext.Request.ServerVariables["REMOTE_HOST"]; // Can be used to only allow valid ip to login
                UserSession.Current.MachineName = visitorIpAddress; // Can be used to only allow valid ip to login
            }

            // This method is called AFTER the action method is executed but BEFORE the
            // result is processed (in the view or in the redirection)
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                BaseController controller = filterContext.Controller as BaseController;
                if (filterContext.Result.GetType() == typeof(ViewResult))
                {
                    if (controller.Toastr != null)
                    {
                        if (controller.Toastr.ToastMessages.Count() > 0)
                        {
                            // We're going to a view so we store Toastr in the ViewData collection
                            controller.ViewData["Toastr"] = controller.Toastr;
                        }
                    }
                }
                else if (filterContext.Result.GetType() == typeof(RedirectToRouteResult))
                {
                    if (controller.Toastr != null && controller.Toastr.ToastMessages.Count() > 0)
                    {
                        // User is being redirected to another action method so we store Toastr in
                        // the TempData collection
                        controller.TempData["Toastr"] = controller.Toastr;
                    }
                }

                base.OnActionExecuted(filterContext);
            }
        }

        protected string GetIpAddress()
        {
            string visitorIpAddress = "";
            if (Request != null)
            {
                visitorIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrWhiteSpace(visitorIpAddress))
                {
                    visitorIpAddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                if (string.IsNullOrWhiteSpace(visitorIpAddress))
                {
                    visitorIpAddress = Request.UserHostAddress;
                }
            }
            return visitorIpAddress;
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public class ForceUserToLogin : ActionFilterAttribute, IAuthenticationFilter
        {
            public void OnAuthentication(AuthenticationContext filterContext)
            {
            }

            public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
            {
                if (UserSession.Current.userDetails == null ||  UserSession.Current.userDetails.tbl_CustomerId == null ||  UserSession.Current.userDetails.tbl_CustomerId == Guid.Empty)// if (user.Identity.IsAuthenticated == false)
                {
                    string rawUrl = filterContext.HttpContext.Request.RawUrl.ToUpper();
                    if (rawUrl != "/")
                    {
                        if (!rawUrl.Contains("/ACCOUNT/LOGIN"))
                        {
                            filterContext.Result = new HttpUnauthorizedResult(); // Redirect to Login Page As Defined in web.config
                        }
                    }
                }
            }
        }

        public bool CheckUserAccess(string moduleId)
        {
            return CheckUserAccessRights.CheckUserAccess(moduleId);
        }


        public void CheckToastMessageFromModel(ModelStateDictionary modelState)
        {
            ToastType toastType = ToastType.Info;
            var toastMsgs = modelState.Where(x => x.Key == "TOASTER").ToList();
            foreach (var item in toastMsgs)
            {
                Toastr.AddToastMessage("Esoft Financials", item.Value.Errors[0].ErrorMessage.ToString(), toastType);
            }
            modelState.Remove("TOASTER");
        }

        /// <summary>
        /// Used when printing (Display PDF OutPut)
        /// </summary>
        /// <returns></returns>
        /// Changed method name to be more Generic to render both PDF and Excel files --Catherine
        protected string RenderOutPut(ReportViewer viewer, string file_name, string file_type)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat> " + file_type + "</OutputFormat>" +
                   "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.5in</MarginTop>" +
                    "  <MarginLeft>0.15in</MarginLeft>" +
                     "  <MarginRight>0.2in</MarginRight>" +
                     "  <MarginBottom>0.5in</MarginBottom>" +
                   "</DeviceInfo>";
            file_name = ValueConverters.MakeValidFileName(file_name);

            byte[] bytes = viewer.LocalReport.Render(file_type, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            string path = SessionVariables.Report_Temp_Directory; // Server.MapPath("~/ReportTemp/");
            Random rnd = new Random();
            int month = rnd.Next(1, 13); // creates a number between 1 and 12
            int dice = rnd.Next(1, 7); // creates a number between 1 and 6
            int card = rnd.Next(9); // creates a number between 0 and 51


            // System.Net.WebClient
            //3. After that use file stream to write from bytes to pdf file on your server path

            FileStream file = new FileStream(path + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            file.Write(bytes, 0, bytes.Length);
            file.Dispose();

            //evaluate file type work flow
            string url = string.Empty;
            Session["ReportFileToPreview"] = path + "\\" + file_name;

            if (file_type.ToUpper() == "PDF")
            {
                var urlBuilder =
                new System.UriBuilder(Request.Url.AbsoluteUri)
                {
                    Path = Url.Action("Print_Dynamic.aspx", "Reports", new { Area = "" }),
                    Query = null,
                };

                Uri uri = urlBuilder.Uri;
                url = urlBuilder.ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!controllerName.Contains("Reports"))
                {
                    controllerName = "/" + controllerName;
                    url = url.Replace(controllerName, "");

                }

                url = string.Format("<script>window.open('{0}','_blank');</script>", url);

            }
            else
            {

                //TODO:Review this
                url = string.Format("<script>window.open('{0}','_blank');</script>", url);

            }
            return url;
        }

        public List<ModelErrorView> JsonModelErrors(string newKey = "")
        {
            List<ModelErrorView> jsonModelErrors = new List<ModelErrorView>();

            var modelState = this.ModelState;
            var errorModel =
                  from x in modelState.Keys
                  where modelState[x].Errors.Count > 0
                  select new
                  {
                      key = x /*.Replace('.','_')*/,
                      errors = modelState[x].Errors.
                                             Select(y => y.ErrorMessage).
                                             ToArray()
                  };

            foreach (var item in errorModel)
            {
                string key = string.Empty;
                if (string.IsNullOrWhiteSpace(newKey))
                {
                    if (!string.IsNullOrWhiteSpace(item.key))
                    {
                        key = item.key.Substring(0, 1).ToUpper() + item.key.Substring(1, item.key.Length - 1);
                    }
                }
                else
                {
                    key = newKey;
                }

                jsonModelErrors.Add(new ModelErrorView
                {
                    Key = key,
                    Errors = item.errors
                });
            }
            return jsonModelErrors;
        }

        public string BaseUrl()
        {
            //string url = Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~").Trim();
            return Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~").Trim();
        }

        public string DynamicReportUrl()
        {
            return BaseUrl() + "Reports/Print_Dynamic.aspx";
        }

        protected void ClearModelErrors(string keyToCheck)
        { // Clear Errors not needed esp when a table Field is used more than once in a view
            var allErros = ModelState.Where(x => x.Value.Errors.Count() > 0).ToList();

            var keyExists = ModelState.Where(m => m.Key == keyToCheck).FirstOrDefault();
            if (keyExists.Value != null)
            {
                ModelState.Where(m => m.Key == keyToCheck).FirstOrDefault().Value.Errors.Clear();
            }
        }








    }
}