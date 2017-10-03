using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Services.Common
{
    public class CompanyManager
    {
        private Esoft_EstateEntities mainDb;
        private IValidationDictionary _validationDictionary;

        public CompanyManager()
        {
            mainDb = new Esoft_EstateEntities();
        }

        public CompanyManager(IValidationDictionary validationDictionary)
            : this()
        {
            _validationDictionary = validationDictionary;
        }

        public Company GetCompanyDetails()
        {
            return mainDb.Company.FirstOrDefault();
        }

        public CompanySettingViewModel GetCompanySettingViewModel(CompanySettingViewModel companySettingViewModel)
        {
            var companydetails = GetCompanyDetails();

            if (companydetails != null)
            {
                //GlAccountsManager glAccountsManager = new GlAccountsManager();
                companySettingViewModel.CompanyId = companydetails.CompanyID;
                companySettingViewModel.Address = companydetails.CompanyAddress;
                companySettingViewModel.CompanyName = companydetails.CompanyName;                
                companySettingViewModel.Email = companydetails.CompanyEmail;
                companySettingViewModel.ExciseDuty = companydetails.Excise_Duty_Rate;
                companySettingViewModel.KraPinNo = companydetails.CompanyPin;                
                companySettingViewModel.ScreenMessage = companydetails.DisplayMessage;
                companySettingViewModel.SlipFooterText = companydetails.Teller_Slip_FooterText;
                companySettingViewModel.StampDutyCharge = companydetails.StampDuty;
               
                companySettingViewModel.SystemOutLockTime = companydetails.System_Lock_Out_time;
                companySettingViewModel.Telephone = companydetails.CompanyTelephone;
                companySettingViewModel.WithHoldingTaxRate = companydetails.WithholdingtaxRate;
               
               // companySettingViewModel.GlAccounts = glAccountsManager.GlAccountsTrimmed(mainDb).ToList();

            }
            return companySettingViewModel;

        }
        public bool SaveCompanyDetails(CompanySettingViewModel companySettingViewModel)
        {
            if (companySettingViewModel == null)
                return false;
            string update_string = " MERGE Company AS TARGET  USING (SELECT '" + companySettingViewModel.CompanyId.ToString() + "','" +
            ValueConverters.format_sql_string(companySettingViewModel.Address) + "','" + ValueConverters.format_sql_string(companySettingViewModel.Email) + "','" +
            ValueConverters.format_sql_string(companySettingViewModel.Telephone) + "','" + ValueConverters.format_sql_string(companySettingViewModel.KraPinNo) + "'," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.AuthorisedCashDeposit).ToString() + "," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.AuthorisedCashWithDrw).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.AuthorisedChqDeposit).ToString() + "," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.AuthorisedCashReceipt).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.AuthorisedCashPayments).ToString() + "," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.ExciseDuty).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.StampDutyCharge).ToString() + "," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.DormancyPeriod).ToString() + ",'" +
            ValueConverters.ConvertNullToBool(companySettingViewModel.ChargeChequeDepositOnDeposit).ToString() + "'," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.TellerInsuranceLimit).ToString() + ",'" +
            ValueConverters.ConvertNullToBool(companySettingViewModel.SummariseLedgerPosting).ToString() + "'," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.WithHoldingTaxRate).ToString() + ",'" +
            ValueConverters.ConvertNullToBool(companySettingViewModel.LoanAppraisalCaptureGuarantor).ToString() + "'," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.OrdinaryChequeCommission).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.BankersChequeSaccoCharge).ToString() + ",'" + ValueConverters.format_sql_string(companySettingViewModel.ScreenMessage).ToString() + "'," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.TellerCommSplit).ToString() + ",'" + ValueConverters.ConvertNullToBool(companySettingViewModel.TransferCashToTreasuryOnCloseDay).ToString() + "'," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.BankersChequeBankCharge).ToString() + ",'" + ValueConverters.format_sql_string(companySettingViewModel.OrdinaryChequeCommissionGl) + "','" +
            ValueConverters.format_sql_string(companySettingViewModel.BankersChequeGl) + "'," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.MaximumLoanToGuarantee).ToString() + ",'" +
            ValueConverters.format_sql_string(companySettingViewModel.SlipFooterText) + "'," + ValueConverters.ConvertNullToInteger(companySettingViewModel.SystemOutLockTime).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.AtmLinkChargeSacco).ToString() + "," + ValueConverters.ConvertNullToDecimal(companySettingViewModel.AtmLinkChargeBank).ToString() + ",'" +
            ValueConverters.format_sql_string(companySettingViewModel.AtmCommissionChargeGl).ToString() + "','" + ValueConverters.format_sql_string(companySettingViewModel.AtmSettlementGlAccount) + "'," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.AtmPinRegerationCharge).ToString() + ",'" + ValueConverters.ConvertNullToBool(companySettingViewModel.CaptureManualLnNo).ToString() + "'," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.RegulatorCashAuthorisedAmount).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.RetirementBonus).ToString() + "," +
            ValueConverters.ConvertNullToDecimal(companySettingViewModel.DailyAtmWithDrawalLimit).ToString()
            + ") AS SOURCE "
            + "(CompanyId,CompanyAddress,CompanyEmail,CompanyTelephone,CompanyPin     "
            + ",CashDepositAuthorityAmount,CashWithdrawalAuthorityAmount,ChequeDepositAuthorityAmount"
            + ",CashReceiptsAuthorityAmount,CashPaymentsAuthorityAmount,Excise_Duty_Rate,StampDuty"
            + ",DormancyPeriod,ChqDeposit_Commission_Charge_On_Deposit,TellerCashInsuranceLimit"
            + ",SummariseLedgerPostings,WithholdingtaxRate,Capture_Guarantors_during_Appraisal"
            + ",OrdinaryChequeCommission,BankersChequeCommission,DisplayMessage,Teller_Commission_Split_Percentage"
            + ",Teller_CloseDay_Transfer_Cash_to_Treasury,BankersChequeCommission_Bank,OrdinaryCheque_account"
            + ",BankersCheque_account,Maximum_Guaranteeship,Teller_Slip_FooterText,System_Lock_Out_time,"
            + "Atmlinkchargesacco,Atmlinkchargecoop,Atmlinkchargeaccount,Atmsettlementaccount,AtmPinChange_Charge,Capture_Manual_Loannumber,CBK_CUSTOMER_CASH_WD_DEP_LIMIT"
            + ",retirementbonus,DailyAtmWithdrawal_Limit)"
            + "ON ( TARGET.CompanyId = SOURCE.CompanyId ) WHEN MATCHED THEN "
            + "UPDATE SET   CompanyAddress=SOURCE.CompanyAddress,CompanyEmail=SOURCE.CompanyEmail,CompanyTelephone=SOURCE.CompanyTelephone,CompanyPin=SOURCE.CompanyPin"
            + ",CashDepositAuthorityAmount=SOURCE.CashDepositAuthorityAmount,CashWithdrawalAuthorityAmount=SOURCE.CashWithdrawalAuthorityAmount,ChequeDepositAuthorityAmount=SOURCE.ChequeDepositAuthorityAmount"
            + ",CashReceiptsAuthorityAmount=SOURCE.CashReceiptsAuthorityAmount,CashPaymentsAuthorityAmount=SOURCE.CashPaymentsAuthorityAmount,Excise_Duty_Rate=SOURCE.Excise_Duty_Rate,StampDuty=SOURCE.StampDuty"
            + ",DormancyPeriod=Source.DormancyPeriod,ChqDeposit_Commission_Charge_On_Deposit=source.ChqDeposit_Commission_Charge_On_Deposit,TellerCashInsuranceLimit=source.TellerCashInsuranceLimit"
            + ",SummariseLedgerPostings=source.SummariseLedgerPostings,WithholdingtaxRate=source.WithholdingtaxRate,Capture_Guarantors_during_Appraisal=source.Capture_Guarantors_during_Appraisal"
            + ",OrdinaryChequeCommission=source.OrdinaryChequeCommission,BankersChequeCommission_sacco=source.BankersChequeCommission"
            + ",DisplayMessage=source.DisplayMessage,Teller_Commission_Split_Percentage=Source.Teller_Commission_Split_Percentage "
            + ",Teller_CloseDay_Transfer_Cash_to_Treasury=source.Teller_CloseDay_Transfer_Cash_to_Treasury"
            + ",BankersChequeCommission_Bank=source.BankersChequeCommission_Bank,OrdinaryCheque_account=source.OrdinaryCheque_account,BankersCheque_account=source.BankersCheque_account"
            + ",Maximum_Guaranteeship= source.Maximum_Guaranteeship,Teller_Slip_FooterText=source.Teller_Slip_FooterText,System_Lock_Out_time=source.System_Lock_Out_time"
            + ",Atmlinkchargesacco= source.Atmlinkchargesacco,Atmlinkchargecoop=SOURCE.Atmlinkchargecoop,Atmlinkchargeaccount=SOURCE.Atmlinkchargeaccount"
            + ",Atmsettlementaccount=source.Atmsettlementaccount,AtmPinChange_Charge=source.AtmPinChange_Charge,Capture_Manual_Loannumber=source.Capture_Manual_Loannumber "
            + ",CBK_CUSTOMER_CASH_WD_DEP_LIMIT=source.CBK_CUSTOMER_CASH_WD_DEP_LIMIT,retirementbonus=SOURCE.retirementbonus,DailyAtmWithdrawal_Limit=SOURCE.DailyAtmWithdrawal_Limit ;";

            PostTransactions transactionsEngine = new PostTransactions();
            string m_current_posting_reference = ValueConverters.RandomString(10);
            List<PostTransactionsViewModel> transList = new List<PostTransactionsViewModel>();
            transactionsEngine.Generate_Sql_Statement(transList, m_current_posting_reference, update_string);

            AuditCompanyChanges(companySettingViewModel, transList, m_current_posting_reference);

            string results = transactionsEngine.Post_Transactions(transList, m_current_posting_reference, false, false);
            transactionsEngine = null;
            transList = null;
            return results == SessionVariables.TransactionPostedSuccessfully;
        }


        private void AuditCompanyChanges(CompanySettingViewModel companySettingViewModel, List<PostTransactionsViewModel> transList, string m_current_posting_reference)
        {
            Company entity = mainDb.Company.FirstOrDefault();

            UpdateModel(companySettingViewModel);
            if (mainDb.Entry(entity).State == EntityState.Added)
            {
                foreach (string propertyName in mainDb.Entry(entity).CurrentValues.PropertyNames)
                {
                    var newValue = mainDb.Entry(entity)
                                     .CurrentValues.GetValue<object>(propertyName);
                    if (newValue != null)
                    {
                        string activity = " Captured Value " + propertyName.Trim() + newValue.ToString().Trim();
                        AuditTrail.CreateTrailPostingRecord(transList, m_current_posting_reference, "9904", activity, 0, "SETUP", UserSession.Current.userDetails.LoginCode, false);
                    }
                }
            }
            else
            {
                foreach (string propertyName in mainDb.Entry(entity).OriginalValues.PropertyNames)
                {
                    var newValue = mainDb.Entry(entity)
                                     .CurrentValues.GetValue<object>(propertyName);

                    var originalValue = mainDb.Entry(entity)
                                        .OriginalValues.GetValue<object>(propertyName);

                    if (originalValue != null && newValue == null && !originalValue.Equals(newValue))
                    {
                        string activity = " Changed " + propertyName.Trim() + " From " + originalValue.ToString().Trim() + " To ";
                        AuditTrail.CreateTrailPostingRecord(transList, m_current_posting_reference, "9904", activity, 0, "SETUP", UserSession.Current.userDetails.LoginCode, false);
                    }
                    else if (originalValue == null && newValue != null)
                    {
                        string activity = " Changed " + propertyName.Trim() + " From  To " + newValue.ToString().Trim();
                        AuditTrail.CreateTrailPostingRecord(transList, m_current_posting_reference, "9904", activity, 0, "SETUP", UserSession.Current.userDetails.LoginCode, false);
                    }
                    else if (originalValue != null && newValue == null)
                    {
                        string activity = " Changed " + propertyName.Trim() + " From " + originalValue.ToString().Trim() + " To ";
                        AuditTrail.CreateTrailPostingRecord(transList, m_current_posting_reference, "9904", activity, 0, "SETUP", UserSession.Current.userDetails.LoginCode, false);
                    }
                }
            }
        }

        private void UpdateModel(CompanySettingViewModel companySettingViewModel)
        {
            var companydetails = GetCompanyDetails();

            if (companydetails != null)
            {
                companydetails.CompanyAddress = companySettingViewModel.Address;
                companydetails.CompanyName = companySettingViewModel.CompanyName;                
                companydetails.CompanyEmail = companySettingViewModel.Email;
                companydetails.Excise_Duty_Rate = companySettingViewModel.ExciseDuty;
                companydetails.CompanyPin = companySettingViewModel.KraPinNo;               
                companydetails.DisplayMessage = companySettingViewModel.ScreenMessage;
                companydetails.Teller_Slip_FooterText = companySettingViewModel.SlipFooterText;
                companydetails.StampDuty = companySettingViewModel.StampDutyCharge;                
                companydetails.System_Lock_Out_time = companySettingViewModel.SystemOutLockTime;
                companydetails.CompanyTelephone = companySettingViewModel.Telephone;               
                companydetails.WithholdingtaxRate = companySettingViewModel.WithHoldingTaxRate;
                
            }
        }
    }
}