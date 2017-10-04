using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ESoft.Web.Services.Registry
{
    public class CustomerAccountsManager
    {
        private readonly EsoftEntities mainDb = new EsoftEntities();
        private CustomerManager customerManager;
        private List<CustomerAccountsView> customerAccounts;
        private CustomerAccountsView customerAccount;
        private IValidationDictionary _validatonDictionary;
        private PostTransactions transactionsEngine = new PostTransactions();


        public CustomerAccountsManager()
        {
            savProductManager = new SavingsProductManager();
            accounttypes = savProductManager.SavingsAccountTypes(mainDb);
            customerManager = new CustomerManager();
            customerAccounts = new List<CustomerAccountsView>();
        }
        public CustomerAccountsManager(IValidationDictionary validationDictionary)
            : this()
        {
            _validatonDictionary = validationDictionary;
        }

        public List<CustomerAccountsView> GetCustomerAccounts(string customerNo, bool telleringOperations = false)
        {
            customerAccounts = new List<CustomerAccountsView>();
            customerNo = ValueConverters.PADLeft(SessionVariables.CustomerNumberMask.Trim().Length, customerNo, '0');
            var accounts = mainDb.tbl_CustomerAccounts.Where(x => x.CustomerNo == customerNo).OrderBy(x => x.AccountNo).ToList();
            if (telleringOperations)
            {
                accounts.RemoveAll(x => x.DateClosed.HasValue && x.DateClosed.Value.Year != 1900);
            }
            GetAccountDetails(customerNo, accounts, telleringOperations);

            return customerAccounts;
        }

        private void GetAccountDetails(string customerNo, List<tbl_CustomerAccounts> accounts, bool telleringOperations)
        {
            if (accounts.Count != 0 && accounts != null)
            {
                if (customerAccounts == null) customerAccounts = new List<CustomerAccountsView>();

                var customerDetails = customerManager.GetCustomerDetails(customerNo).FirstOrDefault();
                if (customerDetails == null)
                {
                    customerDetails = new CustomerDetailsView();
                }

                foreach (var account in accounts)
                {
                    var accountTypeDetails = accounttypes.FirstOrDefault(x => x.code == account.AccountType);//.ToList();
                    if (accountTypeDetails == null) accountTypeDetails = new tbl_accounttypes();

                    if (telleringOperations && ValueConverters.ConvertNullToBool(accountTypeDetails.Disallow_Teller_Transactions))
                    {
                        continue;
                    }
                    customerAccounts.Add(new CustomerAccountsView
                    {
                        tbl_CustomerAccountsID = account.tbl_CustomerAccountsID,
                        CustomerNo = customerNo,
                        AccountNo = account.AccountNo,
                        AccountComments = account.AccountComments,
                        AccountRemarks = account.AccountRemarks,
                        AccountType = account.AccountType,
                        AccountTypeName = accountTypeDetails.category,
                        GlMemSav = accountTypeDetails.glmemsav,
                        MinimumBalance = accountTypeDetails.minimum_bal,
                        OpenedBy = account.OpenedBy,
                        OpenedDate = account.OpenedDate,
                        AuthorisedBy = account.AuthorisedBy,
                        AuthorisedDate = account.AuthorisedDate,
                        DateClosed = account.DateClosed,
                        Locked = account.Locked,
                        CustomerBranch = customerDetails.Branch,
                        customerDetails =
                        { new CustomerDetailsView{
                            CustomerName=customerDetails.CustomerName,
                            AccountComments= customerDetails.AccountComments,
                            AccountRemarks = customerDetails.AccountRemarks,
                            Branch =  customerDetails.Branch,
                            Locked =  customerDetails.Locked,
                            CustomerIdNo =  customerDetails.CustomerIdNo,
                            DateClosed =  customerDetails.DateClosed,
                            MemberType =  customerDetails.MemberType,
                            MobileNo   =  customerDetails.MobileNo,
                            CustomerNo  = customerNo
                        }},
                        AccountTypeSettings = accountTypeDetails
                    });
                }
            }
        }


    }
}