using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Accounts
{
    public class GlAccountsManager
    {
        //ToDo: Add Gl Statement
        private AuditTrail auditTrail = new AuditTrail();
        private Esoft_EstateEntities mainDb = new Esoft_EstateEntities();
        private IValidationDictionary _validationDictionary;
        public GlAccountsManager()
        {
            // Ctor to do
        }
        public GlAccountsManager(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }
        public string AddGlAccount(tbl_GlAccounts glaccounts, Esoft_EstateEntities db)
        {

            string result = "";
            if (glaccounts != null && CheckExistence(glaccounts, db))
            {
                glaccounts.tbl_GlAccountsID = GuidGenerator.NewComb();
                db.tbl_GlAccounts.Add(glaccounts);

                string msg = "Added New Gl Account " + ValueConverters.ConvertNullToEmptyString(glaccounts.GlAccountNo) + " Name " + ValueConverters.ConvertNullToEmptyString(glaccounts.GlName);

                auditTrail.CreateTrailRecord(db, UserSession.Current.userDetails.LoginCode, msg, 0, "GLACCOUNTS", "6001A", false);

                db.SaveChanges();
                GetCached(db);
            }
            else
            {
                result = "Gl Account Already Used By " + ValueConverters.ConvertNullToEmptyString(
                    db.tbl_GlAccounts.FirstOrDefault(x => x.GlAccountNo == glaccounts.GlAccountNo).GlName);
            }

            return result;
        }

        public bool UpdateGlAccount(tbl_GlAccounts glaccounts, Esoft_EstateEntities db)
        {
            bool success = true;
            if (glaccounts != null && CheckExistence(glaccounts, db))
            {
                db.Entry(glaccounts).State = EntityState.Modified;

                string msg = "Amend  Gl Account No.: " + ValueConverters.ConvertNullToEmptyString(glaccounts.GlAccountNo) + " Name " + ValueConverters.ConvertNullToEmptyString(glaccounts.GlName);

                auditTrail.CreateTrailRecord(db, UserSession.Current.userDetails.LoginCode, msg, 0, "GLACCOUNTS", "6001A", false);

                db.SaveChanges();
                GetCached(db);
            }
            else
            {
                success = false;
            }
            return success;

        }

        public bool CheckExistence(tbl_GlAccounts glaccounts, Esoft_EstateEntities db)
        {
            return !(db.tbl_GlAccounts.Any((x => x.tbl_GlAccountsID != glaccounts.tbl_GlAccountsID && x.GlAccountNo == glaccounts.GlAccountNo)));
        }

        public IEnumerable<GlAccountsView> GlAccounts(Esoft_EstateEntities db)
        {
            List<GlAccountsView> GlRecords = GetCached(mainDb);

            var returnList = new List<GlAccountsView>();

            foreach (var item in GlRecords)
            {
                returnList.Add(
                    new GlAccountsView
                    {
                        GlAccountNo = item.GlAccountNo,
                        GlName = item.GlAccountNo + ": " + item.GlName.Trim()
                    });
            }
            return returnList.ToList();
        }

        public IEnumerable<GlAccountsView> GlAccountsTrimmed(Esoft_EstateEntities db)
        {
            List<GlAccountsView> GlRecords = GetCachedTrimmed(mainDb);

            var returnList = new List<GlAccountsView>();

            foreach (var item in GlRecords)
            {
                returnList.Add(
                    new GlAccountsView
                    {
                        GlAccountNo = item.GlAccountNo.Trim(),
                        GlName = item.GlAccountNo.Trim() + ": " + item.GlName.Trim()
                    });
            }
            return returnList.ToList();
        }

        private static List<GlAccountsView> GetCached(Esoft_EstateEntities db)
        {
            const string glAccounts_cacheKey = "GlAccounts_cache";
            List<GlAccountsView> data = CachingProvider.Get<List<GlAccountsView>>(glAccounts_cacheKey);
            if (data == null)
            {
                data = (from gl in db.tbl_GlAccounts
                        select new GlAccountsView
                        {
                            GlAccountNo = gl.GlAccountNo,
                            GlName = gl.GlName.Trim(),
                            AccountBlocked = (gl.Account_Blocked == null ? false : gl.Account_Blocked)
                        }).OrderBy(p => p.GlAccountNo).ToList();

                CachingProvider.Add(data, glAccounts_cacheKey);
            }
            return data;
        }

        private static List<GlAccountsView> GetCachedTrimmed(Esoft_EstateEntities db)
        {
            const string glAccounts_cacheKeyTrimmed = "GlAccounts_cache_trimmed";
            List<GlAccountsView> data = CachingProvider.Get<List<GlAccountsView>>(glAccounts_cacheKeyTrimmed);
            if (data == null)
            {
                data = (from gl in db.tbl_GlAccounts
                        select new GlAccountsView
                        {
                            GlAccountNo = gl.GlAccountNo.Trim(),
                            GlName = gl.GlName.Trim(),
                            AccountBlocked = (gl.Account_Blocked == null ? false : gl.Account_Blocked)
                        }).OrderBy(p => p.GlAccountNo).ToList();

                CachingProvider.Add(data, glAccounts_cacheKeyTrimmed);
            }
            return data;
        }
        public List<GlAccountsView> GetAccountDetails(string accountNo, bool fetchBalance)
        {
            List<GlAccountsView> glaccount = new List<GlAccountsView>();
            if (fetchBalance)
            {
                glaccount.Add(new GlAccountsView
                {
                    GlAccountNo = accountNo,
                    GlName = GlName(accountNo),
                    GlBalance = GetGlBalance(accountNo)
                });
            }
            else
            {
                glaccount.Add(new GlAccountsView
                {
                    GlAccountNo = accountNo,
                    GlName = GlName(accountNo),
                    GlBalance = 0.00
                });
            }
            return glaccount;
        }

        public double GetGlBalance(string accountNo)
        {
            double glBalance = 0.00;
            try
            {
                string query = "Exec GetGlBalance '" + ValueConverters.format_sql_string(accountNo) + "'";
                DbDataReader reader = DbConnector.GetSqlReader(query);
                while (reader.Read())
                {
                    glBalance = ValueConverters.StringtoDouble(reader["balance"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ref ex);
            }

            return glBalance;
        }

        public List<GlAccountsView> GetModuleTransactingAccounts(List<GlAccountsView> glview, string trType, string userBranch)
        {
            switch (trType.ToUpper())
            {
                case "MISCRECEIPTS":
                    glview = GetCategorisedAccounts("MISC.RCPTS", userBranch);
                    break;
                case "MISCPAYMENTS":
                    glview = GetCategorisedAccounts("MISC.PYMTS", userBranch);
                    break;
                case "SALARYCHEQUES":
                    glview = GetCategorisedAccounts("SAL.CHQS", userBranch);
                    break;
                case "AGENCYDEPOSIT":
                    glview = GetCategorisedAccounts("MPESA", userBranch);
                    break;
                case "AGENCYPAYMENT":
                    glview = GetCategorisedAccounts("MPESA", userBranch);
                    break;
                case "ORDINARYCHEQUES":
                    glview = GetCategorisedAccounts("ORD.CHEQUE", userBranch);
                    break;
                case "BANKERSCHEQUES":
                    glview = GetCategorisedAccounts("BNKS.CHQS", userBranch);
                    break;
                default:
                    glview = GetCategorisedAccounts(trType.ToUpper(), userBranch);
                    break;
            }
            return glview;
        }

        private List<GlAccountsView> GetCategorisedAccounts(string accountCategory, string currentUserBranch = null)
        {
           List<GlAccountsView> glaccounts = new List<GlAccountsView>();

            //    List<GlAccountsView> GlRecords = GetCached(mainDb);

            //    var categorisedAccounts = mainDb.tbl_GlAccounts_Categorised.Where(x => x.Category == accountCategory && x.BlockPosting == false).OrderBy(x => x.GlAccountNo).ToList();

            //    foreach (var item in categorisedAccounts)
            //    {
            //        if (currentUserBranch == null || (currentUserBranch != null && (item.BranchCode == currentUserBranch || item.BranchCode == null)))
            //        {
            //            var singleGlAccount = GlRecords.FirstOrDefault(x => x.GlAccountNo == item.GlAccountNo);
            //            if (singleGlAccount == null)
            //            {
            //                singleGlAccount = GlRecords.FirstOrDefault(x => x.GlAccountNo.Trim() == item.GlAccountNo.Trim());
            //            }
            //            if (singleGlAccount != null && singleGlAccount.AccountBlocked == false)
            //            {
            //                glaccounts.Add(new GlAccountsView
            //                {
            //                    GlAccountNo = item.GlAccountNo.Trim(),
            //                    GlName = item.GlAccountNo + " " + GlName(item.GlAccountNo),
            //                    GlBalance = 0.00
            //                });
            //            }
            //        }
            //    }

            return glaccounts;
        }


        public string GlName(string glAccountNo)
        {
            List<GlAccountsView> GlRecords = GetCached(mainDb);
            var singleGlAccount = GlRecords.FirstOrDefault(x => x.GlAccountNo == glAccountNo.Trim());
            if (singleGlAccount == null)
            {
                singleGlAccount = GlRecords.FirstOrDefault(x => x.GlAccountNo.Trim() == glAccountNo.Trim());
            }
            if (singleGlAccount == null)
            {
                singleGlAccount = GlRecords.FirstOrDefault(x => x.GlAccountNo == glAccountNo);
            }

            string glname = string.Empty;
            if (singleGlAccount != null) glname = singleGlAccount.GlName;

            return
                (

                ValueConverters.ConvertNullToEmptyString(glname)
                );
        }

        public DailyAccountsBalances GetDailyAccountsBalances(string accountNo, DateTime dateToday)
        {

            string sqlstring = "Exec GetGlBalance_Daily '" + ValueConverters.format_sql_string(accountNo) + "','" + ValueConverters.FormatSqlDate(dateToday, true) + "'";
            DailyAccountsBalances dailyAccBalance = new DailyAccountsBalances();
            dailyAccBalance.AccountNo = accountNo;
            try
            {
                DbDataReader dailyBalance = DbConnector.GetSqlReader(sqlstring);
                while (dailyBalance.Read())
                {
                    dailyAccBalance.Deposits = ValueConverters.StringtoDouble(dailyBalance["Deposits"].ToString());
                    dailyAccBalance.Withdrawals = ValueConverters.StringtoDouble(dailyBalance["Withdrawals"].ToString());
                    dailyAccBalance.PrevBal = ValueConverters.StringtoDouble(dailyBalance["PrevBal"].ToString());
                }
                dailyBalance.Close();
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ref ex);
            }
            return dailyAccBalance;
        }

        public SingleGlReportPerBranchViewModel GlAccountStatementBranchWise(SingleGlReportPerBranchViewModel glReportViewModel)
        {
            SingleGlReportPerBranchViewModel singleGl = new SingleGlReportPerBranchViewModel();
            try
            {
                glReportViewModel = glReportViewModel == null ? new SingleGlReportPerBranchViewModel() : glReportViewModel;

                if (glReportViewModel.StartDate == null || glReportViewModel.EndDate == null)
                {
                    singleGl.LedgerAccounts = GlAccounts(mainDb).ToList();
                    singleGl.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
                    singleGl.listLedgerRecord = new List<SingleGlReportRecord>();
                }
                else
                {

                    string sqlstatement = "Exec Ledgerstate_Single_Account_br @GlAccountNo='" + ValueConverters.format_sql_string(glReportViewModel.GlAccountNo)
                        + "', @begdate='" + ValueConverters.FormatSqlDate(glReportViewModel.StartDate) +
                        "', @enddate='" + ValueConverters.FormatSqlDate(glReportViewModel.EndDate, true) + "', @BranchCode='" + glReportViewModel.SelectedBranchCode + "'";
                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);
                    singleGl.listLedgerRecord = Functions.DataReaderMapToList<SingleGlReportRecord>(reader);
                    reader.NextResult();
                    decimal openingBalance = 0;
                    while (reader.Read())
                    {
                        openingBalance = ValueConverters.StringtoDecimal(reader["Balance"].ToString());
                    }



                    //GetOpeningBalance



                    DateTime startDate = (DateTime)glReportViewModel.StartDate;
                    singleGl.listLedgerRecord.Add(new SingleGlReportRecord
                    {
                        TransactionDate = startDate,
                        Narration = "Balance as At " + startDate.ToString("dd/MM/yyyy"),
                        DebitAmount = openingBalance >= 0 ? openingBalance : 0,
                        CreditAmount = openingBalance >= 0 ? 0 : Math.Abs(openingBalance),
                        balance = openingBalance
                    });

                    int count = 0;
                    decimal balance = 0; decimal runningtotal = 0; decimal newbal = 0;
                    decimal debitTotal = 0; decimal creditTotal = 0;
                    singleGl.listLedgerRecord = singleGl.listLedgerRecord.OrderBy(x => x.TransactionDate).ToList();
                    foreach (var item in singleGl.listLedgerRecord)
                    {
                        newbal = newbal + item.DebitAmount - item.CreditAmount;
                        debitTotal += singleGl.listLedgerRecord[count].DebitAmount;
                        creditTotal += singleGl.listLedgerRecord[count].CreditAmount;
                        singleGl.listLedgerRecord[count].balance = newbal;

                        count++;
                    }
                    singleGl.GLCreditAmount = creditTotal;
                    singleGl.GLDebitAmount = debitTotal;
                    singleGl.GLBalance = debitTotal - creditTotal;

                }
                singleGl.LedgerAccounts = GlAccounts(mainDb).ToList();
                singleGl.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
            }
            catch (Exception ex)
            {
                singleGl.LedgerAccounts = new List<GlAccountsView>();
                singleGl.BranchList = new List<BranchSettings>();
                Utility.WriteErrorLog("SingleGlRecord", ref ex);

            }
            return singleGl;
        }

        public SingleGlReportPerBranchViewModel GlAccountStatement(SingleGlReportPerBranchViewModel glReportViewModel)
        {
            String glName = string.Empty;

            SingleGlReportPerBranchViewModel singleGl = new SingleGlReportPerBranchViewModel
            {
                GlAccountNo = glReportViewModel.GlAccountNo,
                GlAccountName = GlAccountsTrimmed(mainDb).ToList().Where(x => x.GlAccountNo == glReportViewModel.GlAccountNo).FirstOrDefault().GlName,
            };
            try
            {
                glReportViewModel = glReportViewModel == null ? new SingleGlReportPerBranchViewModel() : glReportViewModel;

                if (glReportViewModel.StartDate == null || glReportViewModel.EndDate == null)
                {
                    singleGl.LedgerAccounts = GlAccounts(mainDb).ToList();
                    singleGl.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
                    singleGl.listLedgerRecord = new List<SingleGlReportRecord>();
                }
                else
                {

                    string sqlstatement = "Exec Ledgerstate_Single_Account @GlAccountNo='" + ValueConverters.format_sql_string(glReportViewModel.GlAccountNo)
                        + "', @begdate='" + ValueConverters.FormatSqlDate(glReportViewModel.StartDate) +
                        "', @enddate='" + ValueConverters.FormatSqlDate(glReportViewModel.EndDate, true) + "'";

                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);
                    decimal openingBalance = 0;
                    while (reader.Read())
                    {
                        openingBalance = ValueConverters.StringtoDecimal(reader["Balance"].ToString());
                    }

                    reader.NextResult();
                    singleGl.listLedgerRecord = Functions.DataReaderMapToList<SingleGlReportRecord>(reader);



                    DateTime startDate = (DateTime)glReportViewModel.StartDate;
                    singleGl.listLedgerRecord.Add(new SingleGlReportRecord
                    {
                        TransactionDate = startDate,
                        Narration = "Balance as At " + startDate.ToString("dd/MM/yyyy"),
                        DebitAmount = openingBalance >= 0 ? openingBalance : 0,
                        CreditAmount = openingBalance >= 0 ? 0 : Math.Abs(openingBalance)
                    });

                    singleGl.listLedgerRecord = singleGl.listLedgerRecord.OrderBy(x => x.TransactionDate).ToList();

                    int count = 0;
                    decimal balance = 0; decimal runningtotal = 0; decimal newbal = 0;
                    decimal debitTotal = 0; decimal creditTotal = 0;

                    foreach (var item in singleGl.listLedgerRecord)
                    {
                        newbal = newbal + item.DebitAmount - item.CreditAmount;
                        // runningtotal = runningtotal - (newbal);
                        singleGl.listLedgerRecord[count].balance = newbal;
                        debitTotal += singleGl.listLedgerRecord[count].DebitAmount;
                        creditTotal += singleGl.listLedgerRecord[count].CreditAmount;
                        count++;
                    }
                    singleGl.GLCreditAmount = creditTotal;
                    singleGl.GLDebitAmount = debitTotal;
                    singleGl.GLBalance = debitTotal - creditTotal;

                }
                singleGl.LedgerAccounts = GlAccounts(mainDb).ToList();
                singleGl.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
            }
            catch (Exception ex)
            {
                singleGl.LedgerAccounts = new List<GlAccountsView>();
                singleGl.BranchList = new List<BranchSettings>();
                Utility.WriteErrorLog("SingleGlRecord", ref ex);
            }
            return singleGl;
        }

        public TransactionTrailViewModel GetGlAuditTrail(TransactionTrailViewModel glReportViewModel)
        {
            TransactionTrailViewModel transactionTrailViewModel = new TransactionTrailViewModel();
            transactionTrailViewModel.TransactionTrail = new List<GeneralLedgerTransactionTrail>();
            try
            {
                glReportViewModel = glReportViewModel == null ? new TransactionTrailViewModel() : glReportViewModel;
                if (glReportViewModel.StartDate == null || glReportViewModel.EndDate == null)
                {
                    glReportViewModel.StartDate = DateTime.Now.Date;
                    glReportViewModel.EndDate = DateTime.Now.Date;
                }
                else
                {
                    decimal debitTotal = 0; decimal creditTotal = 0;
                    glReportViewModel.EndDate = ValueConverters.DayEndToday(glReportViewModel.EndDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */
                    var transactAudit = (from d in mainDb.tbl_Ledger
                                         join ac in mainDb.tbl_GlAccounts on d.GlAccountNo equals ac.GlAccountNo into glNames
                                         join cust in mainDb.tbl_users on d.LoginCode equals cust.LoginCode into userNames
                                         from glName in glNames.DefaultIfEmpty()
                                         from userName in userNames.DefaultIfEmpty()
                                         where d.TransactionDate >= glReportViewModel.StartDate && d.TransactionDate <= glReportViewModel.EndDate
                                         select new GeneralLedgerTransactionTrail
                                         {
                                             AccountRef = d.AccountNo,                                           
                                             CreditAmount = d.CreditAmount,
                                             DebitAmount = d.DebitAmount,
                                             GlAccountNo = d.GlAccountNo,
                                             GlAccount = d.GlAccountNo,
                                             GlName = (glName == null ? string.Empty : glName.GlName),
                                             TransactionDate = d.TransactionDate,
                                             Narration = d.Narration,
                                             ReferenceNo = d.ReferenceNo,
                                             UserName = (userName == null ? string.Empty : userName.LoginName),
                                         }).OrderBy(x => x.TransactionDate).ToList();

                    glReportViewModel.EndDate = glReportViewModel.EndDate.Value.Date;

                    transactionTrailViewModel.TransactionTrail = transactAudit;
                    int count = 0;
                    foreach (var item in transactionTrailViewModel.TransactionTrail.OrderBy(x => x.TransactionDate))
                    {

                        debitTotal += transactionTrailViewModel.TransactionTrail[count].DebitAmount;
                        creditTotal += transactionTrailViewModel.TransactionTrail[count].CreditAmount;
                        count++;
                    }
                    transactionTrailViewModel.CreditTotal = creditTotal;
                    transactionTrailViewModel.DebitTotal = debitTotal;
                    transactionTrailViewModel.DiffAmount = debitTotal - creditTotal;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetGlAuditTrail", ref ex);
            }
            return transactionTrailViewModel;
        }


        public AuditTrailViewModel GetSocietyTrialBal(AuditTrailViewModel glReportViewModel)
        {
            AuditTrailViewModel transactionTrailViewModel = new AuditTrailViewModel();
            transactionTrailViewModel.TransactionTrail = new List<GeneralLedgerTransactionTrail>();
            try
            {
                glReportViewModel = glReportViewModel == null ? new AuditTrailViewModel() : glReportViewModel;
                if (glReportViewModel.StartDate == null || glReportViewModel.EndDate == null)
                {
                    glReportViewModel.StartDate = DateTime.Now.Date;
                    glReportViewModel.EndDate = DateTime.Now.Date;
                }
                else
                {
                    glReportViewModel.EndDate = ValueConverters.DayEndToday(glReportViewModel.EndDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */

                    glReportViewModel.EndDate = glReportViewModel.EndDate.Value.Date;

                    string sqlstatement = "Exec TrialBalance @Date1='" + ValueConverters.FormatSqlDate(glReportViewModel.StartDate) + "',@date2='" +
                        ValueConverters.FormatSqlDate(glReportViewModel.EndDate, true) + "'";
                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);
                    //variables for calculations

                    reader.NextResult();

                    decimal debitTotal = 0;
                    decimal creditTotal = 0;
                    decimal diffAmount = 0;


                    while (reader.Read())
                    {
                        var trailObj = new GeneralLedgerTransactionTrail();
                        //evaluate the balance
                        if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) > 0)
                        {
                            //positive values
                            trailObj.CreditAmount = 0;
                            trailObj.DebitAmount = ValueConverters.StringtoDecimal(reader["balance"].ToString());

                        }
                        else if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) < 0)
                        {
                            //negative values
                            trailObj.CreditAmount = (decimal)reader["balance"];
                            trailObj.DebitAmount = 0;

                        }
                        else if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) == 0)
                        {
                            //skip
                            continue;
                        }

                        trailObj.GlAccountNo = reader["GlAccountNo"].ToString();
                        trailObj.GlName = reader["GlName"].ToString();
                        trailObj.balance = (decimal)reader["balance"];

                        //calculations
                        debitTotal += trailObj.DebitAmount;
                        creditTotal += trailObj.CreditAmount;

                        transactionTrailViewModel.TransactionTrail.Add(trailObj);
                    }

                    diffAmount = debitTotal + creditTotal; //confirm
                    transactionTrailViewModel.DebitTotal = debitTotal;
                    transactionTrailViewModel.CreditTotal = creditTotal;
                    transactionTrailViewModel.DiffAmount = diffAmount;
                    // transactionTrailViewModel.TransactionTrail = singleTrail;



                }

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetSocietyTrialBal", ref ex);
            }
            return transactionTrailViewModel;
        }

        public AuditTrailViewModel GetBranchTrialBal(AuditTrailViewModel glReportViewModel)
        {
            AuditTrailViewModel transactionTrailViewModel = new AuditTrailViewModel();
            transactionTrailViewModel.TransactionTrail = new List<GeneralLedgerTransactionTrail>();
            try
            {
                glReportViewModel = glReportViewModel == null ? new AuditTrailViewModel() : glReportViewModel;
                if (glReportViewModel.StartDate == null || glReportViewModel.EndDate == null)
                {
                    glReportViewModel.StartDate = DateTime.Now.Date;
                    glReportViewModel.EndDate = DateTime.Now.Date;
                    glReportViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
                }
                else
                {
                    glReportViewModel.EndDate = ValueConverters.DayEndToday(glReportViewModel.EndDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */

                    glReportViewModel.EndDate = glReportViewModel.EndDate.Value.Date;
                    string sqlstatement = "Exec TrialBalance_BranchWise @Date1='"
                        + ValueConverters.FormatSqlDate(glReportViewModel.StartDate) +
                        "', @date2='" + ValueConverters.FormatSqlDate(glReportViewModel.EndDate, true) + "', @BranchCode='" + glReportViewModel.SelectedBranchCode + "'";
                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);

                    reader.NextResult();

                    //new code

                    //variables for calculations
                    decimal debitTotal = 0;
                    decimal creditTotal = 0;
                    decimal diffAmount = 0;


                    while (reader.Read())
                    {
                        var trailObj = new GeneralLedgerTransactionTrail();
                        //evaluate the balance
                        if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) > 0)
                        {
                            //positive values
                            trailObj.CreditAmount = 0;
                            trailObj.DebitAmount = ValueConverters.StringtoDecimal(reader["balance"].ToString());

                        }
                        else if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) < 0)
                        {
                            //negative values
                            trailObj.CreditAmount = (decimal)reader["balance"];
                            trailObj.DebitAmount = 0;

                        }
                        else if (ValueConverters.StringtoDecimal(reader["balance"].ToString()) == 0)
                        {
                            //skip
                            continue;
                        }

                        trailObj.GlAccountNo = reader["GlAccountNo"].ToString();
                        trailObj.GlName = reader["GlName"].ToString();
                        trailObj.balance = (decimal)reader["balance"];

                        //calculations
                        debitTotal += trailObj.DebitAmount;
                        creditTotal += trailObj.CreditAmount;

                        transactionTrailViewModel.TransactionTrail.Add(trailObj);
                    }

                    diffAmount = debitTotal + creditTotal; //confirm
                    transactionTrailViewModel.DebitTotal = debitTotal;
                    transactionTrailViewModel.CreditTotal = creditTotal;
                    transactionTrailViewModel.DiffAmount = diffAmount;



                    //   transactionTrailViewModel.TransactionTrail = Functions.DataReaderMapToList<GeneralLedgerTransactionTrail>(reader);

                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetBranchTrialBal", ref ex);
            }
            transactionTrailViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
            return transactionTrailViewModel;
        }


        public StatementViewModel GetStatement(StatementViewModel bsReportViewModel, bool isBranchCode)
        {

            StatementViewModel statementViewModel = new StatementViewModel();
            statementViewModel.statementList = new List<IncomeExpense>();
            try
            {
                bsReportViewModel = bsReportViewModel == null ? new StatementViewModel() : bsReportViewModel;
                if (bsReportViewModel.StartDate == null)
                {
                    bsReportViewModel.StartDate = DateTime.Now.Date;
                    bsReportViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
                }
                else
                {
                    bsReportViewModel.StartDate = ValueConverters.DayEndToday(bsReportViewModel.StartDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */

                    bsReportViewModel.StartDate = bsReportViewModel.StartDate.Value.Date;

                    /*evaluate boolean variable passed*/
                    string sqlstatement = string.Empty;
                    if (isBranchCode == true)
                    {
                        sqlstatement = "Exec IncomeExpenditureReport @endDate='"
                           + ValueConverters.FormatSqlDate(bsReportViewModel.StartDate) +
                           "', @BranchCode='" + bsReportViewModel.SelectedBranchCode + "'";
                    }
                    else
                    {
                        sqlstatement = "Exec IncomeExpenditureReport @endDate='"
                               + ValueConverters.FormatSqlDate(bsReportViewModel.StartDate) + "'";
                    }

                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);


                    //variables for calculations
                    decimal variance = 0;


                    while (reader.Read())
                    {
                        var trailObj = new IncomeExpense();
                        //evaluate the balance

                        trailObj.GlAccountNo = reader["GlAccountNo"].ToString();
                        trailObj.GlAccountName = reader["GlName"].ToString();
                        trailObj.PrevBalance = (decimal)reader["PrevBalance"];
                        trailObj.Month1 = (decimal)reader["Month1"];
                        trailObj.Month2 = (decimal)reader["Month2"];
                        trailObj.Month3 = (decimal)reader["Month3"];
                        trailObj.Budget = (decimal)reader["Budget"];
                        trailObj.AnnualBudget = (decimal)reader["AnnualBudget"];
                        //get variance
                        variance = (trailObj.PrevBalance + trailObj.Month1 + trailObj.Month2 + trailObj.Month3) - trailObj.Budget;
                        trailObj.Variance = variance;

                        //calculations


                        statementViewModel.statementList.Add(trailObj);
                    }

                    reader.NextResult();
                    StatementPeriod periodObj = new StatementPeriod();
                    while (reader.Read())
                    {

                        periodObj.Month1 = reader["Month1"].ToString();
                        periodObj.Month2 = reader["Month2"].ToString();
                        periodObj.Month3 = reader["Month3"].ToString();
                        periodObj.QuarterName = reader["QuarterName"].ToString();

                    }

                    statementViewModel.statementTimeLine = periodObj;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetStatement", ref ex);
            }
            statementViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
            return statementViewModel;
        }


        public StatementViewModel GetStatement_ToDate(StatementViewModel bsReportViewModel, bool isBranchCode)
        {

            StatementViewModel statementViewModel = new StatementViewModel();
            statementViewModel.statementList = new List<IncomeExpense>();
            try
            {
                bsReportViewModel = bsReportViewModel == null ? new StatementViewModel() : bsReportViewModel;
                if (bsReportViewModel.StartDate == null)
                {
                    bsReportViewModel.StartDate = DateTime.Now.Date;
                    bsReportViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
                }
                else
                {
                    bsReportViewModel.StartDate = ValueConverters.DayEndToday(bsReportViewModel.StartDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */
                    int monthValue = bsReportViewModel.StartDate.Value.Month;
                    bsReportViewModel.StartDate = bsReportViewModel.StartDate.Value.Date;

                    string sqlstatement = string.Empty;
                    if (isBranchCode == true)
                    {
                        sqlstatement = "Exec IncomeExpenditureReport @endDate='"
                           + ValueConverters.FormatSqlDate(bsReportViewModel.StartDate) +
                           "', @BranchCode='" + bsReportViewModel.SelectedBranchCode + "'";
                    }
                    else
                    {
                        sqlstatement = "Exec IncomeExpenditureReport @endDate='"
                               + ValueConverters.FormatSqlDate(bsReportViewModel.StartDate) + "'";
                    }
                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);


                    //variables for calculations
                    decimal variance = 0;


                    while (reader.Read())
                    {
                        var trailObj = new IncomeExpense();
                        //evaluate the balance

                        trailObj.GlAccountNo = reader["GlAccountNo"].ToString();
                        trailObj.GlAccountName = reader["GlName"].ToString();
                        trailObj.PrevBalance = (decimal)reader["PrevBalance"];
                        trailObj.Month1 = (decimal)reader["Month1"];
                        trailObj.Month2 = (decimal)reader["Month2"];
                        trailObj.Month3 = (decimal)reader["Month3"];
                        trailObj.AnnualBudget = (decimal)reader["AnnualBudget"];
                        trailObj.Budget = trailObj.AnnualBudget * (monthValue / 12);//(decimal)reader["Budget"];
                        //get variance
                        variance = (trailObj.PrevBalance + trailObj.Month1 + trailObj.Month2 + trailObj.Month3) - trailObj.Budget;
                        trailObj.Variance = variance;

                        //calculations


                        statementViewModel.statementList.Add(trailObj);
                    }

                    reader.NextResult();
                    StatementPeriod periodObj = new StatementPeriod();
                    while (reader.Read())
                    {

                        periodObj.Month1 = reader["Month1"].ToString();
                        periodObj.Month2 = reader["Month2"].ToString();
                        periodObj.Month3 = reader["Month3"].ToString();
                        periodObj.QuarterName = reader["QuarterName"].ToString();

                    }

                    statementViewModel.statementTimeLine = periodObj;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetStatement", ref ex);
            }
            statementViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();
            return statementViewModel;
        }

        public StatementViewModel GetCashBankBalance(StatementViewModel bsReportViewModel)
        {
            StatementViewModel statementViewModel = new StatementViewModel();
            statementViewModel.generalStatement = new List<CashBankBalance>();
            try
            {
                bsReportViewModel = bsReportViewModel == null ? new StatementViewModel() : bsReportViewModel;
                if (bsReportViewModel.StartDate == null)
                {
                    bsReportViewModel.StartDate = DateTime.Now.Date;
                    bsReportViewModel.BranchList = mainDb.BranchSettings.OrderBy(x => x.BranchCode).ToList();

                }
                else
                {
                    bsReportViewModel.StartDate = ValueConverters.DayEndToday(bsReportViewModel.StartDate);
                    /* Take Cares of Empty Master Table Names Same as Left Outer Join */

                    bsReportViewModel.StartDate = bsReportViewModel.StartDate.Value.Date;

                    /*evaluate boolean variable passed*/
                    string sqlstatement = string.Empty;

                    sqlstatement = "Exec cash_balance @endDate='"
                       + ValueConverters.FormatSqlDate(bsReportViewModel.StartDate) + "'";

                    DbDataReader reader = DbConnector.GetSqlReader(sqlstatement);
                    statementViewModel.generalStatement = Functions.DataReaderMapToList<CashBankBalance>(reader);

                    //variables for calculations


                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetCashStatement", ref ex);
            }

            return statementViewModel;
        }


        public List<Company> GetCompanyData()
        {
            var cinfo = mainDb.Company.ToList();
            return cinfo;
        }

        

        
        
    }

    public class DsGlAccounts
    {
        public List<tbl_GlAccounts> getGlAccounts()
        {
            return new List<tbl_GlAccounts>();
        }

    }
}