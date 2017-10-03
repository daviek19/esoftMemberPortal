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
}
