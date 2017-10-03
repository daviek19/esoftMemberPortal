using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Common
{
    public class CustomValidation
    {
        public static ValidationResult ValidateGreaterOrEqualToZero_NullValid(decimal? value, ValidationContext context)
        {
            bool isValid = true;

            if (value != null && value < decimal.Zero)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must be greater than or equal to 0.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult ValidateGreaterOrEqualToZero(decimal? value, ValidationContext context)
        {
            bool isValid = true;

            if (value == null || value < decimal.Zero)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must be greater than or equal to 0.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult ValidatePercentage(decimal? value, ValidationContext context)
        {
            bool isValid = true;

            if (value == null || value < decimal.Zero || value > 100)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must be greater than or equal to 0.", context.MemberName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult ValidatePercentage_Integer(int? value, ValidationContext context)
        {
            decimal? val = (decimal?)value;

            return ValidatePercentage((val), context);
        }

        public static ValidationResult ValidatePercentage_Integer_NullValid(int? value, ValidationContext context)
        {
            decimal? val = (decimal?)value;
            val = val == null ? 0 : val;
            return ValidatePercentage((val), context);
        }
        public static ValidationResult ValidateGreaterOrEqualToZero_Integer_NullValid(int? value, ValidationContext context)
        {
            decimal? val = (decimal?)value;
            val = val == null ? 0 : val;
            return ValidateGreaterOrEqualToZero((val), context);
        }
        public static ValidationResult ValidateGreaterOrEqualToZero_Integer(int? value, ValidationContext context)
        {
            decimal? val = (decimal?)value;

            return ValidateGreaterOrEqualToZero((val), context);
        }
        public static ValidationResult ValidateGreaterOrEqualToZero_Double(double? value, ValidationContext context)
        {
            decimal? val = (decimal?)value;

            return ValidateGreaterOrEqualToZero((val), context);
        }



        public static ValidationResult ValidateGreaterThanZero(decimal? value, ValidationContext context)
        {
            bool isValid = true;

            if (value == null || value <= decimal.Zero)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must be greater than Zero (0).", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }



        public static ValidationResult ValidateGlAccount(string value, ValidationContext context)
        {
            bool isValid = ValidateGlAccount_(value);

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must a Valid Gl Account.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static bool ValidateGlAccount_(string value)
        {
            bool isValid = true;

            if (ValueConverters.IsStringEmpty(value) ||
                value.Trim().Length != SessionVariables.GlAccountMask.Trim().Length)
            {
                isValid = false;
            }

            //ToDo Confirm GlAccount Existence 
            if (isValid && value.Trim().Length == SessionVariables.GlAccountMask.Trim().Length)
            {
                // check existence of account
            }
            return isValid;
        }

        public static bool ValidateCustomerMemberNo_(string value)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||
                value.Trim().Length != SessionVariables.CustomerNumberMask.Trim().Length)
            {
                isValid = false;
            }

            //ToDo Confirm Customer Mno Existence 
            if (isValid && value.Trim().Length == SessionVariables.CustomerNumberMask.Trim().Length)
            {
                // check existence of Mno
            }

            return isValid;
        }


        public static ValidationResult ValidateCustomerMemberNo(string value, ValidationContext context)
        {
            if (value == "NEWCUSTOMER")
                return ValidationResult.Success; // Creating new record

            bool isValid = true;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||
                value.Trim().Length != SessionVariables.CustomerNumberMask.Trim().Length)
            {
                isValid = false;
            }

            //ToDo Confirm Customer Mno Existence 
            if (isValid && value.Trim().Length == SessionVariables.CustomerNumberMask.Trim().Length)
            {
                // check existence of Mno
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must a Valid Customer M/No.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult ValidateCustomerAccountNo(string value, ValidationContext context)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||
                value.Trim().Length != SessionVariables.CustomerAccountMask.Trim().Length)
            {
                isValid = false;
            }

            //ToDo Confirm Account Existence Existence 
            if (isValid && value.Trim().Length == SessionVariables.CustomerAccountMask.Trim().Length)
            {
                // check existence of account
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must a Valid Customer Account.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static bool ValidateCustomerAccountNo_(string value)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||
                value.Trim().Length != SessionVariables.CustomerAccountMask.Trim().Length)
            {
                isValid = false;
            }

            //ToDo Confirm Account Existence Existence 
            if (isValid && value.Trim().Length == SessionVariables.CustomerAccountMask.Trim().Length)
            {
                // check existence of account
            }

            return isValid;

        }

        public static ValidationResult ValidateCommissionCode(string value, ValidationContext context)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
            }
            else
            {
                if (value.Trim().Length != 5)
                {
                    isValid = false;
                }

                //ToDo Confirm Commission Existence  from Paytypes
                if (isValid && value.Trim().Length == 5)
                {
                    // check existence of account
                }
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must a Valid Commission Type.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult Validate_Future_Date(DateTime? value, ValidationContext context)
        {
            bool isValid = true;

            if (value != null)
            {
                if (value > DateTime.Now)
                {
                    isValid = false;
                }
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format(" {0} Cannot be in the Future.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }
        }

        public static ValidationResult Validate_MobileNumber(string mobileNo, ValidationContext context)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(mobileNo) || string.IsNullOrWhiteSpace(mobileNo))
            {
            }
            else
            {
                if (mobileNo.Trim().Length != 10)
                {
                    isValid = false;
                }
                if (isValid)
                {
                    if (mobileNo.Trim().Substring(0, 2) != "07")
                    {
                        isValid = false;
                    }// Not fool proof user can enter 0722000000 .... or 0700000000 etc
                }
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    string.Format("The field {0} must a Valid Mobile No.", context.DisplayName),
                    new List<string>() { context.MemberName });
            }


        }

        public static bool Validate_MobileNumber_(string mobileNo)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(mobileNo) || string.IsNullOrWhiteSpace(mobileNo))
            {
            }
            else
            {
                if (mobileNo.Trim().Length != 10)
                {
                    isValid = false;
                }
                if (isValid)
                {
                    if (mobileNo.Trim().Substring(0, 2) != "07")
                    {
                        isValid = false;
                    }// Not fool proof user can enter 0722000000 .... or 0700000000 etc
                }
            }

            return isValid;

        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                return new EmailAddressAttribute().IsValid(email);
            }
            catch
            {
                return false;
            }
        }

    }
}