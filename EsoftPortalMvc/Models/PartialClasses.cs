using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Models
{
    public class UserDetailsView
    {
        public Guid tbl_usersID { get; set; }
        public string LoginCode { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }
        public string UserBranch { get; set; }
        public string LoginTime_From { get; set; }
        public string LoginTime_To { get; set; }
        public string LoginMachine { get; set; }
        public string LoginPassword { get; set; }
        public bool AccountDisabled { get; set; }
        public string AccessRights { get; set; }
        public decimal? TellerAuthorisationLimit { get; set; }
        public DateTime? PasswordExpiry { get; set; }
        public DateTime? Login_Expiry { get; set; }
        public decimal? LoanApprovalLimit { get; set; }
        public decimal? LoanDisbursementLimit { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserRole { get; set; }
        public string Teller_Excess_GlAccount { get; set; }
        public string Teller_Shortage_GlAccount { get; set; }
        public string EmploymentNumber { get; set; }
        public string SaccoMembershipNumber { get; set; }
        public decimal? CashWithdrawalAlertAmount { get; set; }
        public string OtherUserRoles { get; set; }
        public string BranchExciseDutyGlAccount { get; set; }
        public string MembersChequeSuspense_Account { get; set; }
        //tbl_TellerAccounts
        public string TellerAccountNo { get; set; }
        public string AuthorisedBranch { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
    }
    public class AutoCompleteItem
    {
        public AutoCompleteItem()
        {
        }

        public AutoCompleteItem(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        public string label { get; set; }
        public string value { get; set; }
    }
    public class NavMenuViewModel
    {
        public List<AutoCompleteItem> NavgationList { get; set; }
        public string selectedMenu { get; set; }
        public string menuController { get; set; }
        public string menuAction { get; set; }
        public string menuArea { get; set; }

    }
    public class CompanySettingViewModel
    {
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Enter Company Name ")]
        public String CompanyName { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Company's Address ")]
        public String Address { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Company's Email ")]
        public String Email { get; set; }
        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "Enter Company's Telephone")]
        public String Telephone { get; set; }
        [Display(Name = "KRA Pin No")]
        [Required(ErrorMessage = "Enter Company's KRA Pin No")]
        public String KraPinNo { get; set; }
        [Display(Name = "Cash Deposit")]
        [Required(ErrorMessage = "Enter Cash Deposit Authorisation Limit")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? AuthorisedCashDeposit { get; set; }
        [Required(ErrorMessage = "Enter Cash W/Drawal Authorisation Limit")]
        [Display(Name = "Cash W/Drawal")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? AuthorisedCashWithDrw { get; set; }
        [Required(ErrorMessage = "Enter Cash W/Drawal Authorisation Limit")]
        [Display(Name = "Chq deposit")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]

        public Decimal? AuthorisedChqDeposit { get; set; }
        [Required(ErrorMessage = "Enter Cash W/Drawal Authorisation Limit")]
        [Display(Name = "Cash Receipt")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? AuthorisedCashReceipt { get; set; }
        [Required(ErrorMessage = "Enter Cash Payment Authorisation Limit")]
        [Display(Name = "Cash Payments")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? AuthorisedCashPayments { get; set; }
        [Display(Name = "Teller Insurance Limit")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? TellerInsuranceLimit { get; set; }
        [Display(Name = "Teller Comm. Split")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? TellerCommSplit { get; set; }
        [Display(Name = "Cheque Commission Charged On Deposit?")]
        public Boolean? ChargeChequeDepositOnDeposit { get; set; }
        [Display(Name = "Transfer Cash To Treasury On Close Day ?")]
        public Boolean? TransferCashToTreasuryOnCloseDay { get; set; }
        [Display(Name = "Dormancy Period")]
        public Decimal? DormancyPeriod { get; set; }
        [Display(Name = "Slip Footer Text")]
        public String SlipFooterText { get; set; }
        [Display(Name = "Stamp Duty Charge")]
        public Decimal? StampDutyCharge { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "Excise Duty")]
        public Decimal? ExciseDuty { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "W/Tax Rate")]
        public Decimal? WithHoldingTaxRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Decimal? OrdinaryChequeCommission { get; set; }
        public String OrdinaryChequeCommissionGl { get; set; }

        [Display(Name = "Sacco Charge")]
        public Decimal? BankersChequeSaccoCharge { get; set; }
        [Display(Name = "Bank Charge")]
        public Decimal? BankersChequeBankCharge { get; set; }
        public String BankersChequeGl { get; set; }

        [Display(Name = "Sacco Charge")]
        public Decimal? AtmLinkChargeSacco { get; set; }
        [Display(Name = "Pin Regenaration Charge")]
        public Decimal? AtmPinRegerationCharge { get; set; }
        public String AtmCommissionChargeGl { get; set; }

        [Display(Name = "Coop Bank Charge")]
        public Decimal? AtmLinkChargeBank { get; set; }
        public String AtmSettlementGlAccount { get; set; }
        public Boolean? CaptureManualLnNo { get; set; }

        public Boolean? LoanAppraisalCaptureGuarantor { get; set; }
        public int? MaximumLoanToGuarantee { get; set; }
        [Display(Name = "Summarise Ledger Posting ?")]
        public Boolean? SummariseLedgerPosting { get; set; }
        [Display(Name = "System Auto Lock Time ")]
        public int? SystemOutLockTime { get; set; }
        [Display(Name = "Screen Message ")]
        public String ScreenMessage { get; set; }
        public Guid CompanyId { get; set; }

        // New Fields
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "Large Cash Deposit/Withdrawal")]
        public Decimal? RegulatorCashAuthorisedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "Retirement Bonus")]
        public Decimal? RetirementBonus { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Display(Name = "Daily Atm Withdrawal Limit")]
        public Decimal? DailyAtmWithDrawalLimit { get; set; }

        public List<GlAccountsView> GlAccounts { get; set; }
    }
    public class GlAccountsView
    {
        public string GlAccountNo { get; set; }
        public string GlName { get; set; }
        public double GlBalance { get; set; }
        public bool? AccountBlocked { get; set; }
        public string GlAccountNo2 { get; set; }// GlMemsav etc
    }
    public class ModelErrorView
    {
        public string Key { get; set; }
        public string[] Errors { get; set; }
    }
    public class SingleTellerRecord
    {
        public long Recno { get; set; }

        [Display(Name = "User Code")]
        public string LoginCode { get; set; }

        [Display(Name = "Shortage Account")]
        [Required(ErrorMessage = "Shortage Account is required.")]
        public String ShortageAccount { get; set; }

        [Display(Name = "Excess Account")]
        [Required(ErrorMessage = "Excess Account is required.")]
        public String ExcessAcount { get; set; }

        [Display(Name = "Tellers Account")]
        [Required(ErrorMessage = "Teller Account is required.")]
        public string TellerAccountNo { get; set; }

        [Display(Name = "Teller Branch")]
        public string AuthorisedBranch { get; set; }

        public Guid TellerAccountsID { get; set; }
        public Guid UserRecordId { get; set; }

    }

    public class TellerViewModel
    {
        public List<tbl_users> Users { get; set; }

        [Display(Name = "Teller")]
        [Required(ErrorMessage = "Teller is required.")]
        public string User { get; set; }
               
        [Display(Name = "Branch")]
        [Required(ErrorMessage = "Branch is required.")]
        public string Branch { get; set; }

        public IEnumerable<GlAccountsView> TellerAccounts { get; set; }
        [Display(Name = "Teller Account")]
        [Required(ErrorMessage = "Teller Account is required.")]
        public string TellerAccount { get; set; }

        public IEnumerable<GlAccountsView> ExcessAccounts { get; set; }
        [Display(Name = "Excess Account")]
        [Required(ErrorMessage = "Excess Account is required.")]
        public string ExcessAccount { get; set; }

        public IEnumerable<GlAccountsView> ShortageAccounts { get; set; }
        [Display(Name = "Shortage Account")]
        [Required(ErrorMessage = "Shortage Account is required.")]
        public string ShortageAccount { get; set; }
    }
    public class SingleTellerViewModel
    {
        public SingleTellerRecord SingleTellerRecord { get; set; }
        public List<SelectListItem> TellerAccounts { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<GlAccountsView> GlAccounts { get; set; }
    }
    public class TellerAccountView
    {
        public string TellerAccountNo { get; set; }
        public string AuthorisedBranch { get; set; }
        public string LoginCode { get; set; }
    }
    public class TellersAccountsViewModel
    {
        public List<TellersAccountsDetails> TellerAccount { get; set; }
        public string BranchName { get; set; }
        public string TellerFullName { get; set; }

    }

    public class TellersAccountsDetails
    {
        public string TellerAccountsID { get; set; }
        public String Code { get; set; }
        public String GlAccount { get; set; }
        public String TellerName { get; set; }
        public String BranchName { get; set; }
    }

    public class UserDetail_PartialDetails
    {
        public Guid tbl_usersID { get; set; }
        public string LoginCode { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public decimal? LoanApprovalLimit { get; set; }
        public decimal? TellerAuthorisationLimit { get; set; }
    }

    public class UserRole
    {
        public String RoleCode { get; set; }
        public String RoleName { get; set; }
        public String RoleRights { get; set; }
        public Guid UserRoleId { get; set; }

    }
    public class UserAdministrationViewModel
    {
        public tbl_users user { get; set; }
        public List<BranchSettings> branchSettings { get; set; }
        public List<UserRole> userRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public UserRole SingleUserRole { get; set; }
        public List<UserRightsAssignment> RoleRights { get; set; }
    }

    public class SelectedRoleRights
    {
        public String RoleRights { get; set; }
    }
    public class SelectedOtherRoles
    {
        public String OtherRoles { get; set; }
    }
    public class SelectedUserRights
    {
        public String UserRights { get; set; }
    }
    public class UserRoleAssignment
    {
        public String RoleCode { get; set; }
        public String RoleName { get; set; }
        public bool Checked { get; set; }

    }
    public class AssignRoleToUserViewModel
    {
        public tbl_users user { get; set; }
        [Display(Name = "Primary Role")]
        public List<UserRoleAssignment> Existingroles { get; set; }
    }

    public class AssignRightsToUserViewModel
    {
        [Display(Name = "Copy Rights From")]
        public List<UserDetailsView> Users { get; set; }
        public tbl_users CurrentUser { get; set; }
        public List<UserRightsAssignment> RoleRights { get; set; }
        [Display(Name = "User Role")]
        public List<UserRoleAssignment> Existingroles { get; set; }

    }
    public class UserRightsAssignment
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool Checked { get; set; }
    }
    public class PasswordSettingsViewModel
    {
        [Display(Name = "Expires After")]
        [Required(ErrorMessage = "Expiry Date Missing ")]
        public int PasswordExpiresAfter { get; set; }

        [Display(Name = "Check Password Complexity")]
        public bool AlphaNumeric { get; set; }

        [Display(Name = "Reuse After")]
        [Required(ErrorMessage = "Reuse Date Missing ")]
        public int ReusePasswordAfter { get; set; }

        [Display(Name = "Minimum Password Length")]
        [Required(ErrorMessage = "Minimum Password Length Missing ")]
        public int MinimumPasswordLength { get; set; }
        // public System.Guid tbl_PasswordSettingsID { get; set; }

    }
    public class DailyAccountsBalances
    {
        public string CustomerNo { get; set; }
        public string Name { get; set; }
        public string CustomerIdNo { get; set; }
        public string AccountNo { get; set; }
        public double PrevBal { get; set; }
        public double Deposits { get; set; }
        public double Withdrawals { get; set; }
        public double MinimumBalance { get; set; }
        public double AvailableBalance { get; set; }
        public double NewBalance { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public DateTime LastWithdrawalDate { get; set; }
    }
    public class SingleGlReportRecord
    {
        public string AccountNo { get; set; }
        public string GlContra { get; set; }
        public string Narration { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DebitAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CreditAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal balance { get; set; }
        public string BranchCode { get; set; }
    }

    public class SingleGlReportPerBranchViewModel
    {
        public string GlAccountNo { get; set; }
        public string GlAccountName { get; set; }
        public string SelectedBranchCode { get; set; }
        public List<GlAccountsView> LedgerAccounts { get; set; }
        public List<BranchSettings> BranchList { get; set; }
        public List<SingleGlReportRecord> listLedgerRecord { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal GLDebitAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal GLCreditAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal GLBalance { get; set; }
        public SingleGlReportPerBranchViewModel()
        {
            this.StartDate = DateTime.Now.Date;
            this.EndDate = DateTime.Now.Date;
        }
    }

    public class TransactionTrailViewModel
    {
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public List<GeneralLedgerTransactionTrail> TransactionTrail { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DebitTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CreditTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DiffAmount { get; set; }
        public TransactionTrailViewModel()
        {
            this.StartDate = DateTime.Now.Date;
            this.EndDate = DateTime.Now.Date;
        }
    }

    public class GeneralLedgerTransactionTrail
    {
        public string GlAccount { get; set; }
        public string GlName { get; set; }
        public string Narration { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AccountRef { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DebitAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CreditAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal balance { get; set; }
        public string UserName { get; set; }
        public string BranchCode { get; set; }
        public string ReferenceNo { get; set; }
        //for trial balance
        public string GlAccountNo { get; set; }
    }
    public class GlAccountDetails
    {
        public Guid GlAccountDetailID { get; set; }
        [Display(Name = "Account No")]
        public string GlAccountNo { get; set; }
        [Display(Name = "Address 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }
        public string EmailAddress { get; set; }
        [Display(Name = "Telephone No.")]
        public string PhoneNumber1 { get; set; }
        [Display(Name = "Mobile No.")]
        public string PhoneNumber2 { get; set; }
        [Display(Name = "Company Pin")]
        public string PinNumber { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Contact Person Phone")]
        public string ContactPersonPhoneNumber { get; set; }
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }
        [Display(Name = "Gl Name")]
        public string GlAccountName { get; set; }
    }

    public class GlAccountDetailsViewModel
    {
        public GlAccountDetails SingleRecord { get; set; }
        public List<GlAccountDetails> GlAccountDetails { get; set; }
        public List<GlAccountsView> GlAccounts { get; set; }
    }

    public class CashBankBalance
    {
        public string GlAccountNo { get; set; }
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }
        public string TTYPE { get; set; }
        public string TypeName { get; set; }
    }
    public class IncomeExpense
    {
        public string GlAccountNo { get; set; }
        public string GlAccountName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal PrevBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Month1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Month2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Month3 { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal AnnualBudget { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Variance { get; set; }

    }
    public class StatementViewModel
    {
        public List<IncomeExpense> statementList { get; set; }
        public StatementPeriod statementTimeLine { get; set; }
        public List<BranchSettings> BranchList { get; set; }
        public string SelectedBranchCode { get; set; }
        public List<CashBankBalance> generalStatement { get; set; }
        public DateTime? StartDate { get; set; }
        public bool budgetToDate { get; set; }
    }

    public class StatementPeriod
    {
        public string Month1 { get; set; }
        public string Month2 { get; set; }
        public string Month3 { get; set; }
        public string QuarterName { get; set; }
    }

    public class AuditTrailViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<BranchSettings> BranchList { get; set; }
        public List<GeneralLedgerTransactionTrail> TransactionTrail { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DebitTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal CreditTotal { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal DiffAmount { get; set; }
        public string SelectedBranchCode { get; set; }
    }
}