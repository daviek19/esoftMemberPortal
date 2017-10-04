using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EsoftPortalMvc.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public string OrganisationName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class PortalRegisterViewModel
    {
        public string OrganisationName { get; set; }

        [Required]
        [Display(Name = "Member No")]
        public string MemberNo { get; set; }

        [Required]
        [Display(Name = "Id Number")]
        public string IdNo { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class CustomerDetailsView
    {
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public bool Locked { get; set; }
        public string AccountRemarks { get; set; }
        public string AccountComments { get; set; }
        public string MemberType { get; set; }
        public string Branch { get; set; }
        public DateTime? DateClosed { get; set; }
        public string MobileNo { get; set; }
        public string CustomerIdNo { get; set; }
        public string EmploymentNo { get; set; }
        public string PinNo { get; set; }
        public string BranchName { get; set; }
        public string MemberTypeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string EmployerCode { get; set; }
        public string EmployerName { get; set; }

    }

    public class PortalMembers
    {
        public int rec_no { get; set; }
        public Guid? tbl_CustomerId { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string IdNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
    }

    public class CustomerAccountsView
    {
        public int CustomerAccountsViewId { get; set; }
        public Guid tbl_CustomerAccountsID { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerIdNo { get; set; }
        public string AccountNo { get; set; }
        public DateTime OpenedDate { get; set; }
        public string OpenedBy { get; set; }
        public DateTime? AuthorisedDate { get; set; }
        public string AuthorisedBy { get; set; }
        public bool Locked { get; set; }
        public string AccountRemarks { get; set; }
        public string AccountComments { get; set; }
        public decimal AccountType { get; set; }
        public string AccountTypeName { get; set; }
        public DateTime? DateClosed { get; set; }
        public string ClosedBy { get; set; }
        public decimal? MinimumBalance { get; set; }
        public string GlMemSav { get; set; }
        public tbl_accounttypes AccountTypeSettings { get; set; }
        public string CustomerBranch { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool? Parent_Locked { get; set; }
        public string Parent_AccountRemarks { get; set; }
        public string Parent_AccountComments { get; set; }
        public string ReasonClosed { get; set; }
        // Display Purposes for Account Rejoining
        public List<PostingJournalsHeaderViewModel> ClosedAccountDetails { get; set; }

        public List<CustomerDetailsView> customerDetails { get; set; }
        public List<AccountTypesView> SavingsProducts { get; set; }
        public CustomerAccountsView()
        {
            customerDetails = new List<CustomerDetailsView>();
        }
    }

    public class PostingJournalsHeaderViewModel
    {
        [Display(Name = "Module Id")]
        public string PostingDocid { get; set; }
        [Display(Name = "Reference No.")]
        public string PostingReference { get; set; }
        [Display(Name = "Journal Description")]
        [StringLength(50, MinimumLength = 10)]
        public string PostingDescription { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }
        public string CreateBranch { get; set; }
        [Display(Name = "Create At")]
        public string CreateBranchName { get; set; }
        public DateTime CreateDate { get; set; }
        public string VerifiedBy { get; set; }
        [Display(Name = "Verified By")]
        public string VerifiedByName { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string PostedBy { get; set; }
        [Display(Name = "Posted By")]
        public string PostedByName { get; set; }
        public DateTime? PostDate { get; set; }
        public decimal? PostingLevel { get; set; }
        public string CustomerNo { get; set; }
        public string AccountNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? Balance { get; set; }
        public string Section { get; set; }
        public Guid JournalId { get; set; }
        /* Used by complete withdrawal*/
        public string FosaAccount { get; set; }
        public string BankAccount { get; set; }
        public int PostingMode { get; set; }
        public string WithdrawalReasonCode { get; set; }
        [Display(Name = "Withdrawal Reason")]
        public string WithdrawalReasonName { get; set; }
        public string PaymentMethodName { get; set; }
        public string PayingAccount { get; set; }
        public string ChequeNo { get; set; }
        /* Eof Used by complete withdrawal*/

        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? TotalDebits { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? TotalCredits { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal? BatchDifference { get; set; }

        public int? TotalEntries { get; set; }
        public string CustomerName { get; set; }
        public string CustomerBranchName { get; set; }
        public string CustomerSavingsAccountType { get; set; }
        public List<CustomerAccountsView> CustomerSavingsAccounts { get; set; }

    }

    public class AccountTypesView
    {
        public decimal? code { get; set; }
        public string act_code { get; set; }
        public string category { get; set; }
    }

    public class SavingsViewModel
    {
        public CustomerDetailsView CustomerDetails { get; set; }
        public List<CustomerAccountsView> SavingsAccounts { get; set; }
    }
}
