using AutoMapper;
using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Accounts;
using EsoftPortalMvc.Services.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Services.UserAdministration
{
    public class UserAdministrationManager
    {
        private Esoft_EstateEntities mainDb = new Esoft_EstateEntities();
        private IValidationDictionary _validatonDictionary;
        
        public UserAdministrationManager()
        {

        }

        public UserAdministrationManager(IValidationDictionary validationDictionary)
        {
            _validatonDictionary = validationDictionary;
        }

        public List<UserDetailsView> GetAllUsers()
        {
            Mapper.CreateMap<tbl_users, UserDetailsView>();

            List<tbl_users> branchUsers = mainDb.tbl_users.OrderBy(x => x.LoginCode).ToList();
            List<UserDetailsView> userdetails = Mapper.Map<List<UserDetailsView>>(branchUsers);            
            var userRoles = mainDb.Tbl_UserRoles.ToList();

           
            var singleRole = userRoles.FirstOrDefault();

            int count = 0;
            foreach (var item in userdetails)
            {
               
                singleRole = userRoles.FirstOrDefault(x => x.RoleCode == item.UserRole);
                               
                if (singleRole != null)
                {
                    userdetails[count].RoleName = ValueConverters.ConvertNullToEmptyString(singleRole.RoleName);
                }

                userdetails[count].AccountDisabled = ValueConverters.ConvertNullToBool(userdetails[count].AccountDisabled);
                count = count + 1;
            }

            return userdetails;
        }

        public tbl_users GetUser(Guid? id)
        {
            Mapper.CreateMap<tbl_users, UserDetailsView>();
            tbl_users user = mainDb.tbl_users.Where(x => x.tbl_usersID == id).FirstOrDefault();
            return user;//Mapper.Map<List<UserDetailsView>>(user);
        }

       

        public List<TellerAccountView> TellerAccountDetails(string userID)
        {
            List<TellerAccountView> tellerAccount = new List<TellerAccountView>();
            var teller = mainDb.tbl_TellerAccounts.FirstOrDefault(x => x.LoginCode == userID);
            if (teller != null)
            {
                tellerAccount.Add(new TellerAccountView
                {
                    LoginCode = teller.LoginCode,
                    TellerAccountNo = teller.TellerAccountNo,
                    AuthorisedBranch = ""
                });
            }
            return tellerAccount;
        }

        // used By EsoftPortalMvc.Services.Accounts DirectLedgerManager.CreateTellerPayment
        // userBranch can be empty or specify actual branch to filter
        public List<GlAccountsView> GetAllTellers(string userBranch = "")
        {
            List<GlAccountsView> glview = new List<GlAccountsView>();
            glview = (from users in mainDb.tbl_users
                      join tellerAccounts in mainDb.tbl_TellerAccounts on users.LoginCode equals tellerAccounts.LoginCode
                      join branches in mainDb.BranchSettings on users.UserBranch equals branches.BranchCode into branch
                      from br in branch.DefaultIfEmpty()
                      where (users.UserBranch == userBranch || userBranch == "")
                      select new GlAccountsView
                      {
                          GlAccountNo = tellerAccounts.TellerAccountNo.Trim(),
                          GlName = tellerAccounts.TellerAccountNo.Trim() + " " + users.FullName.Trim() + " ( " + (br == null ? string.Empty : br.Name.Trim()) + ")",
                          GlBalance = 0.00
                      }).OrderBy(x => x.GlAccountNo).ToList();

            return glview;
        }


        public string Login(string userName, string password, string userIpAddress)
        {
            string loginResult = string.Empty;
            var user = mainDb.tbl_users.Where(p => p.LoginName == userName);

            if (user == null || user.Count() != 1)
            {
                loginResult = "Wrong User name/Password Combination!";
            }
            else
            {
                password = EncryptPassword(userName, password);
                string savedPassword = user.FirstOrDefault().WebPassword;
                if (string.Compare(savedPassword, password) != 0)
                {
                    loginResult = "Wrong User name/Password Combination!";
                }
                else
                {
                    string defaultpassword = EncryptPassword(userName, "Password");
                    if (defaultpassword == password)
                    {
                        loginResult = "CHANGEPASSWORD";
                    }

                    SessionVariables.SetUserDetails(user.FirstOrDefault(), mainDb, userIpAddress);
                    if (loginResult == "CHANGEPASSWORD")
                    {
                        UserSession.Current.userDetails.AccessRights = ""; // Disable all rights user needs to change password
                        UserSession.Current.UserMenuIds.Clear();// Disable all rights user needs to change password
                    }

                }
            }
            return loginResult;
        }

        private static string EncryptPassword(string userName, string password)
        {
            password = Crypto.Encrypt(userName.ToLower().Trim() + password.Trim());
            return password;
        }

        public List<UserDetail_PartialDetails> GetAuthorisingUsers(List<UserDetail_PartialDetails> userdetails, decimal authorityAmount, string branch)
        {
            if (userdetails == null) userdetails = new List<UserDetail_PartialDetails>();

            //var users = mainDb.tbl_users.Where(x => x.TellerAuthorisationLimit >= authorityAmount &&
            //    (x.UserBranch == branch || x.UserBranch == "99")).ToList();
            //if (users != null)
            //{
            //    foreach (var item in users)
            //    {
            //        userdetails.Add(new UserDetail_PartialDetails
            //        {
            //            tbl_usersID = item.tbl_usersID,
            //            LoginCode = item.LoginCode,
            //            LoginName = item.LoginName,
            //            FullName = item.FullName,
            //            TellerAuthorisationLimit = item.TellerAuthorisationLimit
            //        });
            //    }
            //}
            return userdetails;
        }

        public List<UserRole> GetUserRoles()
        {
            List<UserRole> roles = new List<UserRole>();
            DbDataReader reader = DbConnector.GetSqlReader("SELECT RoleName,RoleCode,RoleRights,Tbl_UserRolesID as UserRoleId FROM Tbl_UserRoles order by RoleCode");
            roles = Functions.DataReaderMapToList<UserRole>(reader);
            return roles;
        }

        public String GenerateLoginCode()
        {
            string loginCode = PostTransactions.Generate_Posting_Reference("9900");
            return loginCode.Substring(0, 1) + loginCode.Substring(loginCode.Length - 2, 2);// will cause duplicates
        }

        public bool CreateUser(UserAdministrationViewModel userDetails)
        {
            bool insertResult = false;
            if (userDetails != null && userDetails.user != null)
            {
                if (!ValidateUserDetails(true, userDetails))
                    return false;

                userDetails.user.tbl_usersID = GuidGenerator.NewComb();
                userDetails.user.WebPassword = EncryptPassword(userDetails.user.LoginName, "Password");
                userDetails.user.LoginCode = this.GenerateLoginCode();

                String insertQuery = "INSERT INTO tbl_users (LoginCode, LoginName, FullName, EmailID, UserBranch," +
                     "LoginTime_From, LoginTime_To, LoginMachine, LoginPassword, AccountDisabled, AccessRights," +
                     "TellerAuthorisationLimit, PasswordExpiry, Login_Expiry, LoanApprovalLimit, LoanDisbursementLimit," +
                     "CreateDate, UserRole, Teller_Excess_GlAccount, Teller_Shortage_GlAccount, EmploymentNumber, SaccoMembershipNumber, " +
                     "CashWithdrawalAlertAmount, OtherUserRoles, tbl_usersID, WebPassword )" +
                     " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'," +
                     "'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')";


                var formtedQueryString = String.Format(insertQuery,
                    ValueConverters.format_sql_string(userDetails.user.LoginCode),
                    ValueConverters.format_sql_string(userDetails.user.LoginName),
                    ValueConverters.format_sql_string(userDetails.user.FullName),
                    ValueConverters.format_sql_string(userDetails.user.EmailID),
                    ValueConverters.format_sql_string(userDetails.user.UserBranch),
                    ValueConverters.format_sql_string(userDetails.user.LoginTime_From),
                    ValueConverters.format_sql_string(userDetails.user.LoginTime_To),
                    ValueConverters.format_sql_string(userDetails.user.LoginMachine),
                    ValueConverters.format_sql_string(userDetails.user.LoginPassword),
                    ValueConverters.ConvertNullToBool(userDetails.user.AccountDisabled),
                    ValueConverters.format_sql_string(userDetails.user.AccessRights),
                    ValueConverters.ConvertNullToDecimal(0),
                    ValueConverters.FormatSqlDate(userDetails.user.PasswordExpiry),
                    ValueConverters.FormatSqlDate(userDetails.user.Login_Expiry),
                    ValueConverters.ConvertNullToDecimal(0),
                    ValueConverters.ConvertNullToDecimal(0),
                    ValueConverters.FormatSqlDate(userDetails.user.CreateDate),
                    ValueConverters.format_sql_string(userDetails.user.UserRole),
                    ValueConverters.format_sql_string(""),
                    ValueConverters.format_sql_string(""),
                    ValueConverters.format_sql_string(userDetails.user.EmploymentNumber),
                    ValueConverters.format_sql_string(""),
                    ValueConverters.ConvertNullToDecimal(0),
                    ValueConverters.format_sql_string(userDetails.user.OtherUserRoles),
                    userDetails.user.tbl_usersID,
                    ValueConverters.format_sql_string(userDetails.user.WebPassword)
                );

                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();
                transEngine.Generate_Sql_Statement(transList, transactionID, formtedQueryString);


                //AuditUserDetailsChanges(userDetails);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                insertResult = results == SessionVariables.TransactionPostedSuccessfully;

            }
            return insertResult;
        }

        public bool UpdateUser(UserAdministrationViewModel userDetails)
        {
            bool updateResult = false;
            if (userDetails != null && userDetails.user != null)
            {
                if (!ValidateUserDetails(false, userDetails))
                    return false;

                userDetails.user.AccountDisabled = ValueConverters.ConvertNullToBool(userDetails.user.AccountDisabled);
                String updateQuery = "UPDATE tbl_users " +
                        "SET LoginName = '" + ValueConverters.format_sql_string(userDetails.user.LoginName) + "'," +
                        "AccountDisabled = '" + userDetails.user.AccountDisabled + "'," +
                         "FullName = '" + ValueConverters.format_sql_string(userDetails.user.FullName) + "'," +                        
                         "EmailID = '" + ValueConverters.format_sql_string(userDetails.user.EmailID) + "'," +                         
                         "UserBranch = '" + ValueConverters.format_sql_string(userDetails.user.UserBranch) + "'," +                         
                         "UserRole = '" + ValueConverters.format_sql_string(userDetails.user.UserRole) + "'," +                         
                         "LoginTime_From = '" + ValueConverters.format_sql_string(userDetails.user.LoginTime_From) + "'," +
                         "LoginTime_To = '" + ValueConverters.format_sql_string(userDetails.user.LoginTime_To) + "'," +
                         "Login_Expiry = '" + ValueConverters.FormatSqlDate(userDetails.user.Login_Expiry) + "'," +
                         "EmploymentNumber = '" + ValueConverters.format_sql_string(userDetails.user.EmploymentNumber) + "'," +
                         "LoginMachine = '" + ValueConverters.format_sql_string(userDetails.user.LoginMachine) + "'," +                         
                         " WHERE tbl_usersID = '" + userDetails.user.tbl_usersID + "' ";

                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();
                transEngine.Generate_Sql_Statement(transList, transactionID, updateQuery);

                //ToDo Error on Audit change when creating system users
                //AuditUserDetailsChanges(userDetails);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                updateResult = results == SessionVariables.TransactionPostedSuccessfully;
            }
            return updateResult;
        }

        private bool ValidateUserDetails(bool addMode, UserAdministrationViewModel userDetails)
        {
            if (addMode)
            {
                var usernameExists = mainDb.tbl_users.FirstOrDefault(x => x.LoginName.ToUpper() == userDetails.user.LoginName.ToUpper());
                if (usernameExists != null)
                {
                    _validatonDictionary.AddError("user.LoginName", string.Format("User Name already used By !" + ValueConverters.ConvertNullToEmptyString(usernameExists.FullName)));
                }
            }
            //ToDo Check that user enabling account is not user who created the account
            if (ValueConverters.IsStringEmpty(userDetails.user.FullName))
            {
                _validatonDictionary.AddError("user.FullName", "User's Full Name Is Missing");
            }
            if (ValueConverters.IsStringEmpty(userDetails.user.UserBranch))
            {
                _validatonDictionary.AddError("user.UserBranch", "User Branch Is Missing");
            }
            return _validatonDictionary.IsValid;
        }
        public bool ResetPassword(Guid userId)
        {
            bool updateResult = false;
            var userDetails = mainDb.tbl_users.FirstOrDefault(x => x.tbl_usersID == userId);
            if (userDetails != null)
            {
                string defaultPassword = EncryptPassword(userDetails.LoginName, "Password");

                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                transEngine.Generate_Sql_Statement(transList, transactionID, "UPDATE tbl_users SET WebPassword = '" + defaultPassword + "' WHERE tbl_usersID = '" + userId + "' ");
                transEngine.Generate_Sql_Statement(transList, transactionID, string.Format("INSERT INTO tbl_PasswordHistory(trxdate,LoginCode,passvalue)values(getdate(),'{0}','{1}')",
                    userDetails.LoginCode, defaultPassword));

                // Audit Changes
                AuditTrail.CreateTrailPostingRecord(transList, transactionID, "9900",
                    " User Password Reset for " + ValueConverters.format_sql_string(userDetails.LoginName) + " " + ValueConverters.format_sql_string(userDetails.FullName)
                    , 0, "USERMGMT", UserSession.Current.userDetails.LoginCode, false);

                AuditTrail.CreateTrailPostingRecord(transList, transactionID, "9900",
                    " Your User Password Was Reset By " +
                    ValueConverters.format_sql_string(UserSession.Current.userDetails.LoginName) + " " + ValueConverters.format_sql_string(UserSession.Current.userDetails.FullName)
                    , 0, "USERMGMT", UserSession.Current.userDetails.LoginCode, false);

                NotifyUserOnPasswordChange(userDetails, transList, transactionID, transEngine);
                string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                updateResult = results == SessionVariables.TransactionPostedSuccessfully;

            }
            return updateResult;
        }

        public PasswordSettingsViewModel GetUserPasswordSettings()
        {

            Mapper.CreateMap<tbl_PasswordSettings, PasswordSettingsViewModel>();

            tbl_PasswordSettings passwordSettings = mainDb.tbl_PasswordSettings.FirstOrDefault();

            return Mapper.Map<PasswordSettingsViewModel>(passwordSettings);
        }

        public bool UpdateUserPasswordSettings(PasswordSettingsViewModel passwordSettings)
        {
            bool updateResult = false;
            if (passwordSettings != null)
            {
                var savedSettings = mainDb.tbl_PasswordSettings.FirstOrDefault();

                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                var updateQuery = "UPDATE tbl_PasswordSettings " +
                    "SET PasswordExpiresAfter = '" + (passwordSettings.PasswordExpiresAfter.ToString()) + "', " +
                    "AlphaNumeric = '" + ValueConverters.ConvertNullToBool(passwordSettings.AlphaNumeric) + "', " +
                    "ReusePasswordAfter = '" + passwordSettings.ReusePasswordAfter.ToString() + "', " +
                    "MinimumPasswordLength = '" + passwordSettings.MinimumPasswordLength.ToString() + "';";

                transEngine.Generate_Sql_Statement(transList, transactionID, updateQuery);

                // Audit Changes
                AuditTrail.CreateTrailPostingRecord(transList, transactionID, "9902", "Password Settings" +
                    string.Format("Change Password Expires from {0} to {1}. AlphaNumeric Passwords from {2} to {3}.ReusePassword After from {4} to {5}.Minimum Password Length from {6} to {7}.",
                    savedSettings.PasswordExpiresAfter.ToString(), passwordSettings.PasswordExpiresAfter.ToString(),
                    ValueConverters.ConvertNullToBool(savedSettings.AlphaNumeric), ValueConverters.ConvertNullToBool(passwordSettings.AlphaNumeric),
                    passwordSettings.PasswordExpiresAfter.ToString(), passwordSettings.PasswordExpiresAfter.ToString(),
                    passwordSettings.MinimumPasswordLength.ToString(), passwordSettings.MinimumPasswordLength.ToString()), 0, "PWDSETTINGS", UserSession.Current.userDetails.LoginCode, false);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                updateResult = results == SessionVariables.TransactionPostedSuccessfully;
            }

            return updateResult;
        }

        private void AuditUserDetailsChanges(UserAdministrationViewModel userDetails)
        {
            var user = mainDb.tbl_users.FirstOrDefault(x => x.tbl_usersID == userDetails.user.tbl_usersID);
            if (user == null)
            {
                user = new tbl_users();
            }
            user = Mapper.Map<tbl_users>(userDetails.user);


            //ToDo Not Auditing
            AuditTrail auditTrail = new AuditTrail();
            if (mainDb.Entry(user).State == EntityState.Added)
            {
                foreach (string propertyName in mainDb.Entry(user).CurrentValues.PropertyNames)
                {
                    var newValue = mainDb.Entry(user)
                                     .CurrentValues.GetValue<object>(propertyName);
                    if (newValue != null)
                    {
                        string activity = " Captured Value " + propertyName.Trim() + newValue.ToString().Trim();
                        auditTrail.CreateTrailRecord(mainDb, UserSession.Current.userDetails.LoginCode, activity, 0, user.LoginName, "9900", false);
                    }
                }
            }
            else
            {
                foreach (string propertyName in mainDb.Entry(user).OriginalValues.PropertyNames)
                {
                    var newValue = mainDb.Entry(user)
                                     .CurrentValues.GetValue<object>(propertyName);

                    var originalValue = mainDb.Entry(user)
                                        .OriginalValues.GetValue<object>(propertyName);

                    if (originalValue != null && newValue == null && !originalValue.Equals(newValue))
                    {
                        string activity = " Changed " + propertyName.Trim() + " From " + originalValue.ToString().Trim() + " To ";
                        auditTrail.CreateTrailRecord(mainDb, UserSession.Current.userDetails.LoginCode, activity, 0, user.LoginName, "9900", false);
                    }
                    else if (originalValue == null && newValue != null)
                    {
                        string activity = " Changed " + propertyName.Trim() + " From  To " + newValue.ToString().Trim();
                        auditTrail.CreateTrailRecord(mainDb, UserSession.Current.userDetails.LoginCode, activity, 0, user.LoginName, "9900", false);
                    }
                    else if (originalValue != null && newValue == null)
                    {
                        string activity = " Changed " + propertyName.Trim() + " From " + originalValue.ToString().Trim() + " To ";
                        auditTrail.CreateTrailRecord(mainDb, UserSession.Current.userDetails.LoginCode, activity, 0, user.LoginName, "9900", false);
                    }
                }
            }
        }

        //public TellersAccountsViewModel GetTellersAccounts()
        //{
        //    List<TellersAccountsDetails> tellersAccountsDetails = new List<TellersAccountsDetails>();
        //    var accountlist = (from accounts in mainDb.tbl_TellerAccounts
        //                       join branch in mainDb.BranchSettings on accounts.AuthorisedBranch equals branch.BranchCode
        //                       join users in mainDb.tbl_users on accounts.LoginCode equals users.LoginCode
        //                       select new TellersAccountsDetails
        //                       {
        //                           Code = accounts.LoginCode,
        //                           GlAccount = accounts.TellerAccountNo,
        //                           TellerName = users.FullName,
        //                           BranchName = branch.Name,
        //                           TellerAccountsID = accounts.tbl_TellerAccountsID.ToString(),
        //                       }).ToList();
        //    if (accountlist != null)
        //    {
        //        tellersAccountsDetails = accountlist;
        //    }
        //    TellersAccountsViewModel tellersAccountsViewModel = new TellersAccountsViewModel();
        //    tellersAccountsViewModel.TellerAccount = tellersAccountsDetails;
        //    return tellersAccountsViewModel;
        //}


        //    public SingleTellerViewModel GetSingleTeller(Guid? id, GlAccountsManager glAccountsManager)
        //    {           



        //        SingleTellerViewModel singleTellerViewModel = new SingleTellerViewModel();
        //        GetTellerProperties(glAccountsManager, singleTellerViewModel);

        //        singleTellerViewModel.SingleTellerRecord = (from accounts in mainDb.tbl_TellerAccounts
        //                                                    join users in mainDb.tbl_users on accounts.LoginCode equals users.LoginCode into tempAccount
        //                                                    from user in tempAccount.DefaultIfEmpty()
        //                                                    where accounts.tbl_TellerAccountsID == id
        //                                                    select new SingleTellerRecord
        //                                                    {
        //                                                        TellerAccountsID = accounts.tbl_TellerAccountsID,
        //                                                        TellerAccountNo = accounts.TellerAccountNo,
        //                                                        AuthorisedBranch = accounts.AuthorisedBranch,
        //                                                        UserRecordId= (user==null?Guid.Empty:user.tbl_usersID),
        //                                                        LoginCode = (user == null ? string.Empty : user.FullName),
        //                                                        ShortageAccount = (user == null ? string.Empty : user.Teller_Shortage_GlAccount),
        //                                                        ExcessAcount = (user == null ? string.Empty : user.Teller_Excess_GlAccount),
        //                                                    }).FirstOrDefault();

        //        return singleTellerViewModel;
        //    }

        //    public void GetTellerProperties(GlAccountsManager glaccounts, SingleTellerViewModel singleTellerViewModel)
        //    {
        //        singleTellerViewModel.TellerAccounts = new List<System.Web.Mvc.SelectListItem>();
        //        singleTellerViewModel.Branches = mainDb.BranchSettings.Select(x =>
        //            new SelectListItem { Value = x.BranchCode, Text = x.Name, Selected = false }).ToList();
        //        singleTellerViewModel.GlAccounts = glaccounts.GlAccounts(mainDb).ToList();
        //    }






        //    public bool UpdateTellerDetails(SingleTellerViewModel tellerDetails)
        //    {
        //        return true;
        //    }


        public TellerViewModel GetTellerDetails()
        {
            GlAccountsManager glAccountsManager = new GlAccountsManager();
            TellerViewModel tellerViewModel = new TellerViewModel();


            var accounts = glAccountsManager.GlAccounts(mainDb);

            tellerViewModel.Users = (from userslist in mainDb.tbl_users
                                     where !(mainDb.tbl_TellerAccounts.Any(x => x.LoginCode == userslist.LoginCode))
                                     select userslist).ToList();


            
            tellerViewModel.TellerAccounts = accounts;
            tellerViewModel.ExcessAccounts = accounts;
            tellerViewModel.ShortageAccounts = accounts;

            return tellerViewModel;
        }

        public bool CreateTeller(TellerViewModel tellerDetails)
        {
            bool insertResult = false;

            if (tellerDetails != null)
            {
                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();
                string tbl_TellerAccountsID = Guid.NewGuid().ToString();
                string insertTeller = "INSERT INTO tbl_TellerAccounts (LoginCode,TellerAccountNo,AuthorisedBranch,tbl_TellerAccountsID) VALUES ('" + ValueConverters.format_sql_string(tellerDetails.User) + "','" + ValueConverters.format_sql_string(tellerDetails.TellerAccount.Trim()) + "','" + ValueConverters.format_sql_string(tellerDetails.Branch) + "','" + tbl_TellerAccountsID + "');";
                String updateUserExcessShortageAc = "UPDATE tbl_users SET Teller_Excess_GlAccount = '" + ValueConverters.format_sql_string(tellerDetails.ExcessAccount.Trim()) + "', Teller_Shortage_GlAccount = '" + ValueConverters.format_sql_string(tellerDetails.ShortageAccount.Trim()) + "' WHERE LoginCode = '" + ValueConverters.format_sql_string(tellerDetails.User) + "';";
                String queryBatch = insertTeller + updateUserExcessShortageAc;

                transEngine.Generate_Sql_Statement(transList, transactionID, queryBatch);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);

                insertResult = results == SessionVariables.TransactionPostedSuccessfully;

            }

            return insertResult;
        }

        public bool UpdateTellerDetails(SingleTellerViewModel tellerDetails)
        {
            bool updateResult = false;

            if (!String.IsNullOrEmpty(tellerDetails.SingleTellerRecord.UserRecordId.ToString()) && !String.IsNullOrEmpty(tellerDetails.SingleTellerRecord.TellerAccountsID.ToString()))
            {
                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                string updateUser = "UPDATE tbl_users set Teller_Excess_GlAccount = '" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.ExcessAcount.Trim()) + "' , Teller_Shortage_GlAccount = '" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.ShortageAccount.Trim()) + "' WHERE tbl_usersID = '" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.UserRecordId.ToString()) + "'; ";
                String updateTellerAccount = "UPDATE tbl_TellerAccounts set TellerAccountNo ='" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.TellerAccountNo.Trim()) + "', AuthorisedBranch = '" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.AuthorisedBranch) + "' where tbl_TellerAccountsID = '" + ValueConverters.format_sql_string(tellerDetails.SingleTellerRecord.TellerAccountsID.ToString()) + "'; ";
                String queryBatch = updateTellerAccount + updateUser;

                transEngine.Generate_Sql_Statement(transList, transactionID, queryBatch);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);

                updateResult = results == SessionVariables.TransactionPostedSuccessfully;

            }

            return updateResult;
        }

        public bool ChangeUserPassword(ChangePasswordViewModel newPasswordDetails)
        {
            bool updateResult = false;
            if (newPasswordDetails.ConfirmPassword != newPasswordDetails.NewPassword)
            {
                _validatonDictionary.AddError("ConfirmPassword", "Passwords Do not Match");
                return false;
            }
            try
            {
                Guid userId = UserSession.Current.userDetails.tbl_usersID;
                var userDetails = mainDb.tbl_users.FirstOrDefault(x => x.tbl_usersID == userId);
                if (userDetails != null)
                {
                    string password = EncryptPassword(userDetails.LoginName, newPasswordDetails.OldPassword);
                    string savedPassword = userDetails.WebPassword;
                    if (string.Compare(savedPassword, password) != 0)
                    {
                        _validatonDictionary.AddError("OldPassword", "Wrong Old Password");
                    }
                    else
                    {
                        string newPassword = EncryptPassword(userDetails.LoginName, newPasswordDetails.ConfirmPassword);
                        var passwordSettings = mainDb.tbl_PasswordSettings.FirstOrDefault();
                        if (passwordSettings != null)
                        {
                            if (ValueConverters.ConvertNullToDecimal(passwordSettings.MinimumPasswordLength) > 0)
                            {
                                if (newPasswordDetails.ConfirmPassword.Trim().Length < passwordSettings.MinimumPasswordLength)
                                {
                                    _validatonDictionary.AddError("ConfirmPassword", String.Format("New Password Must be At Least {} Characters  Long", passwordSettings.MinimumPasswordLength.ToString()));
                                }
                            }

                            if (ValueConverters.ConvertNullToBool(passwordSettings.AlphaNumeric))
                            {
                                bool validPasssword = PasswordComplexity.IsValidPassword(newPasswordDetails.ConfirmPassword);
                                if (!validPasssword)
                                {
                                    _validatonDictionary.AddError("ConfirmPassword", "Password Must Contain an Alphabet,Numeric and Special Character");
                                }
                            }

                            DbDataReader reader = DbConnector.GetSqlReader(
                                " select TrxDate,PassValue from tbl_PasswordHistory where Logincode='" + userDetails.LoginCode + "' and PassValue ='" + newPassword.Trim() + "'" +
                                " and TrxDate>=DateAdd(DD,-" + passwordSettings.ReusePasswordAfter.ToString().Trim() + ",GETDATE()) order by TrxDate desc");
                            if (reader.HasRows)
                            {
                                _validatonDictionary.AddError("ConfirmPassword",
                                    string.Format("You Cannot Re-Use the Same Password Before {0} Days Are Over ", passwordSettings.ReusePasswordAfter.ToString("N2")));
                            }
                        }

                        if (_validatonDictionary.IsValid)
                        {

                            List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                            string transactionID = ValueConverters.RandomString(10);
                            PostTransactions transEngine = new PostTransactions();

                            transEngine.Generate_Sql_Statement(transList, transactionID, "UPDATE tbl_users SET WebPassword = '" + newPassword + "' WHERE tbl_usersID = '" + userId + "' ");
                            transEngine.Generate_Sql_Statement(transList, transactionID,
                                string.Format("INSERT INTO tbl_PasswordHistory(trxdate,LoginCode,passvalue)values(getdate(),'{0}','{1}')",
                                userDetails.LoginCode, newPassword));

                            // Audit Changes
                            AuditTrail.CreateTrailPostingRecord(transList, transactionID, "9900",
                                " Your User Password Has Been Changed " + ValueConverters.format_sql_string(userDetails.LoginName) + " " + ValueConverters.format_sql_string(userDetails.FullName)
                                , 0, "USERMGMT", UserSession.Current.userDetails.LoginCode, false);

                            NotifyUserOnPasswordChange(userDetails, transList, transactionID, transEngine);

                            string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                            updateResult = results == SessionVariables.TransactionPostedSuccessfully;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("ChangeUserPassword", ref ex);
            }

            return updateResult;
        }

        private void NotifyUserOnPasswordChange(tbl_users userDetails, List<PostTransactionsViewModel> transList, string transactionID, PostTransactions transEngine)
        {
            //if (userDetails.SaccoMembershipNumber != null)
            //{
            //    var custRecord = mainDb.tbl_Customer.FirstOrDefault(x => x.CustomerNo == userDetails.SaccoMembershipNumber);
            //    if (custRecord != null)
            //    {
            //        string smsMessage = " Your Esoft Financials System User Password Has Been Changed";
            //        transEngine.Generate_Sms_Message_NoCharge(transList, transactionID, custRecord.CustomerNo, custRecord.MobileNo, smsMessage);
            //    }
            //}
        }

        public Tbl_UserRoles GetSingleUSerRole(Guid id)
        {
            Mapper.CreateMap<Tbl_UserRoles, UserRole>();
            Tbl_UserRoles userRole = mainDb.Tbl_UserRoles.Where(x => x.Tbl_UserRolesID == id).FirstOrDefault();
            return userRole;
        }

        public bool UpdateUserRole(Tbl_UserRoles userRoles)
        {
            bool updateResult = false;

            List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
            string transactionID = ValueConverters.RandomString(10);
            PostTransactions transEngine = new PostTransactions();

            string updateUserRole = "UPDATE Tbl_UserRoles SET RoleName = '" + ValueConverters.format_sql_string(userRoles.RoleName.ToUpper()) + "' where RoleCode = '" + ValueConverters.format_sql_string(userRoles.RoleCode) + "';";

            transEngine.Generate_Sql_Statement(transList, transactionID, updateUserRole);

            string results = transEngine.Post_Transactions(transList, transactionID, false, false);

            updateResult = results == SessionVariables.TransactionPostedSuccessfully;

            return updateResult;
        }

        public bool CreateUserRole(Tbl_UserRoles userRoles)
        {
            bool insertRsult = false;
            if (userRoles != null)
            {
                userRoles.RoleCode = PostTransactions.Generate_Posting_Reference("990B").ToString();
                userRoles.RoleCode = userRoles.RoleCode.Substring(0, 1) + userRoles.RoleCode.Substring(userRoles.RoleCode.Trim().Length - 2, 2);
                userRoles.RoleRights = "";
                userRoles.Tbl_UserRolesID = Guid.NewGuid();

                String insertQuery = "INSERT INTO Tbl_UserRoles(RoleCode,RoleName,RoleRights,Tbl_UserRolesID) VALUES ('{0}','{1}','{2}','{3}')";
                var formtedQueryString = String.Format(insertQuery,
                   userRoles.RoleCode,
                   ValueConverters.format_sql_string(userRoles.RoleName.ToUpper()),
                    userRoles.RoleRights,
                    userRoles.Tbl_UserRolesID
               );

                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();
                transEngine.Generate_Sql_Statement(transList, transactionID, formtedQueryString);
                //AuditUserDetailsChanges(userRoles.RoleName);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);
                insertRsult = results == SessionVariables.TransactionPostedSuccessfully;
            }

            return insertRsult;
        }

        public UserRolesViewModel GetUserRoleDetails(Guid? roleId, UserRolesViewModel userRoleViewModel)
        {
            userRoleViewModel = userRoleViewModel == null ? new UserRolesViewModel() : userRoleViewModel;

            var roleRecord = mainDb.Tbl_UserRoles.FirstOrDefault(x => x.Tbl_UserRolesID == roleId);
            if (roleRecord != null)
            {
                userRoleViewModel.SingleUserRole = new UserRole
                {
                    RoleCode = roleRecord.RoleCode,
                    RoleName = roleRecord.RoleName,
                    RoleRights = roleRecord.RoleRights,
                    UserRoleId = roleRecord.Tbl_UserRolesID
                };
                List<UserRightsAssignment> userRights = new List<UserRightsAssignment>();
                string assignedUserRights = userRoleViewModel.SingleUserRole.RoleRights;

                GetUserRights(userRights, assignedUserRights);
                userRoleViewModel.RoleRights = new List<UserRightsAssignment>();
                userRoleViewModel.RoleRights.AddRange(userRights.OrderBy(x => x.ModuleId).ToList());

            }

            return userRoleViewModel;
        }

        public AssignRightsToUserViewModel GetUserRightsDetails(Guid? id, AssignRightsToUserViewModel assignRightsToUserViewModel)
        {
            List<UserRightsAssignment> userRights = new List<UserRightsAssignment>();
            if (id != null && id != Guid.Empty)
            {
                assignRightsToUserViewModel.CurrentUser = this.GetUser(id);
                assignRightsToUserViewModel.Existingroles = this.GetOtherUserRoles(id);
                assignRightsToUserViewModel.Users = this.GetAllUsers();
                this.GetUserRights(userRights, assignRightsToUserViewModel.CurrentUser.AccessRights);
                assignRightsToUserViewModel.RoleRights = userRights;
            }
            return assignRightsToUserViewModel;
        }

        private void GetUserRights(List<UserRightsAssignment> userRights, string assignedUserRights)
        {
            var moduleNames = mainDb.tbl_ModuleNames.ToList();
            foreach (var userright in assignedUserRights.Split(':'))
            {
                var singleModule = moduleNames.FirstOrDefault(x => x.ModuleId == userright);
                userRights.Add(new UserRightsAssignment
                {
                    ModuleId = userright,
                    Checked = true,
                    ModuleName = (singleModule == null ? string.Empty : singleModule.ModuleName)
                });
            }
            var notAssigned = (from allModules in moduleNames
                               join assigned in userRights on allModules.ModuleId equals assigned.ModuleId into temp
                               from exists in temp.DefaultIfEmpty()
                               where exists == null
                               select new UserRightsAssignment
                               {
                                   ModuleId = allModules.ModuleId,
                                   Checked = false,
                                   ModuleName = allModules.ModuleName
                               }).ToList();

            userRights.AddRange(notAssigned);
            userRights.RemoveAll(x => x.ModuleId == "");
        }

        public bool AssignRightsToRoles(Guid? userRoleID, string newJoinedRoleRights)
        {
            bool updateResult = false;

            if (!String.IsNullOrEmpty(userRoleID.ToString()))
            {
                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                string updateUserRole = "UPDATE Tbl_UserRoles SET RoleRights = '" + ValueConverters.format_sql_string(newJoinedRoleRights) + "' where Tbl_UserRolesID = '" + userRoleID.ToString() + "' ;";

                transEngine.Generate_Sql_Statement(transList, transactionID, updateUserRole);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);

                updateResult = results == SessionVariables.TransactionPostedSuccessfully;
            }

            return updateResult;
        }

        public TellersAccountsViewModel GetTellersAccounts()
        {
            List<TellersAccountsDetails> tellersAccountsDetails = new List<TellersAccountsDetails>();
            var accountlist = (from accounts in mainDb.tbl_TellerAccounts
                              // join branch in mainDb.BranchSettings on accounts.AuthorisedBranch equals branch.BranchCode
                               join users in mainDb.tbl_users on accounts.LoginCode equals users.LoginCode
                               orderby users.LoginCode
                               orderby users.UserBranch
                               orderby users.AccountDisabled
                               select new TellersAccountsDetails
                               {
                                   Code = accounts.LoginCode,
                                   GlAccount = accounts.TellerAccountNo.Trim(),
                                   TellerName = users.FullName,
                                   //BranchName = branch.Name,
                                   TellerAccountsID = accounts.tbl_TellerAccountsID.ToString(),
                               }).ToList();
            if (accountlist != null)
            {
                tellersAccountsDetails = accountlist;
            }
            TellersAccountsViewModel tellersAccountsViewModel = new TellersAccountsViewModel();
            tellersAccountsViewModel.TellerAccount = tellersAccountsDetails;
            return tellersAccountsViewModel;
        }

        public void GetTellerProperties(GlAccountsManager glaccounts, SingleTellerViewModel singleTellerViewModel)
        {
            singleTellerViewModel.TellerAccounts = new List<System.Web.Mvc.SelectListItem>();
            singleTellerViewModel.Branches = mainDb.BranchSettings.Select(x =>
                new SelectListItem { Value = x.BranchCode, Text = x.Name, Selected = false }).ToList();
            singleTellerViewModel.GlAccounts = glaccounts.GlAccountsTrimmed(mainDb).ToList();
        }

        public SingleTellerViewModel GetSingleTeller(Guid? id, GlAccountsManager glAccountsManager)
        {
            SingleTellerViewModel singleTellerViewModel = new SingleTellerViewModel();
            GetTellerProperties(glAccountsManager, singleTellerViewModel);

            singleTellerViewModel.SingleTellerRecord = (from accounts in mainDb.tbl_TellerAccounts
                                                        join users in mainDb.tbl_users on accounts.LoginCode equals users.LoginCode into tempAccount
                                                        from user in tempAccount.DefaultIfEmpty()
                                                        where accounts.tbl_TellerAccountsID == id
                                                        select new SingleTellerRecord
                                                        {
                                                            TellerAccountsID = accounts.tbl_TellerAccountsID,
                                                            TellerAccountNo = accounts.TellerAccountNo.Trim(),
                                                            AuthorisedBranch = string.Empty,
                                                            UserRecordId = (user == null ? Guid.Empty : user.tbl_usersID),
                                                            LoginCode = (user == null ? string.Empty : user.FullName),
                                                            ShortageAccount = (user == null ? string.Empty :"" ),
                                                            ExcessAcount = (user == null ? string.Empty : ""),
                                                        }).FirstOrDefault();

            return singleTellerViewModel;
        }

        public List<UserRoleAssignment> GetOtherUserRoles(Guid? id)
        {
            List<UserRoleAssignment> userOtherRoles = new List<UserRoleAssignment>();

            //Select * roles
            var allRoles = this.GetUserRoles();

            //Select the Assigned roles.
            if (id != null && id != Guid.Empty)
            {
                var assignedRoles = mainDb.tbl_users.Where(x => x.tbl_usersID == id).Select(x => x.OtherUserRoles).FirstOrDefault();

                //Split the roles
                foreach (var userright in assignedRoles.Split(':'))
                {
                    var singleRole = allRoles.FirstOrDefault(x => x.RoleCode == userright);

                    userOtherRoles.Add(new UserRoleAssignment
                    {
                        Checked = true,
                        RoleCode = userright,
                        RoleName = (singleRole == null ? string.Empty : singleRole.RoleName)
                    });
                }

                //sort them assigned and not assigned
                var notAssigned = (
                    from role in allRoles
                    join assigned in userOtherRoles on role.RoleCode equals assigned.RoleCode into temp
                    from exists in temp.DefaultIfEmpty()
                    where exists == null
                    select new UserRoleAssignment
                    {
                        Checked = false,
                        RoleCode = role.RoleCode,
                        RoleName = role.RoleName
                    });

                userOtherRoles.AddRange(notAssigned);
            }
            return userOtherRoles;
        }

        public bool AssignRolesToUSer(Guid? userId, string newJoinedRoles, string primaryRole)
        {
            bool updateResult = false;

            if (!String.IsNullOrEmpty(userId.ToString()))
            {
                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                string updateUserRole = "UPDATE tbl_users SET UserRole = '" + ValueConverters.format_sql_string(primaryRole) + "', OtherUserRoles = '" + ValueConverters.format_sql_string(newJoinedRoles) + "' where tbl_usersID = '" + userId + "';";

                transEngine.Generate_Sql_Statement(transList, transactionID, updateUserRole);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);

                updateResult = results == SessionVariables.TransactionPostedSuccessfully;
            }

            return updateResult;
        }

        public bool AssignRolesToUSer(Guid? userId, string newJoinedRights)
        {
            bool updateResult = false;

            if (!String.IsNullOrEmpty(userId.ToString()))
            {
                List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
                string transactionID = ValueConverters.RandomString(10);
                PostTransactions transEngine = new PostTransactions();

                string updateUserRole = "UPDATE tbl_users SET AccessRights = '" + ValueConverters.format_sql_string(newJoinedRights) + "' WHERE tbl_usersID = '" + userId + "';";

                transEngine.Generate_Sql_Statement(transList, transactionID, updateUserRole);

                string results = transEngine.Post_Transactions(transList, transactionID, false, false);

                updateResult = results == SessionVariables.TransactionPostedSuccessfully;
            }

            return updateResult;
        }

        public UserAdministrationViewModel NewUser(UserAdministrationViewModel userAdministrationViewModel)
        {            
            userAdministrationViewModel.userRoles = GetUserRoles();
            userAdministrationViewModel.user = new tbl_users();

            return userAdministrationViewModel;
        }

        public ReportViewer SystemUsersReport()
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            List<UserDetailsView> users = GetAllUsers();

            viewer.LocalReport.ReportPath = "Reports/SystemSettings/SystemUsersReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsSystemUsers", users));

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public ReportViewer SystemUSerReport(Guid? userId)
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            List<UserDetailsView> user = GetAllUsers().Where(x => x.tbl_usersID == userId).ToList();


            viewer.LocalReport.ReportPath = "Reports/SystemSettings/SystemUserReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsSystemUser", user));

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public ReportViewer UserRolesReport()
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            List<UserRole> userRoles = GetUserRoles();

            viewer.LocalReport.ReportPath = "Reports/SystemSettings/UserRolesReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsUserRoles", userRoles));

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public List<UserRole> DsSingleUserRole(UserRolesViewModel userRoleViewModel)
        {
            //convert to list
            List<UserRole> singleUserRole = new List<UserRole>();
            singleUserRole.Add(userRoleViewModel.SingleUserRole);
            return singleUserRole;
        }

        public List<UserRightsAssignment> DsroleRights(UserRolesViewModel userRoleViewModel)
        {
            List<UserRightsAssignment> roleRights = (from checkedRights in userRoleViewModel.RoleRights where checkedRights.Checked == true select checkedRights).ToList();

            return roleRights;
        }

        public ReportViewer SingleRoleRightsReport(Guid? roleId)
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            UserRolesViewModel userRoleViewModel = new UserRolesViewModel();
            userRoleViewModel = GetUserRoleDetails(roleId, userRoleViewModel);


            List<UserRightsAssignment> roleRights = DsroleRights(userRoleViewModel);
            List<UserRole> singleUserRole = DsSingleUserRole(userRoleViewModel);


            viewer.LocalReport.ReportPath = "Reports/SystemSettings/SingleRoleRightsReport.rdlc";

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsSingleUserRole", singleUserRole));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsRoleRights", roleRights));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }


        public List<tbl_users> DsSingleUser(AssignRightsToUserViewModel assignRightsToUserViewModel)
        {

            List<tbl_users> currentUser = new List<tbl_users>();
            currentUser.Add(assignRightsToUserViewModel.CurrentUser);
            return currentUser;
        }

        public List<UserRightsAssignment> DsUserRights(AssignRightsToUserViewModel assignRightsToUserViewModel)
        {
            List<UserRightsAssignment> roleRights = (from checkedRights in assignRightsToUserViewModel.RoleRights where checkedRights.Checked == true select checkedRights).ToList();

            return roleRights;
        }

        public ReportViewer UserRightsReport(Guid? userId)
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            AssignRightsToUserViewModel assignRightsToUserViewModel = new AssignRightsToUserViewModel();

            assignRightsToUserViewModel = GetUserRightsDetails(userId, assignRightsToUserViewModel);


            List<tbl_users> singleUser = DsSingleUser(assignRightsToUserViewModel);
            List<UserRightsAssignment> userRights = DsUserRights(assignRightsToUserViewModel);


            viewer.LocalReport.ReportPath = "Reports/SystemSettings/UserRightsReport.rdlc";

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsSingleUser", singleUser));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsUserRights", userRights));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public List<tbl_users> DsSingleRolesUser(AssignRoleToUserViewModel assignRoleToUserViewModel)
        {
            //convert to list
            List<tbl_users> singleUserRole = new List<tbl_users>();
            singleUserRole.Add(assignRoleToUserViewModel.user);
            return singleUserRole;

        }

        public List<UserRoleAssignment> DsuserPrimaryRoles(AssignRoleToUserViewModel assignRoleToUserViewModel)
        {
            List<UserRoleAssignment> primaryRole = new List<UserRoleAssignment>();


            if (!String.IsNullOrEmpty(assignRoleToUserViewModel.user.UserRole))
            {
                string userRole = assignRoleToUserViewModel.user.UserRole;
                List<UserRoleAssignment> existingRoles = assignRoleToUserViewModel.Existingroles;

                primaryRole = (from roleList in existingRoles where roleList.RoleCode == userRole select roleList).ToList();
            }

            return primaryRole;
        }

        public List<UserRoleAssignment> DsUserRoles(AssignRoleToUserViewModel assignRoleToUserViewModel)
        {
            List<UserRoleAssignment> otherRoles = new List<UserRoleAssignment>();

            otherRoles = (from checkedRoles in assignRoleToUserViewModel.Existingroles where checkedRoles.Checked == true select checkedRoles).ToList();

            return otherRoles;
        }


        public ReportViewer UserRolesReport(Guid? userId)
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            AssignRoleToUserViewModel assignRoleToUserViewModel = new AssignRoleToUserViewModel();
            assignRoleToUserViewModel.user = GetUser(userId);
            assignRoleToUserViewModel.Existingroles = GetOtherUserRoles(userId).OrderBy(x => x.RoleCode).ToList();

            List<tbl_users> singleUser = DsSingleRolesUser(assignRoleToUserViewModel);
            List<UserRoleAssignment> userRoles = DsUserRoles(assignRoleToUserViewModel);
            List<UserRoleAssignment> userPrimaryRoles = DsuserPrimaryRoles(assignRoleToUserViewModel);

            viewer.LocalReport.ReportPath = "Reports/SystemSettings/SingleUserRolesReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsSingleRoleUser", singleUser));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsUserRoles", userRoles));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsPrimaryRole", userPrimaryRoles));

            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public List<PasswordSettingsViewModel> DsPasswordSettings(PasswordSettingsViewModel passwordSettings)
        {
            List<PasswordSettingsViewModel> settings = new List<PasswordSettingsViewModel>();

            passwordSettings = GetUserPasswordSettings();

            settings.Add(passwordSettings);

            return settings;

        }

        public ReportViewer UserPasswordSettingsReport()
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            PasswordSettingsViewModel passwordSettings = new PasswordSettingsViewModel();
            List<PasswordSettingsViewModel> userPasswordSettings = DsPasswordSettings(passwordSettings);

            viewer.LocalReport.ReportPath = "Reports/SystemSettings/PasswordSettingsReport.rdlc";

            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsPasswordSettings", userPasswordSettings));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public ReportViewer TellerAccountsReport()
        {
            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            List<TellersAccountsDetails> tellers = DsTellerAccounts();

            viewer.LocalReport.ReportPath = "Reports/SystemSettings/TellerAccountsReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsTellerAccounts", tellers));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1 });
            return viewer;
        }

        public List<TellersAccountsDetails> DsTellerAccounts()
        {
            //List<TellersAccountsViewModel> tellerAccountsList = new List<TellersAccountsViewModel>();

            TellersAccountsViewModel tellersAccounts = GetTellersAccounts();

            return tellersAccounts.TellerAccount;
        }



        public ReportViewer SingleTellerAccountsReport(Guid? tellerID)
        {
            GlAccountsManager glAccountsManager = new GlAccountsManager();
            SingleTellerViewModel singleTellerViewModel = GetSingleTeller(tellerID, glAccountsManager);
            singleTellerViewModel.SingleTellerRecord.AuthorisedBranch = singleTellerViewModel.Branches.Where(x => x.Value == singleTellerViewModel.SingleTellerRecord.AuthorisedBranch).Select(x => x.Text).FirstOrDefault();
            singleTellerViewModel.SingleTellerRecord.ShortageAccount = singleTellerViewModel.GlAccounts.Where(x => x.GlAccountNo == singleTellerViewModel.SingleTellerRecord.ShortageAccount).Select(x => x.GlName).FirstOrDefault();
            singleTellerViewModel.SingleTellerRecord.ExcessAcount = singleTellerViewModel.GlAccounts.Where(x => x.GlAccountNo == singleTellerViewModel.SingleTellerRecord.ExcessAcount).Select(x => x.GlName).FirstOrDefault();
            singleTellerViewModel.SingleTellerRecord.TellerAccountNo = singleTellerViewModel.GlAccounts.Where(x => x.GlAccountNo == singleTellerViewModel.SingleTellerRecord.TellerAccountNo).Select(x => x.GlName).FirstOrDefault();

            List<Company> company = mainDb.Company.ToList();
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;


            viewer.LocalReport.ReportPath = "Reports/SystemSettings/SingleTellerAccountReport.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            ReportParameter param1 = new ReportParameter("paramUserName", UserSession.Current.userDetails.LoginName);
            ReportParameter param2 = new ReportParameter("LoginName", singleTellerViewModel.SingleTellerRecord.LoginCode);
            ReportParameter param3 = new ReportParameter("TellerAccountNo", singleTellerViewModel.SingleTellerRecord.TellerAccountNo);
            ReportParameter param4 = new ReportParameter("AuthorisedBranch", singleTellerViewModel.SingleTellerRecord.AuthorisedBranch);
            ReportParameter param5 = new ReportParameter("ExcessAcount", singleTellerViewModel.SingleTellerRecord.ExcessAcount);
            ReportParameter param6 = new ReportParameter("ShortageAccount", singleTellerViewModel.SingleTellerRecord.ShortageAccount);
            viewer.LocalReport.SetParameters(new ReportParameter[] { param1, param2, param3, param4, param5, param6 });
            return viewer;
        }


    }

    
}