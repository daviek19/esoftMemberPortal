using EstateManagementMvc.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagementMvc.Models
{
    public class NavigationMenu
    {
        public int MenuId;
        public string MenuName;
        public string Action;
        public string Controller;
        public string Area;
        public string Icon;
        public int ParentId;
        public string ModuleId;
        public string Url = "#";
        public bool Report { get; set; }
        public NavigationMenu()
        {

        }

        public NavigationMenu(int menuId, String menuName, string action, string controller, string icon, int parentId, string area, string moduleId, bool report = false)
        {
            if (ValueConverters.IsStringEmpty(action))
            {
                // action = "#";
            }
            this.MenuId = menuId;
            menuName = menuName.Length > 50 ? menuName.Substring(0, 50) : menuName;
            this.MenuName = menuName;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
            this.Icon = icon;
            this.ParentId = parentId;
            this.ModuleId = moduleId;
            this.Report = report;
        }

        public void GenerateMenus()
        {
            List<NavigationMenu> mymenuList = new List<NavigationMenu>();
            // MainMenu
            mymenuList.Add(new NavigationMenu(0000, "DashBoard", "", "#", "fa fa-lg fa-fw fa-dashboard", -1, "", ""));
            mymenuList.Add(new NavigationMenu(10000, "System Setup", "", "#", "fa fa-lg fa-fw fa-wrench", -1, "", "100000"));
            mymenuList.Add(new NavigationMenu(20000, "Registry", "", "#", "fa fa-lg fa-fw fa-briefcase", -1, "", "200000"));
            mymenuList.Add(new NavigationMenu(30000, "Banking", "", "#", "fa fa-lg fa-fw fa-money", -1, "", "300000"));
            mymenuList.Add(new NavigationMenu(40000, "Loaning", "", "#", "fa fa-lg fa-fw fa-gift", -1, "", "400000"));
            mymenuList.Add(new NavigationMenu(50000, "Authorisations", "", "#", "fa fa-lg fa-fw fa-shield", -1, "", "500000"));
            mymenuList.Add(new NavigationMenu(60000, "Accounts", "", "#", "fa fa-lg fa-fw fa-book", -1, "", "600000"));
            mymenuList.Add(new NavigationMenu(70000, "Human Resource", "", "#", "fa fa-lg fa-fw fa-group", -1, "", "700000"));
            mymenuList.Add(new NavigationMenu(80000, "Asset Register", "", "#", "fa fa-lg fa-fw fa-tags", -1, "", "800000"));
            mymenuList.Add(new NavigationMenu(90000, "Stock Register", "", "#", "fa fa-lg fa-fw fa-truck", -1, "", "900000"));
            mymenuList.Add(new NavigationMenu(98000, "Periodic Procedures", "", "#", "fa fa-lg fa-fw fa-ticket", -1, "", "980000"));
            mymenuList.Add(new NavigationMenu(99000, "System Settings", "", "#", "fa fa-lg fa-fw fa-cogs", -1, "", "990000"));

            DashBoard(mymenuList);
            SystemSetup(mymenuList);
            Registry(mymenuList);
            Banking(mymenuList);
            Loaning(mymenuList);
            Accounts(mymenuList);
            Authorisations(mymenuList);
            HumanResource(mymenuList);
            AssetRegister(mymenuList);
            StockRegister(mymenuList);
            PeriodicProcedures(mymenuList);
            SystemSettings(mymenuList);


            MenuManager menuMgr = new MenuManager();

            menuMgr.SaveSystemMenus(mymenuList);


        }

        private void DashBoard(List<NavigationMenu> mymenuList)
        {
            mymenuList.Add(new NavigationMenu(0001, "DashBoard", "Index", "DashBoard", "fa fa-lg fa-fw fa-dashboard", 0000, "DashBoard", ""));
        }


        private static void SystemSetup(List<NavigationMenu> mymenuList)
        {
            //System Setup
            mymenuList.Add(new NavigationMenu(10010, "General Settings", "", "#", "fa fa-lg fa-fw  fa-home", 10000, "GeneralSettings", "100010"));
            mymenuList.Add(new NavigationMenu(10020, "Product Settings", "", "#", "fa fa-lg fa-fw  fa-home", 10000, "ProductSettings", "100200"));
            mymenuList.Add(new NavigationMenu(10030, "Savings Settings", "", "#", "fa fa-lg fa-fw  fa-home", 10000, "", "100300"));
            mymenuList.Add(new NavigationMenu(10040, "Commissions", "", "#", "fa fa-lg fa-fw  fa-home", 10000, "", "100400"));
            mymenuList.Add(new NavigationMenu(10050, "Mobile Banking Settings", "", "#", "fa fa-lg fa-fw  fa-home", 10000, "Messaging", "1004G0"));

            // System Setup/General Settings
            mymenuList.Add(new NavigationMenu(10011, "A Society Branches", "Index", "BranchSettings", "", 10010, "GeneralSettings", "10001A"));
            mymenuList.Add(new NavigationMenu(10012, "B Employers Codes", "Index", "Employers", "", 10010, "GeneralSettings", "10001B"));
            mymenuList.Add(new NavigationMenu(10013, "C Station Codes ", "Index", "StationCodes", "", 10010, "GeneralSettings", "10001C"));
            mymenuList.Add(new NavigationMenu(10014, "D Membership Types ", "Index", "MembershipTypes", "", 10010, "GeneralSettings", "10001D"));
            mymenuList.Add(new NavigationMenu(10015, "E Postal Codes ", "Index", "PostalCodes", "", 10010, "GeneralSettings", "10001E"));
            mymenuList.Add(new NavigationMenu(10016, "F Departmental Offices", "Index", "OfficeLocations", "", 10010, "GeneralSettings", "10001F"));
            mymenuList.Add(new NavigationMenu(10017, "G Zone Codes ", "Index", "ZoneCodes", "", 10010, "GeneralSettings", "10001G"));

            // System Setup/Product Settings
            mymenuList.Add(new NavigationMenu(10021, "A Loan Codes ", "Index", "LoanCodes", "", 10020, "ProductSettings", "1002A0"));
            mymenuList.Add(new NavigationMenu(10022, "B Investment Codes ", "Index", "InvestmentCodes", "", 10020, "ProductSettings", "1002B0"));
            mymenuList.Add(new NavigationMenu(10023, "C Collateral Documents Types ", "Index", "CollateralTypes", "", 10020, "ProductSettings", "1002C0"));

            // System Setup/Savings Settings
            mymenuList.Add(new NavigationMenu(10031, "A Savings Settings ", "Index", "SavingsProducts", "", 10030, "ProductSettings", "1003A1"));

            // System Setup/Commission Settings
            mymenuList.Add(new NavigationMenu(10041, "A Commissions Codes ", "Index", "CommissionCodes", "", 10040, "ProductSettings", "1003A1"));
            mymenuList.Add(new NavigationMenu(10042, "B Payments Types ", "Create", "PaymentCommissions", "", 10040, "ProductSettings", "1003A1"));

            mymenuList.Add(new NavigationMenu(10043, "C Teller Cheque Deposits ", "", "#", "", 10040, "", "1004C0"));
            mymenuList.Add(new NavigationMenu(10431, "1 Cheque Types", "Index", "ChequeDepositTypes", "", 10043, "ProductSettings", "1004C1"));
            mymenuList.Add(new NavigationMenu(10432, "2 Cheque Deposit Banks", "Index", "ChequeDepositBanks", "", 10043, "ProductSettings", "1004C2"));

            mymenuList.Add(new NavigationMenu(10046, "D Fixed Deposit Rates ", "Index", "FixedDepositsRates", "", 10040, "ProductSettings", "1004D0"));
            mymenuList.Add(new NavigationMenu(10047, "E Payout Codes", "Index", "PayoutCodes", "", 10040, "ProductSettings", "1004F0"));

            // System Setup/Mobile Banking Settings
            mymenuList.Add(new NavigationMenu(10051, "F Messaging Setup", "", "#", "", 10050, "", "1004G0"));
            mymenuList.Add(new NavigationMenu(10511, "1 Charges Setup ", "Index", "SubscriptionTypes", "", 10051, "Messaging", "1004G1"));
            mymenuList.Add(new NavigationMenu(10512, "2 Define Messaging Groups", "Index", "SmsGroupCodes", "", 10051, "Messaging", "1004G2"));
            mymenuList.Add(new NavigationMenu(10513, "3 Attach Members to Messaging Groups", "SmsGroupList", "SmsGroups", "", 10051, "Messaging", "1004G3"));
        }


        private void Registry(List<NavigationMenu> mymenuList)
        {
            //Registry Menus
            mymenuList.Add(new NavigationMenu(20001, "Members Register ", "CustomerSearch", "Customer", "", 20000, "Registry", "2001A0"));
            mymenuList.Add(new NavigationMenu(20002, "Customer Accounts", "Index", "CustomerAccounts", "", 20000, "Registry", "2001B0"));
            mymenuList.Add(new NavigationMenu(20003, "Authorise Sub Accounts", "AuthoriseSubAccounts", "CustomerAccounts", "", 20000, "Registry", "2001C0"));
            mymenuList.Add(new NavigationMenu(20004, "Accounts Comments / Remarks", "AccountRemarksComments", "CustomerAccounts", "", 20000, "Registry", "2001D0"));
            mymenuList.Add(new NavigationMenu(20005, "External Numbers Maintainance ", "SelectExternalFileLinkCode", "ExternalNumbers", "", 20000, "Registry", "2001E0"));

            mymenuList.Add(new NavigationMenu(20006, "File Movements", "", "#", "fa fa-lg fa-fw  fa-home", 20000, "", "2001F0"));
            mymenuList.Add(new NavigationMenu(20007, "Staff And Committee Classification ", "StaffCommitteeClassification", "StaffCommiteeClassification", "", 20000, "Registry", "2001G0"));
            mymenuList.Add(new NavigationMenu(20008, "Photos And Specimen Capturing", "Index", "PhotosSpecimenCapturing", "", 20000, "Registry", "2001H0"));
            //mymenuList.Add(new NavigationMenu(20009, "Transfer Accounts", "TransferAccount", "TransferAccount", "", 20000, "Registry", "2001I0"));
            mymenuList.Add(new NavigationMenu(20010, "MicroFinance", "", "#", "fa fa-lg fa-fw  fa-home", 20000, "", "280000"));
            mymenuList.Add(new NavigationMenu(20011, "Reports", "Index", "Reports", "", 20000, "Registry", ""));
            //Registry File Movements
            mymenuList.Add(new NavigationMenu(20061, "1 File Dispatching ", "FileDispatch", "Files", "", 20006, "Registry", ""));
            mymenuList.Add(new NavigationMenu(20062, "2 File Receiving ", "FileReceit", "Files", "", 20006, "Registry", ""));
            //Registry/MicroFinance
            mymenuList.Add(new NavigationMenu(20063, "MicroFinance Details", "", "#", "", 20010, "", "280100"));
            mymenuList.Add(new NavigationMenu(20631, "1 Agents Details", "MicroAgentList", "MicroFinance", "", 20063, "Registry", "280100"));
            mymenuList.Add(new NavigationMenu(20632, "2 Regions Details", "MicroFinanceRegionList", "MicroFinance", "", 20063, "Registry", "280100"));
            mymenuList.Add(new NavigationMenu(20633, "3 SubRegions Details", "MicroFinanceSubRegionList", "MicroFinance", "", 20063, "Registry", "280100"));

            mymenuList.Add(new NavigationMenu(20066, "Group Main Accounts Register", "MicroFinanceRegistry", "MicroFinance", "", 20010, "Registry", "280200"));
            mymenuList.Add(new NavigationMenu(20067, "Group Members Register", "MicroFinanceMembers", "MicroFinance", "", 20010, "Registry", "280300"));
            //Registry Reports
            mymenuList.Add(new NavigationMenu(20111, "A Members Register Report", "MemberList", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20112, "B Member Missing Some Details Report", "MemberMissingDetails", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20113, "C Members Register -By Stations Report ", "MemberStationList", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20114, "D Member Accounts Register Report", "MemberAccountsRegister", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20115, "E Closed Accounts Report", "ClosedAccounts", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20116, "F Member Savings Status Report ", "CustomerSavingStatus", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20118, "G Member Investments Status Report", "InvestmentStatus", "Reports", "", 20011, "Registry", "299900", true));

            mymenuList.Add(new NavigationMenu(20119, "H Society Member's Age Statistics Report", "MemberAgeStatistics", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20120, "I Society Statistical Analysis Report", "SocietyStatistics", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20121, "J Single File Tracking Report", "MemberFileTracking", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20122, "K File Tracking Listing Report", "FileTrackingListing", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20123, "L MicroFinance Group Main Report", "GroupMainListing", "Reports", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20124, "M MicroFinance Group Members Report", "GroupMemberListing", "Reports", "", 20011, "Registry", "299900", true));

            mymenuList.Add(new NavigationMenu(20125, "N Members With Comments/Remarks ", "Reports", "", "", 20011, "Registry", "299900", true));
            mymenuList.Add(new NavigationMenu(20126, "O Analysed Membership", "", "Reports", "", 20011, "Registry", "299900", true));
        }


        private void Banking(List<NavigationMenu> mymenuList)
        {
            //Banking
            mymenuList.Add(new NavigationMenu(30010, "Treasury Operations", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "Treasury", "300100"));
            mymenuList.Add(new NavigationMenu(30020, "Teller Operations", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "Tellering", "300200"));
            mymenuList.Add(new NavigationMenu(30030, "Cheques Clearance", "ChequeClearanceList", "SavingsChequeClearance", "fa fa-lg fa-fw  fa-home", 30000, "Banking", "3003A0"));
            mymenuList.Add(new NavigationMenu(30040, "Salaries/Payouts", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "3004A0"));
            mymenuList.Add(new NavigationMenu(30050, "Fixed Deposits", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "3006A0"));
            mymenuList.Add(new NavigationMenu(30060, "Savings Bankers Cheque", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "300700"));
            mymenuList.Add(new NavigationMenu(30070, "Standing Orders", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "3008A0"));
            mymenuList.Add(new NavigationMenu(30080, "Noncash Transactions", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "300900"));
            mymenuList.Add(new NavigationMenu(30090, "Complete Shares WithDrawal", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "301100"));
            //Investment(Shares) Drive
            mymenuList.Add(new NavigationMenu(30100, "Investment(Shares) Drive", "", "#", "fa fa-lg fa-fw  fa-home", 30000, "", "301200"));
            mymenuList.Add(new NavigationMenu(30200, "Reports", "Index", "Reports", "fa fa-lg fa-fw  fa-home", 30000, "Banking", "300999"));

            // Banking/Treasury Operations
            mymenuList.Add(new NavigationMenu(30011, "A Cash Operations", "Treasury", "Treasury", "", 30010, "Treasury", "3001A0"));

            // Banking/Teller Operations
            mymenuList.Add(new NavigationMenu(30021, "A Savings Cash Transactions ", "SavingsTransactions", "TellerSavingsOperations", "", 30020, "Tellering", "3002A0"));
            mymenuList.Add(new NavigationMenu(30022, "B Ledger A/c Transactions", "TellerLedgerTransaction", "TellerLedgerOperations", "", 30020, "Tellering", "3002B0"));
            mymenuList.Add(new NavigationMenu(30023, "C Product Repayments ", "ProductRepayments", "TellerProductRepayments", "", 30020, "Tellering", "3002B0"));
            mymenuList.Add(new NavigationMenu(30024, "D MicroFinance Repayments ", "", "#", "", 30020, "Tellering", "3002D0"));
            mymenuList.Add(new NavigationMenu(30025, "E Cash Transfer ", "CashTransfer", "TellerCashTransfer", "", 30020, "Tellering", "300970"));
            mymenuList.Add(new NavigationMenu(30026, "F Close Day ", "CloseDay", "TellerCloseDay", "", 30020, "Tellering", "300990"));

            // Banking/Teller Operations/Ledger A/c Transactions
            mymenuList.Add(new NavigationMenu(30027, "A Direct Receipts/Payments/Salary Cheques ", "TellerLedgerTransaction", "TellerLedgerOperations", "", 30022, "Tellering", "3002B0"));
            mymenuList.Add(new NavigationMenu(30028, "B Authorised Payments", "AuthorisedPayments", "TellerLedgerOperations", "", 30022, "Tellering", "3002B0"));
            mymenuList.Add(new NavigationMenu(30029, "C Agency Transactions ", "AGENCYTransactions", "TellerLedgerOperations", "", 30022, "Tellering", "3002B0"));
            mymenuList.Add(new NavigationMenu(30031, "D Cash Bankers Cheques ", "TellerBankers", "TellerBankers", "", 30022, "Tellering", "3002B0"));

            // Banking/Salaries/Payouts
            mymenuList.Add(new NavigationMenu(30041, "A Payout Extraction", "SelectAutomaticBatchToPost", "SalariesPayout", "", 30040, "Banking", "3004A1"));
            mymenuList.Add(new NavigationMenu(30042, "B Manual Payouts", "SelectManualBatchToPost", "SalariesPayout", "", 30040, "Banking", "3004A3"));
            mymenuList.Add(new NavigationMenu(30043, "C View Posted Batches ", "ViewPostedSalaryBatches", "SalariesPayout", "", 30040, "Banking", ""));

            // Banking/Fixed Deposits
            mymenuList.Add(new NavigationMenu(30051, "A Invocation Registration", "Registration", "FixedDeposits", "", 30050, "Banking", "3006A2"));
            mymenuList.Add(new NavigationMenu(30052, "B Invocation Authorisation", "RegistrationList", "FixedDeposits", "", 30050, "Banking", "3006A3"));
            mymenuList.Add(new NavigationMenu(30053, "C Revocation of Deposits", "RevocationList", "FixedDeposits", "", 30050, "Banking", "3006A4"));
            mymenuList.Add(new NavigationMenu(30054, "D Payment Of Matured ", "MaturityList", "FixedDeposits", "", 30050, "Banking", "3006A5"));



            // Banking/Savings Bankers Cheque
            mymenuList.Add(new NavigationMenu(30061, "A Bankers Cheque Capturing", "Capturing", "SavingsBankersCheque", "", 30060, "Banking", "300710"));
            mymenuList.Add(new NavigationMenu(30062, "B Bankers Cheque Posting", "ListUnPosted", "SavingsBankersCheque", "", 30060, "Banking", "300720"));

            // Banking/Standing Orders
            mymenuList.Add(new NavigationMenu(30071, "A Standing Orders", "SearchStandingOrders", "StandingOrders", "", 30070, "Banking", "3008A0"));
            mymenuList.Add(new NavigationMenu(30072, "B Arrears Adjustments", "ManageArrears", "ArrearsManagement", "", 30070, "Banking", "3008A1"));

            // Banking/Noncash Transactions
            mymenuList.Add(new NavigationMenu(30081, "A Product vs Product Journals", "ProductJournalsList", "NoncashTransactions", "", 30080, "Banking", "300910"));
            mymenuList.Add(new NavigationMenu(30082, "B Ledger Vs Products Journals", "ProductLedgerJournalsList", "NoncashTransactions", "", 30080, "Banking", "300920"));
            mymenuList.Add(new NavigationMenu(30083, "C Savings Vs Product Journals", "ProductSavingsJournalsList", "NoncashTransactions", "", 30080, "Banking", "300930"));

            //Banking/Complete Shares WithDrawal
            mymenuList.Add(new NavigationMenu(30091, "A Normal Shares Withdrawals", "Withdrawals", "MembersCompleteWithdrawal", "", 30090, "Banking", "3010B1"));
            mymenuList.Add(new NavigationMenu(30092, "B Funeral Rider", "FuneralRiderList", "MembersFuneralRider", "", 30090, "Banking", "3010C0"));
            mymenuList.Add(new NavigationMenu(30093, "C Death Claims", "DeathClaimList", "MembersDeathClaim", "", 30090, "Banking", "3010D0"));
            mymenuList.Add(new NavigationMenu(30094, "D Retirement Bonus", "RetirementBonusView", "MembersRetirementBonus", "", 30090, "Banking", "3010D0"));
            //Allocate Transactions
            mymenuList.Add(new NavigationMenu(30101, "A Allocate Transactions", "", "#", "", 30100, "Banking", "301210"));
            mymenuList.Add(new NavigationMenu(30102, "B Reports", "", "#", "", 30100, "Banking", "301220"));
            //Banking Reports
            mymenuList.Add(new NavigationMenu(30111, "A Customer Statement", "FinancialStatus", "MembersFinancialStatus", "", 30200, "Registry", "300999", true));
            mymenuList.Add(new NavigationMenu(30112, "B1 Tellers Daily Transactions", "TellersDailyTransaction", "Reports", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30113, "B2 View MicroFinance Repayments", "", "#", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30114, "B3 Tellers Cash Closing", "TellerDayClosingReport", "TellerDayClosingReport", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30115, "B4 Tellers Micro Daily Returns Summary", "", "#", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30116, "B5 Tellers Vouchers Reprint", "TellerVoucherReprint", "TellerVoucherReprintReport", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30117, "C1 Customer Product Balances", "ProductBalanceReport", "ProductBalanceReport", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30118, "C2 Loan Balances", "LoanBalances", "LoanBalances", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30119, "C3 Investment Balances", "ShareBalances", "ShareBalances", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30120, "C4 Savings Balances ", "ShavingsBalances", "ShavingsBalances", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30121, "C5 Branch Balances Listing ", "BranchBalancesListing", "BranchBalancesListing", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30122, "C6 Loans Vs Deposits", "Index", "LoanVsDeposits", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30123, "C7 Dormant Investment Balances", "InvestmentBalancesDormantReport", "InvestmentDormantReport", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30124, "D1 All Cheques Deposited Report ", "ChequeReceived", "AllChequeReceived", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30125, "D2 All Cheques Matured Report ", "AllChequeMatured", "AllChequeMatured", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30126, "D3 All Cheques Cleared Report ", "AllChequeCleared", "AllChequeCleared", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30127, "E1 Cheques Printing ", "ChequePrint", "ChequePrint", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30128, "E2 Cheque Printing Report ", "ChequeList", "ChequesPrinting", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30129, "F1 Fixed Deposits -UnApproved ", "FixedDepositUnapproved", "FixedDepositUnapproved", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30130, "F2 Fixed Deposits Listing", "FixedDepositsListing", "FixedDepositListing", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30131, "F3 Fixed Deposits Revoked", "FixedDepositRevoked", "FixedDepositRevoked", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30132, "F4 Matured Fixed Deposits -Unpaid", "MaturedFixedDepositsUnpaid", "MaturedFixedDepositUnpaid", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30133, "F5 Matured Fixed Deposits -Paid", "MaturedFixedDepositsPaid", "MaturedFixedDepositsPaid", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30134, "G1 Savings Bankers Cheques UnApproved", "SavingsBankersChequesListing", "SavingsBankersChequesUnapproved", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30135, "G2 Savings Bankers Cheques Paid", "BankersPaid", "BankersPaid", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30136, "H1 View Posted Product Vs Product Journals", "ViewProductJournals", "NoncashTransactions", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30137, "H2 View Posted Ledger Vs Products Journals", "ViewLedgerJournals", "NoncashTransactions", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30138, "H3 View Posted Savings Vs Product Journals", "ViewSavingsJournals", "NoncashTransactions", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30139, "I1 Shares WithDrawal  Report", "SharesWithdrawal", "SharesWithdrawal", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30140, "I1 View Posted Complete Shares WithDrawals", "ViewPostedWithdrawals", "MembersCompleteWithdrawal", "", 30200, "Banking", "300999", true));
            mymenuList.Add(new NavigationMenu(30141, "J1 Standing Orders Recovery Reports", "OptionListings", "StandingOrderRecovery", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30142, "J2 Arrears Reports", "CustomerArrearsRecovery", "CustomerArrearsRecovery", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30143, "K1 MicroFinance/Group Balances", "Index", "MicrofinanceGroupBalances", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30144, "L1 Savings Transaction By Posting References", "SavingsTransactionsByRefListing", "SavingsTransactionsByRef", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30145, "L2 Investments Transaction By Posting References", "ShareTransactionsByReference", "ShareTransactionsByReference", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30146, "L3 Loans Transaction By Posting References", "LoansTransactionsByReference", "LoansTransactionsByReference", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30147, "L4 Savings Transactions By Posting Ref Summary", "SavingsTransactionsSummary", "SavingsTransactionsSummary", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30148, "M1 Branch Summary", "BranchSummary", "BranchSummary", "", 30200, "BankingReports", "300999", true));

            // Loaning/Sacco Statistics
            mymenuList.Add(new NavigationMenu(30149, "N1 Top Saver(Investments)", "TopSaversInvestmentsListing", "TopSaversInvestments", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30150, "N2 Top Saver(Fosa -Savings)", "TopSavingsSaversListing", "TopSavingsSavers", "", 30200, "BankingReports", "300999", true));
            mymenuList.Add(new NavigationMenu(30151, "N3 Top Loanee (Balances )", "TopLoaneesListing", "TopLoanees", "", 30200, "BankingReports", "300999", true));


        }

        private void Loaning(List<NavigationMenu> mymenuList)
        {
            //Loaning
            mymenuList.Add(new NavigationMenu(40010, "Loan Eligibility / Calculator", "", "#", "fa fa-lg fa-fw  fa-home", 40000, "", "4001A0"));
            mymenuList.Add(new NavigationMenu(40020, "Loan Registration", "ListOfLoansRegistered", "LoaningProcess", "fa fa-lg fa-fw  fa-home", 40000, "Loaning", "4002A0"));
            mymenuList.Add(new NavigationMenu(40030, "Loan Process", "", "#", "fa fa-lg fa-fw  fa-home", 40000, "", "4003A0"));
            mymenuList.Add(new NavigationMenu(40040, "Short Term Loans", "ShortTermLoansList", "LoaningProcess", "", 40000, "Loaning", "4004A0"));
            mymenuList.Add(new NavigationMenu(40045, "Loan Disbursement", "LoanDisbursementList", "LoaningProcess", "Loaning", 40000, "Loaning", "4003A4"));
            mymenuList.Add(new NavigationMenu(40050, "Loan Collaterals", "RegisteredCollateralList", "LoanCollateral", "fa fa-lg fa-fw  fa-home", 40000, "Loaning", "4006A0"));
            mymenuList.Add(new NavigationMenu(40060, "Guarantors  Management", "", "#", "fa fa-lg fa-fw  fa-home", 40000, "", "400700"));
            mymenuList.Add(new NavigationMenu(40070, "Check Off Processing", "", "#", "fa fa-lg fa-fw  fa-home", 40000, "", "470000"));
            mymenuList.Add(new NavigationMenu(40080, "Reports", "Index", "Reports", "fa fa-lg fa-fw  fa-home", 40000, "Loaning", "499900"));


            // Loaning/Loan Eligibility / Calculator
            mymenuList.Add(new NavigationMenu(40011, "A Calculator", "", "#", "", 40010, "", ""));
            mymenuList.Add(new NavigationMenu(40012, "B Eligibility", "", "#", "", 40010, "", ""));
            // Loaning/Loan process
            mymenuList.Add(new NavigationMenu(40031, "A Loan Appraisal", "LoanAppraisalList", "LoaningProcess", "", 40030, "Loaning", "4003A1"));
            mymenuList.Add(new NavigationMenu(40032, "B Loan Approval", "LoanApprovalList", "LoaningProcess", "", 40030, "Loaning", "4003A2"));
            mymenuList.Add(new NavigationMenu(40033, "C Loan Auditing", "LoanAudittingList", "LoaningProcess", "", 40030, "Loaning", "4003A3"));
            //mymenuList.Add(new NavigationMenu(40034, "D Loan Disbursement", "LoanDisbursementList", "LoaningProcess", "", 40030, "Loaning", "4003A4"));

            // Loaning/Guarantors  Management
            mymenuList.Add(new NavigationMenu(40061, "A Substitution", "Substitute", "GuarantorManagement", "", 40060, "Loaning", "400700"));
            mymenuList.Add(new NavigationMenu(40062, "B Capture Guarantors", "CaptureGuarantors", "GuarantorManagement", "", 40060, "Loaning", "400700"));
            mymenuList.Add(new NavigationMenu(40063, "C Attach Guarantors-Defaulters", "AttachGuarantor", "GuarantorManagement", "", 40060, "Loaning", "400700"));

            // Loaning/Check Off Processing
            mymenuList.Add(new NavigationMenu(40071, "A Loan Variations", "LoanVariations", "LoanVariations", "", 40070, "Loaning", "470010"));
            mymenuList.Add(new NavigationMenu(40072, "B Check Off Extraction", "SelectAutomaticBatchToPost", "LoansCheckOff", "", 40070, "Loaning", "470020"));
            mymenuList.Add(new NavigationMenu(40073, "C Manual Check Off Posting", "SelectManualBatchToPost", "LoansCheckOff", "", 40070, "Loaning", "470030"));
            mymenuList.Add(new NavigationMenu(40074, "D Employer Deductions Extraction", "", "#", "", 40070, "", "470040"));
            mymenuList.Add(new NavigationMenu(40075, "E Employer Ability Extraction", "", "#", "", 40070, "", "470050"));
            mymenuList.Add(new NavigationMenu(40076, "F View Posted CheckOff", "ViewPostedCheckoffBatches", "LoansCheckOff", "", 40070, "Loaning", "5001H3"));

            // Loaning/Loaning Reports
            //Loaning Reports
            mymenuList.Add(new NavigationMenu(40081, "Loaning Reports ", "OptionListings", "LoaningReports", "", 40080, "LoaningReports", "499900", true));
            mymenuList.Add(new NavigationMenu(40082, "Loans Statistical Reports", "OptionListings", "LoansExpectedListing", "", 40080, "LoaningReports", "499900", true));
            mymenuList.Add(new NavigationMenu(40083, "Security Reports", "Index", "SecurityReports", "", 40080, "LoaningReports", "499900", true));


            // Loaning/Loans Statistical Reports
            mymenuList.Add(new NavigationMenu(40091, "B1 Loans Expected Listing", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40092, "B2 Loans Defaulters Report", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40093, "B3 Members Information (CRB Template)", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40094, "B4 Society Loans Book Report", "", "#", "", 40080, "", "499900", true));
            // Loaning/Loans Security Report
            mymenuList.Add(new NavigationMenu(40095, "C1 Members Loans Guarantors", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40096, "C2 Guarantees Member", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40097, "C3 Collateral Listing", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40098, "C4 Collateral By Document Type", "", "#", "", 40080, "", "499900", true));
            mymenuList.Add(new NavigationMenu(40099, "C5 Self Guarantee", "", "#", "", 40080, "", "499900", true));

        }

        private void Authorisations(List<NavigationMenu> mymenuList)
        {
            //Authorisations
            mymenuList.Add(new NavigationMenu(50010, "Transaction Authorisations", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "Tellering", "5001A0"));
            mymenuList.Add(new NavigationMenu(50020, "Account Notices", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500200"));
            mymenuList.Add(new NavigationMenu(50030, "ATM Cards Management", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500300"));
            mymenuList.Add(new NavigationMenu(50040, "Account Management", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500400"));
            mymenuList.Add(new NavigationMenu(50050, "Account Exemptions", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500700"));
            mymenuList.Add(new NavigationMenu(50060, "SMS Management", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500600"));
            mymenuList.Add(new NavigationMenu(50070, "USSD Management", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500800"));
            mymenuList.Add(new NavigationMenu(50080, "POS Module", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "500900"));

            mymenuList.Add(new NavigationMenu(51000, "Reports", "Index", "AuthorisationReports", "fa fa-lg fa-fw  fa-home", 50000, "AuthorisationReports", "509800"));

            mymenuList.Add(new NavigationMenu(52000, "System Audit Trails", "", "#", "fa fa-lg fa-fw  fa-home", 50000, "", "509900"));
            mymenuList.Add(new NavigationMenu(53000, "Transactions Logs", "GetLog", "TransactionLogs", "", 52000, "AuthorisationReports", "500900"));
            mymenuList.Add(new NavigationMenu(54000, "System Audit Trails", "AuditTrail", "AuditTrail", "", 52000, "AuthorisationReports", "509900"));


            //Authorisations/Transaction Authorisations
            mymenuList.Add(new NavigationMenu(50011, "A Teller Operations Authorisations", "UnAuthorisedTransactions", "TellerTransactionAuthorisations", "", 50010, "Tellering", "5001A1"));
            //Authorisations/Salary/Payouts Posting Batches
            mymenuList.Add(new NavigationMenu(50012, "B Salary/Payouts", "", "#", "", 50010, "", "5001A2"));


            //Authorisations/SalaryPayouts/Sub Menu
            mymenuList.Add(new NavigationMenu(50121, "1 Create Posting Batches", "Index", "SalariesPayout", "", 50012, "Banking", "5001A2"));
            mymenuList.Add(new NavigationMenu(50122, "2 Authorise/Post Batches", "AuthorisePostingBatches", "SalariesPayout", "", 50012, "Banking", "5001A2"));
            mymenuList.Add(new NavigationMenu(50123, "3 View Posted Batches", "ViewPostedSalaryBatches", "SalariesPayout", "", 50012, "Banking", "5001A2"));


            //Authorisations/Authorise /Post Journals
            mymenuList.Add(new NavigationMenu(50013, "C Authorise/Post Journals", "", "#", "", 50010, "", "5001A3"));
            //Authorisations/Authorise /Post Journals/Sub Menu
            mymenuList.Add(new NavigationMenu(50131, "1 Product Vs Product ", "ProductJournalsToVerify", "NoncashTransactions", "", 50013, "Banking", "5001A8"));
            mymenuList.Add(new NavigationMenu(50132, "2 Ledger Vs Products ", "ProductLedgerJournalsToVerify", "NoncashTransactions", "", 50013, "Banking", "5001A9"));
            mymenuList.Add(new NavigationMenu(50133, "3 Savings vs Product ", "ProductSavingsJournalsToVerify", "NoncashTransactions", "", 50013, "Banking", "5001B0"));
            mymenuList.Add(new NavigationMenu(50134, "4 Retirement Bonus ", "RetirementBonusVerificationList", "MembersRetirementBonus", "", 50013, "Banking", "3010D2"));


            //Authorisations/CheckOff Batches
            mymenuList.Add(new NavigationMenu(50016, "D CheckOff Batches", "", "#", "", 50010, "", "5001H0"));
            //Authorisations/CheckOff Batches/Sub Menu
            mymenuList.Add(new NavigationMenu(50161, "1 Create Posting Batches", "Index", "LoansCheckOff", "", 50016, "Loaning", "5001H1"));
            mymenuList.Add(new NavigationMenu(50162, "2 Authorise/Post  Batches", "AuthoriseCheckOffBatches", "LoansCheckOff", "", 50016, "Loaning", "5001H2"));
            mymenuList.Add(new NavigationMenu(50163, "3 View Posted Batches", "ViewPostedCheckoffBatches", "LoansCheckOff", "", 50016, "Loaning", "5001H3"));



            //Authorisations/Authorise Complete Shares WithDrawal
            mymenuList.Add(new NavigationMenu(50017, "E Shares WithDrawal", "", "#", "", 50010, "", "5001G0"));
            mymenuList.Add(new NavigationMenu(50171, "1 Complete Shares WithDrawal", "WithdrawalBatchesToVerify", "MembersCompleteWithdrawal", "", 50017, "Banking", "5001G1"));
            mymenuList.Add(new NavigationMenu(50172, "2 Funeral Rider", "FuneralRiderClaimsToVerify", "MembersFuneralRider", "", 50017, "Banking", "5001G2"));
            mymenuList.Add(new NavigationMenu(50173, "3 Death Claims", "DeathClaimBatchToVerify", "MembersDeathClaim", "", 50017, "Banking", "5001G3"));


            //Authorisations/Authorise / Post Dividends
            mymenuList.Add(new NavigationMenu(50114, "G Authorise/Post Dividends", "", "#", "", 50010, "", ""));
            mymenuList.Add(new NavigationMenu(50141, "1 Balance As At Dividends", "BalanceAsAtTransfer", "DividendProcessing", "", 50114, "PeriodicProcedures", "9802A1"));
            mymenuList.Add(new NavigationMenu(50142, "2 Prorata Dividends", "ProrataTransfer", "DividendProcessing", "", 50114, "PeriodicProcedures", "9802A1"));

            //Authorisations/Authorise / Post defaulters Guarantors attached
            mymenuList.Add(new NavigationMenu(50115, "H Defaulters Guarantors Attached", "", "#", "", 50010, "", "5001J0"));
            mymenuList.Add(new NavigationMenu(50151, "1 Verify/Post Guarantors Attached", "AuthoriseGuarantorAttachments", "GuarantorManagement", "", 50115, "Loaning", "5001J1"));
            mymenuList.Add(new NavigationMenu(50152, "2 View Posted Guarantors Attached", "PostedGuarantorAttachments", "GuarantorManagement", "", 50115, "Loaning", "5001J2"));

            //Authorisations/Authorise Cheques Clearing
            mymenuList.Add(new NavigationMenu(50116, "I Authorise Cheques Clearing", "VerifyChequeForClearanceList", "SavingsChequeClearance", "", 50010, "Banking", "50001J"));

            //Authorisations/Transactions Reversals
            mymenuList.Add(new NavigationMenu(50199, "Z Transactions Reversals", "", "#", "", 50010, "", "5001F0"));



            //Authorisations/Account Notices
            mymenuList.Add(new NavigationMenu(50021, "A Cash WithDrawal Notice", "Index", "CashWithdrawalNotice", "", 50020, "Banking", "5002A0"));
            mymenuList.Add(new NavigationMenu(50022, "B Complete WithDrawal", "Index", "CompleteWithdrawalNotices", "", 50020, "Banking", "5002B0"));
            mymenuList.Add(new NavigationMenu(50023, "C Dividend Capitalisation", "CapitalisationList", "DividendProcessing", "", 50020, "PeriodicProcedures", "5002C0"));
            //Authorisations/ATM Cards Management
            mymenuList.Add(new NavigationMenu(50031, "A ATM Cards Attaching", "ManageAtmCard", "AtmCards", "", 50030, "Authorisations", "500310"));
            mymenuList.Add(new NavigationMenu(50032, "B ATM Cards Authorisation", "AuthoriseAtmCard", "AtmCards", "", 50030, "Authorisations", "500320"));
            mymenuList.Add(new NavigationMenu(50033, "C ATM Withdrawal Limit", "AuthoriseWithdrawalRequest", "AtmCards", "", 50030, "Authorisations", "500330"));

            //  Submenu Above Limit ATM Withdrawal
            mymenuList.Add(new NavigationMenu(50331, "1 Create Request", "CreateATMWithdrawalRequest", "AtmCards", "", 50033, "Authorisations", "500331"));
            mymenuList.Add(new NavigationMenu(50332, "2 Authorise Request", "AuthoriseWithdrawalRequest", "AtmCards", "", 50033, "Authorisations", "500332"));

            //Authorisations/Account Management
            mymenuList.Add(new NavigationMenu(50041, "A Account Closure", "", "#", "", 50040, "", "500410"));
            mymenuList.Add(new NavigationMenu(50042, "B Deceased Cases Registration", "Index", "DeceasedCases", "", 50040, "Banking", "500420"));
            mymenuList.Add(new NavigationMenu(50043, "C Members Rejoining", "", "#", "", 50040, "", "500430"));
            mymenuList.Add(new NavigationMenu(50044, "D Reinstate Closed Accounts", "", "#", "", 50040, "", "500440"));

            //  Submenu Account Closure
            mymenuList.Add(new NavigationMenu(50431, "1 Create Request", "MemberRejoining", "Customer", "", 50043, "Registry", "500431"));
            mymenuList.Add(new NavigationMenu(50432, "2 Authorise Request", "AuthoriseMemberRejoining", "Customer", "", 50043, "Registry", "500432"));


            //  Submenu Account Closure
            mymenuList.Add(new NavigationMenu(50411, "1 Create Request", "CreateAccountClosureRequest", "CustomerAccounts", "", 50041, "Registry", "500411"));
            mymenuList.Add(new NavigationMenu(50412, "2 Authorise Request", "AuthoriseAccountClosure", "CustomerAccounts", "", 50041, "Registry", "500412"));

            //  Submenu Reinstate Closed Accounts
            mymenuList.Add(new NavigationMenu(50441, "1 Create Request", "CreateAccountRejoinRequest", "CustomerAccounts", "", 50044, "Registry", "500441"));
            mymenuList.Add(new NavigationMenu(50442, "2 Authorise Request", "AuthoriseAccountRejoinRequest", "CustomerAccounts", "", 50044, "Registry", "500442"));

            //Authorisations/Account Exemptions
            mymenuList.Add(new NavigationMenu(50051, "A Loans Interest Charging Exemptions", "LoanInterestChargingExemption", "AccountExemptions", "", 50050, "Authorisations", "500710"));
            mymenuList.Add(new NavigationMenu(50052, "B Members Exempted from W/Tax", "WTaxExemption", "AccountExemptions", "", 50050, "Authorisations", "500720"));

            //Authorisations/SMS Management
            mymenuList.Add(new NavigationMenu(50061, "A Member Registration", "RegisterMember", "SMSBanking", "", 50060, "Messaging", "50060A"));
            mymenuList.Add(new NavigationMenu(50062, "B Service Requests", "ServiceRequest", "SMSBanking", "", 50060, "Messaging", "50060B"));

            mymenuList.Add(new NavigationMenu(50063, "C Send User Defined Messages", "", "#", "", 50060, "", "5006C0"));
            mymenuList.Add(new NavigationMenu(50631, "1 By Sms Group", "MessageBySmsGroup", "SMSBanking", "", 50063, "Messaging", "5006C1"));
            mymenuList.Add(new NavigationMenu(50632, "2 By Customer Group", "MessageByCustomerGroup", "SMSBanking", "", 50063, "Messaging", "5006C2"));
            mymenuList.Add(new NavigationMenu(50633, "3 Manual List", "MessageByManualList", "SMSBanking", "", 50063, "Messaging", "5006C3"));


            mymenuList.Add(new NavigationMenu(50071, "A Register Members", "UssdRegistration", "USSD", "", 50070, "Messaging", "5006D0"));
            mymenuList.Add(new NavigationMenu(50072, "B Authorise Members", "AuthoriseUssdRegistration", "USSD", "", 50070, "Messaging", "5006D0"));
            mymenuList.Add(new NavigationMenu(50073, "C DeRegister Members", "DeregisterUSSD", "USSD", "", 50070, "Messaging", "5006D0"));


            //Pos module
            mymenuList.Add(new NavigationMenu(50081, "A Device Setup", "Index", "POSDevice", "", 50080, "POS", "500910"));
            mymenuList.Add(new NavigationMenu(50082, "B Open Day", "OpenDay", "POSDeviceManagement", "", 50080, "POS", "500920"));
            mymenuList.Add(new NavigationMenu(50083, "C Close Day", "CloseDay", "POSDeviceManagement", "", 50080, "POS", "500930"));

            mymenuList.Add(new NavigationMenu(50084, "D Device Log", "", "#", "", 50080, "", "500940"));
            mymenuList.Add(new NavigationMenu(50841, "1 All Devices", "DeviceLog", "POSDeviceManagement", "", 50084, "POS", "500940"));
            mymenuList.Add(new NavigationMenu(50842, "2 Single Device", "DeviceLogSingle", "POSDeviceManagement", "", 50084, "POS", "500950"));


            // Authorisation/Authorisation Reports
            mymenuList.Add(new NavigationMenu(51010, "A1 Tellers Authorisations Report", "TellersAuthorisations", "TellersAuthorisationsReport", "", 51000, "AuthorisationReports", "500900", true));
            mymenuList.Add(new NavigationMenu(51011, "A2 Authorised Cheques For Clearing Report", "AuthorisedChequesForClearing", "ChequesForClearingReport", "", 51000, "AuthorisationReports", "500900", true));
            // Authorisation/Notices Reports
            mymenuList.Add(new NavigationMenu(51012, "B Notices Reports", "NoticesReports", "NoticesReport", "", 51000, "AuthorisationReports", "500900", true));
            // Authorisation/ATM Reports
            mymenuList.Add(new NavigationMenu(51013, "C ATM Reports", "OptionListings", "AtmReports", "", 51000, "AuthorisationReports", "500900", true));
            //Authorisation/ SMS Banking Reports
            mymenuList.Add(new NavigationMenu(51014, "D SMS Banking Reports", "OptionListings", "SmsBankingReports", "", 51000, "AuthorisationReports", "500900", true));
            //Electronic Channels (ATM/Mobile) Banking
            mymenuList.Add(new NavigationMenu(51017, "E Electronic Channels (ATM/Mobile) Banking", "ElectronicChannelsBanking", "ElectronicChannelsBanking", "", 51000, "AuthorisationReports", "500900", true));

        }

        private void Accounts(List<NavigationMenu> mymenuList)
        {
            //Accounts
            mymenuList.Add(new NavigationMenu(60010, "Chart Of Accounts", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "6001A0"));
            mymenuList.Add(new NavigationMenu(60020, "Direct Ledger Transactions", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "6002A0"));
            mymenuList.Add(new NavigationMenu(60030, "BOD Allowances", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "600300"));
            mymenuList.Add(new NavigationMenu(60040, "Accounts Cheque Books", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "600400"));
            mymenuList.Add(new NavigationMenu(60050, "Bank Reconciliation", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "600500"));
            mymenuList.Add(new NavigationMenu(60060, "Financial Reports", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "500700"));
            mymenuList.Add(new NavigationMenu(60070, "Regulatory Reports", "", "#", "fa fa-lg fa-fw  fa-home", 60000, "", "500700"));


            //Accounts/Chart Of Accounts
            mymenuList.Add(new NavigationMenu(60011, "A Main accounts Classification", "Index", "GlClassification", "", 60010, "Accounts", "6001A1"));
            mymenuList.Add(new NavigationMenu(60012, "B Accounts SubClassification", "Index", "GlSubClass", "", 60010, "Accounts", "6001A2"));
            mymenuList.Add(new NavigationMenu(60013, "C Accounts SubClass Categorised", "Index", "GlSubCategory", "", 60010, "Accounts", "6001A3"));
            //Accounts/Charts Of Accounts
            mymenuList.Add(new NavigationMenu(60014, "D Chart of Accounts", "Index", "GlAccounts", "", 60010, "Accounts", "6001A4"));
            mymenuList.Add(new NavigationMenu(60015, "E Gl Accounts Transaction Categorisation", "Index", "GlAccounts_Categorised", "", 60010, "Accounts", "6001A9"));
            //Accounts/Branch Apportionment
            mymenuList.Add(new NavigationMenu(60017, "F Branch Apportionment-Branch Settings", "Index", "BranchAportionmentList", "", 60010, "Accounts", "6001A5"));

            mymenuList.Add(new NavigationMenu(60019, "G Suppliers/Debtors Details", "Index", "GlAccountDetails", "", 60010, "Accounts", "6001A6"));
            mymenuList.Add(new NavigationMenu(60191, "H General Ledger Budget", "Budget", "GlBudget", "", 60010, "Accounts", "6001A7"));
            mymenuList.Add(new NavigationMenu(60192, "I Society General Ledger Budget", "SocietyBudget", "GlBudget", "", 60010, "Accounts", "6001A8"));

            //Accounts/Direct Ledger Transactions
            mymenuList.Add(new NavigationMenu(60021, "A Petty Cash Payments", "PettyCashPayments", "DirectLedger", "", 60020, "Accounts", "3009D0"));

            mymenuList.Add(new NavigationMenu(60022, "B Ledger Journals", "", "#", "", 60020, "Accounts", "6002A1"));

            mymenuList.Add(new NavigationMenu(60221, "1 Create Journals", " LedgerJournalList", "DirectLedger", "", 60022, "Accounts", "6002A1"));
            mymenuList.Add(new NavigationMenu(60222, "2 Verify / Post Journals", "LedgerJournalVerifyList", "DirectLedger", "", 60022, "Accounts", "6002A1"));
            mymenuList.Add(new NavigationMenu(60223, "3 View Posted Journals", " LedgerJournalPostedList", "DirectLedger", "", 60022, "Accounts", "6002A1"));

            mymenuList.Add(new NavigationMenu(60025, "C Invoice Receiving", "", "#", "", 60020, "Accounts", "6002A2"));
            mymenuList.Add(new NavigationMenu(60251, "1 Create", "SupplierInvoicesList", "InvoicePayments", "", 60025, "Accounts", "6002A2"));
            mymenuList.Add(new NavigationMenu(60252, "2 Verify / Post ", "SupplierInvoiceVerifyList", "InvoicePayments", "", 60025, "Accounts", "6002A2"));
            mymenuList.Add(new NavigationMenu(60253, "3 View Received Invoices", "SupplierInvoicePostedList", "InvoicePayments", "", 60025, "Accounts", "6002A2"));

            mymenuList.Add(new NavigationMenu(60026, "D Invoice Payments", "", "#", "", 60020, "", "6002A3"));
            mymenuList.Add(new NavigationMenu(60261, "1 Create ", "SupplierPaymentList", "SupplierInvoicePayments", "", 60026, "Accounts", "6002A3"));
            mymenuList.Add(new NavigationMenu(60262, "2 Verify / Post ", "SupplierPaymentVerifyList", "SupplierInvoicePayments", "", 60026, "Accounts", "6002A3"));
            mymenuList.Add(new NavigationMenu(60263, "3 View Paid Invoices ", "SupplierPaymentPostedList", "SupplierInvoicePayments", "", 60026, "Accounts", "6002A3"));
            //Accounts/Teller Payments

            mymenuList.Add(new NavigationMenu(60027, "E Teller Payments", "", "#", "", 60020, "", "6002A4"));
            //Accounts/Teller Payments Sub Menu
            mymenuList.Add(new NavigationMenu(60271, "1 Create ", "ListOfPayments", "TellerPayments", "", 60027, "Accounts", "6002A4"));
            mymenuList.Add(new NavigationMenu(60272, "2 Verify ", "ListOfBatchPayments", "TellerPayments", "", 60027, "Accounts", "6002A4"));
            mymenuList.Add(new NavigationMenu(60273, "3 View Paid ", "ListOfPostedPayments", "TellerPayments", "", 60027, "Accounts", "6002A4"));

            //Accounts/Debtors Invoices
            mymenuList.Add(new NavigationMenu(60028, "F Debtors Invoices", "", "#", "", 60020, "", "6002E1"));
            mymenuList.Add(new NavigationMenu(60281, "1 Create ", "DebtorInvoicesList", "DebtorsInvoice", "", 60028, "Accounts", "6002E1"));
            mymenuList.Add(new NavigationMenu(60282, "2 Verify / Post ", "DebtorsInvoiceVerifyList", "DebtorsInvoice", "", 60028, "Accounts", "6002E1"));
            mymenuList.Add(new NavigationMenu(60283, "3 View Paid Invoices ", "DebtorsInvoicePostedList", "DebtorsInvoice", "", 60028, "Accounts", "6002E1"));
            //Accounts/Manual PayBill Transactions
            mymenuList.Add(new NavigationMenu(60031, "G Manual PayBill Transactions", "", "#", "", 60020, "", "6002F1"));
            mymenuList.Add(new NavigationMenu(60311, "1 Create ", "UnclosedPayBills", "ManualPayBillTransaction", "", 60031, "Accounts", "6002F1"));
            mymenuList.Add(new NavigationMenu(60312, "2 Verify / Post ", "UnVerifiedManualPayBillTransations", "ManualPayBillTransaction", "", 60031, "Accounts", "6002F1"));
            mymenuList.Add(new NavigationMenu(60313, "3 View Paid Invoices ", "PostedPayBills", "ManualPayBillTransaction", "", 60031, "Accounts", "6002F1"));
            //Accounts/BOD Allowances/Setup
            mymenuList.Add(new NavigationMenu(60032, "A Allowance Rates", "BodCommittees", "BodAllowances", "", 60030, "Accounts", "600301"));
            mymenuList.Add(new NavigationMenu(60033, "B Attach Committee Members", "BodCommittees", "BodCommitteeMembers", "", 60030, "Accounts", "600302"));
            //Accounts/BOD Allowances/claims
            mymenuList.Add(new NavigationMenu(60034, "C Raise Claims", "", "#", "", 60030, "", "600303"));
            mymenuList.Add(new NavigationMenu(60341, "1 Create", "RaisedClaims", "BodRaiseClaims", "", 60034, "Accounts", "600303"));
            mymenuList.Add(new NavigationMenu(60342, "2 Verify /Post ", "VerifiedPostedClaims", "BodRaiseClaims", "", 60034, "Accounts", "600306"));
            mymenuList.Add(new NavigationMenu(60343, "3 View Posted ", "ViewPostedClaims", "BodRaiseClaims", "", 60034, "Accounts", "600307"));
            //Accounts/BOD Allowances /Reports
            mymenuList.Add(new NavigationMenu(60326, "D Reports", "OptionListing", "BodReports", "", 60030, "Accounts", "600307"));

            //Accounts/Accounts Cheque Books/ Sub Menu
            mymenuList.Add(new NavigationMenu(60041, "A Register Cheque Book", "Index", "RegisteredChequeBooks", "", 60040, "Accounts", "600401"));
            mymenuList.Add(new NavigationMenu(60042, "B Cheques Issued Report", "ChequeBooksUsedListing", "ChequeBooksUsed", "", 60040, "Accounts", "600403"));

            //Accounts/Bank Reconciliation SubMenu
            mymenuList.Add(new NavigationMenu(60051, "A Reconciliation Data", "", "#", "", 60050, "", "600510"));
            mymenuList.Add(new NavigationMenu(60511, "1 Reconciliation Period ", "Index", "BankReconciliationPeriod", "", 60051, "Accounts", "600511"));
            mymenuList.Add(new NavigationMenu(60512, "2 Import CashBokk Data", "Index", "ImportCashBookData", "", 60051, "Accounts", "600512"));
            mymenuList.Add(new NavigationMenu(60513, "3 Bank Statements Details", "", "#", "", 60051, "", "600513"));
            //Accounts/Bank Reconciliation
            mymenuList.Add(new NavigationMenu(60052, "B Bank Reconciliation", "", "#", "", 60050, "", "600520"));
            //Accounts/Bank Reconciliation/reports
            mymenuList.Add(new NavigationMenu(60053, "C Reports", "", "#", "", 60050, "", "600500"));
            mymenuList.Add(new NavigationMenu(60531, "1 Items Missing In CashBook", "", "#", "", 60053, "", "600500"));
            mymenuList.Add(new NavigationMenu(60532, "2 Items Missing In Bank Statement", "", "#", "", 60053, "", "600500"));
            mymenuList.Add(new NavigationMenu(60533, "3 CashBook & Bank Statement Amount Differs", "", "#", "", 60053, "", "600500"));
            mymenuList.Add(new NavigationMenu(60534, "4 Bank Reconciliation report", "", "#", "", 60053, "", "600500"));

            //Accounts/Financial Reports

            mymenuList.Add(new NavigationMenu(60061, "A Single GL Transactions", "", "#", "", 60060, "", "600811"));
            mymenuList.Add(new NavigationMenu(60611, "1.Per Branch ", "SingleGlReportPerBranch", "SingleGlReportPerBranch", "", 60061, "Accounts", "600811"));
            mymenuList.Add(new NavigationMenu(60612, "2.All Branches ", "SingleGlReportAllBranches", "SingleGlReportAllBranches", "", 60061, "Accounts", "600812"));

            mymenuList.Add(new NavigationMenu(60062, "B Transaction Audit Trail", "TransactionAuditTrail", "TransactionAuditTrail", "", 60060, "Accounts", "600820"));

            mymenuList.Add(new NavigationMenu(60063, "C Trial Balance", "", "#", "", 60060, "", "600830"));
            mymenuList.Add(new NavigationMenu(60631, "1.Society Trial Balance", "SocietyTrialBalance", "FinancialReports", "", 60063, "Accounts", "600831"));
            mymenuList.Add(new NavigationMenu(60632, "2.Branch Trial Balance", "BranchTrialBalance", "FinancialReports", "", 60063, "Accounts", "600832"));



            mymenuList.Add(new NavigationMenu(60064, "D Income & Expenditure", "", "#", "", 60060, "", "600840"));
            mymenuList.Add(new NavigationMenu(60641, "1.Branch Statement", "BranchStatement", "FinancialReports", "", 60064, "Accounts", "600841"));
            mymenuList.Add(new NavigationMenu(60642, "2.Society Statement", "SocietyStatement", "FinancialReports", "", 60064, "Accounts", "600842"));
            mymenuList.Add(new NavigationMenu(60643, "3.Consolidated Statement ", "", "FinancialReports", "", 60064, "Accounts", "600843"));

            mymenuList.Add(new NavigationMenu(60065, "E Cash & Bank Balances Report", "CashBankBalance", "FinancialReports", "", 60060, "Accounts", "600850"));
            mymenuList.Add(new NavigationMenu(60066, "F Mpa Vs Ledger Comparisons", "MPAVsLedgerReport", "FinancialReports", "", 60060, "Accounts", "600860"));
            mymenuList.Add(new NavigationMenu(60067, "G Cash Flow Statement", "CashStatement", "FinancialReports", "", 60060, "Accounts", "600870"));


            //Accounts/Regulatory Reports
            mymenuList.Add(new NavigationMenu(60071, "A Regulatory Reports Setup", "ReportSetup", "RegulatoryReports", "", 60070, "Accounts", "6009A1"));
            mymenuList.Add(new NavigationMenu(60072, "B Reporting", "SasraReport", "RegulatoryReports", "", 60070, "Accounts", "6009A2"));
            mymenuList.Add(new NavigationMenu(60073, "C Risk Classification", "", "#", "", 60070, "", ""));
            mymenuList.Add(new NavigationMenu(60074, "D Insider Lending", "", "#", "", 60070, "", ""));
            mymenuList.Add(new NavigationMenu(60075, "E Statement of Deposit Return", "", "#", "", 60070, "", "6009D0"));
        }

        private void HumanResource(List<NavigationMenu> mymenuList)
        {
            //Human Resource
            mymenuList.Add(new NavigationMenu(70010, "General Settings", "", "#", "fa fa-lg fa-fw  fa-home", 70000, "", "7001A0"));
            mymenuList.Add(new NavigationMenu(70020, "Employee Register", "", "#", "fa fa-lg fa-fw  fa-home", 70000, "", "7002A0"));
            mymenuList.Add(new NavigationMenu(70030, "Leave Management", "", "#", "fa fa-lg fa-fw  fa-home", 70000, "", "7002C0"));
            mymenuList.Add(new NavigationMenu(70040, "Off Payroll Claims", "", "#", "fa fa-lg fa-fw  fa-home", 70000, "", "700400"));
            mymenuList.Add(new NavigationMenu(70050, "Payroll", "", "#", "fa fa-lg fa-fw  fa-home", 70000, "", "7008A0"));
            mymenuList.Add(new NavigationMenu(70060, "Reports", "Index", "Reports", "fa fa-lg fa-fw  fa-home", 70000, "HumanResource", "708Z00"));

            //Human Resource/General Settings
            mymenuList.Add(new NavigationMenu(70011, "A Earning & Deduction Codes", "index", "EarningAndDeductionCodes", "", 70010, "HumanResource", "7001A1"));
            mymenuList.Add(new NavigationMenu(70012, "B Job Groups", "index", "JobGroups", "", 70010, "HumanResource", "7001B0"));
            mymenuList.Add(new NavigationMenu(70013, "C NHIF Table", "index", "NHIFTable", "", 70010, "HumanResource", "7001C1"));
            mymenuList.Add(new NavigationMenu(70014, "D PAYE Table", "index", "PAYETable", "", 70010, "HumanResource", "7001D1"));

            mymenuList.Add(new NavigationMenu(70015, "E Payroll Earnings Setup", "", "#", "", 70010, "HumanResource", "7001E0"));

            mymenuList.Add(new NavigationMenu(70151, "1 On Payroll Earnings", "index", "OnPayrollEarning", "", 70015, "HumanResource", "7001E1"));
            mymenuList.Add(new NavigationMenu(70152, "2 Off Payroll Earnings Codes", "codes", "OffPayrollEarnings", "", 70015, "HumanResource", "7001E2"));
            mymenuList.Add(new NavigationMenu(70153, "3 Off Payroll Earnings Rates", "Rates", "OffPayrollEarnings", "", 70015, "HumanResource", "7001E1"));

            mymenuList.Add(new NavigationMenu(70018, "F Payroll Gl Linkage", "index", "PayrollGLLinkage", "", 70010, "HumanResource", "7001Z0"));

            //Human Resource/Employee Register
            mymenuList.Add(new NavigationMenu(70021, "A Employee Register", "index", "EmployeeRegister", "", 70020, "HumanResource", "7002A1"));
            mymenuList.Add(new NavigationMenu(70022, "B Employee Absenteeism", "EmployeeAbsenteeismIndex", "EmployeeRegister", "", 70020, "HumanResource", "7002A2"));

            //Human Resource/Leave Management
            mymenuList.Add(new NavigationMenu(70031, "A Leave Types", "index", "LeaveTypes", "", 70030, "HumanResource", "7002C1"));
            mymenuList.Add(new NavigationMenu(70032, "B Annual Leave Schedule", "index", "AnnualLeaveSchedules", "", 70030, "HumanResource", "7002C2"));

            mymenuList.Add(new NavigationMenu(70033, "C Leave Process", "", "#", "", 70030, "HumanResource", "7002C0"));
            mymenuList.Add(new NavigationMenu(70331, "1 Application", "index", "LeaveApplication", "", 70033, "HumanResource", "7002C3"));
            mymenuList.Add(new NavigationMenu(70332, "2 Supervisor Approval", "index", "SupervisorApproval", "", 70033, "HumanResource", "7002C4"));
            mymenuList.Add(new NavigationMenu(70333, "3 HR Approval", "index", "HrApproval", "", 70033, "HumanResource", "7002C5"));

            mymenuList.Add(new NavigationMenu(70034, "D Leave Reports", "", "#", "", 70030, "HumanResource", "7002CZ"));
            mymenuList.Add(new NavigationMenu(70341, "1 Leave Status Report", "index", "LeaveStatusReport", "", 70034, "HumanResource", "7002CZ"));
            mymenuList.Add(new NavigationMenu(70342, "2 Leave Statement", "index", "LeaveStatement", "", 70034, "HumanResource", "7002CZ"));
            mymenuList.Add(new NavigationMenu(70343, "3 Leave Balances", "index", "", "", 70034, "HumanResource", "7002CZ"));
            mymenuList.Add(new NavigationMenu(70344, "4 Leave Taken SpreadSheet", "index", "", "", 70034, "HumanResource", "7002CZ"));
            //Human Resource/Off Payroll Claims
            mymenuList.Add(new NavigationMenu(70041, "A Raise Claim", "index", "RaiseClaims", "", 70040, "HumanResource", "700401"));
            mymenuList.Add(new NavigationMenu(70042, "B Verify/Post Claims", "RaisedClaims", "RaiseClaims", "", 70040, "HumanResource", "700402"));
            mymenuList.Add(new NavigationMenu(70043, "C View Posted Claims", "PostedClaims", "RaiseClaims", "", 70040, "HumanResource", "700403"));

            //Human Resource/Payroll
            mymenuList.Add(new NavigationMenu(70051, "A Import Data from Main System", "", "#", "", 70050, "", "7008A1"));
            mymenuList.Add(new NavigationMenu(70052, "B Payroll Changes", "", "#", "", 70050, "", "7008A2"));
            mymenuList.Add(new NavigationMenu(70053, "C Payroll Processing", "", "#", "", 70050, "", "7008A3"));
            mymenuList.Add(new NavigationMenu(70054, "D Posting to Accounts", "", "#", "", 70050, "HumanResource", "7008A4"));

            //Human Resource Reports            
            mymenuList.Add(new NavigationMenu(70061, "A Payslips", "OptionListings", "PayslipsReports", "", 70060, "HumanResource", "708Z01", true));
            mymenuList.Add(new NavigationMenu(70062, "B Credit Transfer List", "", "#", "", 70060, "HumanResource", "708Z02", true));
            mymenuList.Add(new NavigationMenu(70063, "C Payroll Analysis", "", "#", "", 70060, "HumanResource", "708Z03", true));
            /* 
             *Add the below reports to a drop down.
            mymenuList.Add(new NavigationMenu(70631, "1 Summarised Report", "", "#", "", 70063, "HumanResource", "708Z03"));
            mymenuList.Add(new NavigationMenu(70632, "2 Detailed Report", "", "#", "", 70063, "HumanResource", "708Z03"));
            */
            mymenuList.Add(new NavigationMenu(70065, "D Code Analysis", "", "#", "", 70060, "HumanResource", "708Z04", true));
            mymenuList.Add(new NavigationMenu(70066, "E End Year Reports", "", "#", "", 70060, "HumanResource", "708Z09", true));
        }

        private void AssetRegister(List<NavigationMenu> mymenuList)
        {
            //Asset Register
            mymenuList.Add(new NavigationMenu(80010, "General Settings", "", "#", "fa fa-lg fa-fw  fa-home", 80000, "", "800100"));
            mymenuList.Add(new NavigationMenu(80020, "Asset Register", "AssetRegisterList", "AssetRegister", "fa fa-lg fa-fw  fa-home", 80000, "AssetRegister", "800200"));
            mymenuList.Add(new NavigationMenu(80030, "Transactions", "", "#", "fa fa-lg fa-fw  fa-home", 80000, "", "800300"));
            mymenuList.Add(new NavigationMenu(80040, "Reports", "Index", "Reports", "fa fa-lg fa-fw  fa-home", 80000, "AssetRegister", ""));

            //Asset Register/General Settings
            mymenuList.Add(new NavigationMenu(80011, "A Asset Categories", "Index", "AssetRegister", "", 80010, "AssetRegister", "800110"));
            mymenuList.Add(new NavigationMenu(80012, "B Asset Sub Categories", "AssetSubCategoryList", "AssetRegister", "", 80010, "AssetRegister", "800120"));
            //Asset Register/Transactions
            mymenuList.Add(new NavigationMenu(80031, "A Charge Depreciation", "ChargeDepreciation", "AssetRegister", "", 80030, "AssetRegister", "800310"));
            //reports
            mymenuList.Add(new NavigationMenu(80032, "A Single Asset Statement", "SingleAssetStatement", "Reports", "", 80040, "AssetRegister", "800320", true));
            mymenuList.Add(new NavigationMenu(80033, "B Assets register report", "Assetsregister", "Reports", "", 80040, "AssetRegister", "800330", true));
            mymenuList.Add(new NavigationMenu(80034, "C Assets depreciation report", "AssetsDepreciation", "Reports", "", 80040, "AssetRegister", "800340", true));
        }


        private void StockRegister(List<NavigationMenu> mymenuList)
        {
            //Stock Register
            mymenuList.Add(new NavigationMenu(90010, "General Settings", "", "#", "fa fa-lg fa-fw  fa-home", 90000, "", "900100"));
            mymenuList.Add(new NavigationMenu(90020, "Stock Register", "Index", "StockRegister", "fa fa-lg fa-fw  fa-home", 90000, "StockRegister", "900200"));
            mymenuList.Add(new NavigationMenu(90030, "Receipts/Returns to Suppliers", "", "#", "fa fa-lg fa-fw  fa-home", 90000, "", "900310"));
            mymenuList.Add(new NavigationMenu(90040, "Issues/Returns from Branches", "", "#", "fa fa-lg fa-fw  fa-home", 90000, "", "900320"));
            mymenuList.Add(new NavigationMenu(90050, "Issues/Returns from Staff", "", "#", "fa fa-lg fa-fw  fa-home", 90000, "", "900330"));
            mymenuList.Add(new NavigationMenu(90060, "Reports", "Index", "Reports", "fa fa-lg fa-fw  fa-home", 90000, "StockRegister", ""));

            //Stock Register/General Settings
            mymenuList.Add(new NavigationMenu(90011, "A Stock Categories", "Index", "StockCategory", "", 90010, "StockRegister", "900110"));
            mymenuList.Add(new NavigationMenu(90012, "B Stock SubCategories", "Index", "StockSubCategory", "", 90010, "StockRegister", "900120"));
            //Stock Transactions/Receipts/Returns to Suppliers
            mymenuList.Add(new NavigationMenu(90031, "A Create Batches", "Index", "ReceiptsReturnsToSuppliers", "", 90030, "StockRegister", "900311"));
            mymenuList.Add(new NavigationMenu(90032, "B Authorise/Transfer", "AuthoriseTransfer", "ReceiptsReturnsToSuppliers", "", 90030, "StockRegister", "900315"));
            mymenuList.Add(new NavigationMenu(90033, "C View Posted", "PosteBatches", "ReceiptsReturnsToSuppliers", "", 90030, "StockRegister", "900319"));
            //Stock Transactions/Issues/Returns from Branches
            mymenuList.Add(new NavigationMenu(90041, "A Create Batches", "Index", "IssuedReturnsToBranches", "", 90040, "StockRegister", "900321"));
            mymenuList.Add(new NavigationMenu(90042, "B Authorise/Transfer", "AuthoriseTransfer", "IssuedReturnsToBranches", "", 90040, "StockRegister", "900325"));
            mymenuList.Add(new NavigationMenu(90043, "C View Posted", "PostedBatches", "IssuedReturnsToBranches", "", 90040, "StockRegister", "900329"));
            //Stock Transactions/Issues/Returns from Staff
            mymenuList.Add(new NavigationMenu(90051, "A Create Batches", "Index", "IssuedReturnedFromStaff", "", 90050, "StockRegister", "900331"));
            mymenuList.Add(new NavigationMenu(90052, "B Authorise/Transfer", "AuthoriseTransfer", "IssuedReturnedFromStaff", "", 90050, "StockRegister", "900335"));
            mymenuList.Add(new NavigationMenu(90053, "C View Posted", "PostedBatches", "IssuedReturnedFromStaff", "", 90050, "StockRegister", "900339"));
            //reports
            mymenuList.Add(new NavigationMenu(90054, "A Stock Register By Category", "StockRegisterBySubCatReport", "Reports", "", 90060, "StockRegister", "500900", true));
            mymenuList.Add(new NavigationMenu(90055, "B Receipts/Returns To Suppliers By Sub-Category", "ReceiptsReturnsToSuppliersBySubCatReport", "Reports", "", 90060, "StockRegister", "500900", true));
            mymenuList.Add(new NavigationMenu(90056, "C Issues/Returns From Branches By Sub-Category", "IssuesReturnsFromBranchesBySubCatReport", "Reports", "", 90060, "StockRegister", "500900", true));
            mymenuList.Add(new NavigationMenu(90057, "D Issues/Returns From Staff By Sub-Category", "IssuesReturnsFromStaffBySubCatReport", "Reports", "", 90060, "StockRegister", "500900", true));
            mymenuList.Add(new NavigationMenu(90058, "E Single stock Itemstatement", "SinglestockItemstatement", "Reports", "", 90060, "StockRegister", "500900", true));
        }


        private void PeriodicProcedures(List<NavigationMenu> mymenuList)
        {

            mymenuList.Add(new NavigationMenu(98001, "Shares Periodic Procedures", "", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980100"));
            mymenuList.Add(new NavigationMenu(98002, "Loans Periodic Procedures", "", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980200"));
            mymenuList.Add(new NavigationMenu(98003, "Savings Periodic Procedures", "", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980300"));

            mymenuList.Add(new NavigationMenu(98005, "Close Day Procedures", "Index", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980600"));
            mymenuList.Add(new NavigationMenu(98007, "Yearly Procedures", "", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980700"));
            mymenuList.Add(new NavigationMenu(98008, "System Maintenance", "Index", "#", "fa fa-lg fa-fw  fa-home", 98000, "", "980900"));

            //Periodic Procedures/Shares  Periodic Procedures
            mymenuList.Add(new NavigationMenu(98011, "A Transfer Investment (Fixed Amount)", "FixedAmount", "SharesTransfer", "", 98001, "PeriodicProcedures", "980110"));
            mymenuList.Add(new NavigationMenu(98012, "B Transfer Deposits to Shares", "DepositToShares", "SharesTransfer", "", 98001, "PeriodicProcedures", "980120"));
            mymenuList.Add(new NavigationMenu(98013, "C Recover Negative Investment from Savings", "RecoverNegativeInvestments", "SavingsRecovery", "", 98001, "PeriodicProcedures", "980130"));
            mymenuList.Add(new NavigationMenu(98014, "D Shares/Investments Ledger Fee", "InvestmentLedgerFeeCharging", "SharesTransfer", "", 98001, "PeriodicProcedures", "980140"));
            mymenuList.Add(new NavigationMenu(98015, "E Load Penalty (Investments Arrears)", "InvestmentPenaltyCharging", "SharesTransfer", "", 98001, "PeriodicProcedures", "980150"));


            //Periodic Procedures/Loans Periodic Procedures
            mymenuList.Add(new NavigationMenu(98021, "A Charge Interest", "LoansInterestCharging", "LoanPeriodicProcedures", "", 98002, "PeriodicProcedures", "980210"));
            mymenuList.Add(new NavigationMenu(98022, "B Charge Ledger Fee", "LoansLedgerFeeCharging", "LoanPeriodicProcedures", "", 98002, "PeriodicProcedures", "980220"));
            mymenuList.Add(new NavigationMenu(98023, "C Penalty Charging", "LoansPenaltyCharging", "LoanPeriodicProcedures", "", 98002, "PeriodicProcedures", "980240"));
            mymenuList.Add(new NavigationMenu(98024, "D Special Deductions", "LoansInterestDeductFromPrinciple", "LoanPeriodicProcedures", "", 98002, "PeriodicProcedures", "980230"));

            //Periodic Procedures/Savings Periodic Procedures
            mymenuList.Add(new NavigationMenu(98031, "A Savings Interest", "MonthlySavingsInterest", "SavingsPeriodic", "", 98003, "PeriodicProcedures", "980310"));
            mymenuList.Add(new NavigationMenu(98032, "B Savings Ledger Fee", "SavingsLedgerFee", "SavingsPeriodic", "", 98003, "PeriodicProcedures", "980320"));
            mymenuList.Add(new NavigationMenu(98039, "Z Recover Arrears", "", "#", "", 98003, "PeriodicProcedures", "980330"));


            //Yearly Procedures

            mymenuList.Add(new NavigationMenu(98071, "1. Dividend Processing", "", "#", "", 98007, "", "9802A1"));
            mymenuList.Add(new NavigationMenu(98072, "2. Post Savings Interest", "PostSavingsInterest", "SavingsPeriodic", "", 98007, "PeriodicProcedures", "9802B1"));

            //Yearly Procedures/Dividend Processing
            mymenuList.Add(new NavigationMenu(98711, "A Balance As At", "DividendsBalanceAsAt", "DividendProcessing", "", 98071, "PeriodicProcedures", "9802A1"));
            mymenuList.Add(new NavigationMenu(98712, "B Prorata Basis", "ProrataDividends", "DividendProcessing", "", 98071, "PeriodicProcedures", "9802A2"));
            mymenuList.Add(new NavigationMenu(98713, "C View Posted (Balance As At)", "BalanceAsAtTransferViewPosted", "DividendProcessing", "", 98071, "PeriodicProcedures", "9802A1"));
            mymenuList.Add(new NavigationMenu(98714, "D View Posted (Prorata)", "ProrataViewPosted", "DividendProcessing", "", 98071, "PeriodicProcedures", "9802A1"));

        }

        private void SystemSettings(List<NavigationMenu> mymenuList)
        {
            //System Settings
            mymenuList.Add(new NavigationMenu(99010, "User Administration", "", "#", "fa fa-lg fa-fw  fa-home", 99000, "", "990100"));
            mymenuList.Add(new NavigationMenu(99020, "Change User Password", "Index", "ChangeUserPassword", "", 99000, "SystemSettings", "990100"));
            mymenuList.Add(new NavigationMenu(99030, "Company Settings", "Index", "CompanySetting", "fa fa-lg fa-fw  fa-home", 99000, "SystemSettings", "990300"));

            //System Settings/User Administration
            mymenuList.Add(new NavigationMenu(99011, "A User Administration", "Index", "UserAdministration", "", 99010, "SystemSettings", "990110"));
            mymenuList.Add(new NavigationMenu(99012, "B User Roles", "Index", "UserRoles", "", 99010, "SystemSettings", "990121"));
            mymenuList.Add(new NavigationMenu(99013, "C Assign Rights to Roles", "Index", "AssignRightsToRoles", "", 99010, "SystemSettings", "990124"));
            mymenuList.Add(new NavigationMenu(99014, "D Assign User Rights", "Index", "AssignUserRights", "", 99010, "SystemSettings", "990126"));
            mymenuList.Add(new NavigationMenu(99015, "E Assign Users to Roles", "Index", "AssignUserToRole", "", 99010, "SystemSettings", "990128"));


            //System Settings/User Administration
            mymenuList.Add(new NavigationMenu(99016, "A Password Settings", "UserPasswordSettings", "UserAdministration", "", 99010, "SystemSettings", "990130"));
            mymenuList.Add(new NavigationMenu(99017, "B Teller Accounts", "Index", "TellerAccounts", "", 99010, "SystemSettings", "990140"));
        }

    }
}

