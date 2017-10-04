using AutoMapper;
using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EstateManagementMvc;

namespace EsoftPortalMvc.Services.Common
{
    public static class SessionVariables
    {
        public static string LoginCode { get; set; }
        public static string LoginName { get; set; }
        public static string AccessRights { get; set; }
        public static string GlAccountMask { get; set; }
        public static string CustomerNumberMask { get; set; }
        public static string CustomerAccountMask { get; set; }
        public static Guid UserId { get; set; }
        public static string UserBranch { get; set; }
        public static string MachineName { get; set; }
        public static string Log_Directory { get; set; }
        public static string TransactionPostedSuccessfully;
        public static string CompanyName;
        public static string CompanyAddress;
        public static string CompanyAddress1;
        public static string CompanyTelephone;
        public static double Excise_Duty_Rate;
        public static double Teller_Commission_Split_Percentage;
        public static decimal DefaultSavingsType { get; set; }
        public static string Report_Temp_Directory { get; set; }
        public static string UserImagePath { get; set; }
        public static string Report_Viewer_Control { get; set; }
        public static string SASRA_Directory { get; set; }
        public static string SASRA_Exports { get; set; }

        static SessionVariables()
        {
            try
            {
                //GlAccountMask = System.Configuration.ConfigurationManager.AppSettings["GlAccountMask"].ToString();
                //CustomerNumberMask = System.Configuration.ConfigurationManager.AppSettings["CustomerNumberMask"].ToString();
                //CustomerAccountMask = System.Configuration.ConfigurationManager.AppSettings["CustomerAccountMask"].ToString();
                Log_Directory = System.Configuration.ConfigurationManager.AppSettings["Error_Log_Directory"].ToString();
                Report_Temp_Directory = System.Configuration.ConfigurationManager.AppSettings["Report_Temp_Directory"].ToString();
                //UserImagePath = System.Configuration.ConfigurationManager.AppSettings["UserImagePath"].ToString();
                //SASRA_Directory = System.Configuration.ConfigurationManager.AppSettings["SASRA_Directory"].ToString();
                //SASRA_Exports = System.Configuration.ConfigurationManager.AppSettings["SASRA_Exports"].ToString();
                TransactionPostedSuccessfully = "Transactions Was Updated Successfully ";
                //Utility.WriteErrorLog("Setting Default Variables");
                GetCompanyInfo();
                DefaultSavingsType = 1;

                CheckLogDirectories(Log_Directory);
                CheckLogDirectories(Report_Temp_Directory);
                CheckLogDirectories(UserImagePath);
                CheckLogDirectories(SASRA_Directory);
                CheckLogDirectories(SASRA_Exports);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("SessionVariables", ref ex);
            }
        }

        private static void GetCompanyInfo()
        {
            EsoftPortalEntities mainDb = new EsoftPortalEntities();
            var company = mainDb.Companies.FirstOrDefault();
            CompanyName = company.CompanyName;
            CompanyAddress = company.CompanyAddress;
            CompanyAddress1 = company.CompanyEmail;
            CompanyTelephone = company.CompanyTelephone;
            Excise_Duty_Rate = (double)ValueConverters.ConvertNullToDecimal(company.Excise_Duty_Rate);
            Teller_Commission_Split_Percentage = 0;
        }

        public static void CheckLogDirectories(string directory)
        {
            try
            {
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                    var user = System.Security.Principal.WindowsIdentity.GetCurrent().User;
                    var userName = user.Translate(typeof(System.Security.Principal.NTAccount));
                    var dirInfo = new System.IO.DirectoryInfo(directory);
                    var sec = dirInfo.GetAccessControl();
                    sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(userName,
                                    System.Security.AccessControl.FileSystemRights.Modify,
                                    System.Security.AccessControl.AccessControlType.Allow)
                                    );
                    dirInfo.SetAccessControl(sec);
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("Error Check Directory " + directory + " " + ex.InnerException.ToString());
            }
        }

        public static void SetUserDetails(tbl_PortalMembers tbl_PortalMember, EsoftPortalEntities mainDb, string machineName)
        {
            //SessionVariables.LoginCode = user.LoginCode;
            //SessionVariables.AccessRights = user.AccessRights;
            //SessionVariables.UserId = user.tbl_usersID;
            //SessionVariables.LoginName = user.LoginName;
            //SessionVariables.UserBranch = user.UserBranch;
            UserSession.Current.userDetails = new tbl_PortalMembers() ;
            UserSession.Current.userDetails = tbl_PortalMember;
            UserSession.Current.MachineName = machineName;
            MenuManager menuMgr = new MenuManager();
           // UserSession.Current.UserImage = @String.Format(SessionVariables.UserImagePath + UserSession.Current.userDetails.CustomerNo.Trim() + ".png");
            UserSession.Current.Teller_Footer_Text = mainDb.Companies.FirstOrDefault().Teller_Slip_FooterText;
            UserSession.Current.userDetails.CustomerNo = UserSession.Current.userDetails.CustomerNo.ToUpper();
            AuditTrail audit = new AuditTrail();

            audit.CreateTrailRecord(mainDb, UserSession.Current.userDetails.CustomerNo, "Login to System (Web)", 0, UserSession.Current.userDetails.CustomerNo, "LOGIN", true);

        }

        public static double Calculate_Excise_Duty(double transactionAmount)
        {
            return ValueConverters.Round05(transactionAmount * SessionVariables.Excise_Duty_Rate * .01);
        }



        public static List<Company> CompanyStaticData()
        {
            Company company = new Company { CompanyAddress = SessionVariables.CompanyAddress, CompanyName = SessionVariables.CompanyName, CompanyTelephone = SessionVariables.CompanyTelephone };
            List<Company> companyInfox = new List<Company>();
            companyInfox.Add(company);

            return companyInfox;
        }
    }

}