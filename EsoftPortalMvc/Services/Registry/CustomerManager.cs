using ESoft.Web.Services.Registry;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using Microsoft.Reporting.WebForms;
using EsoftPortalMvc.Models;
using EsoftPortalMvc.Services.Common;

namespace ESoft.Web.Services.Registry
{
    public class CustomerManager
    {
        public const string customerNumberMask = "999999";

        private AuditTrail auditTrail = new AuditTrail();
        private Esoft_EstateEntities mainDb = new Esoft_EstateEntities();
        private IValidationDictionary _validatonDictionary;
        readonly string connectionString = DbConnector.MainDbConnectionString();
        private PostTransactions transactionsEngine = new PostTransactions();
        List<PostTransactionsViewModel> translist = new List<PostTransactionsViewModel>();
        public CustomerManager()
        {

        }

        public CustomerManager(IValidationDictionary validationDictionary)
        {
            _validatonDictionary = validationDictionary;
        }

        public List<CustomerDetailsView> GetCustomerDetails(string customerNo)
        {
            List<CustomerDetailsView> custRecord = null;
            if (ValueConverters.IsStringEmpty(customerNo))
            {
                return custRecord;
            }
            customerNo = ValueConverters.PADLeft(customerNumberMask.Length, customerNo.Trim(), '0');
            try
            {
                string sqlCommand = "SELECT CustomerNo,coalesce(CustomerIdNo,'') as CustomerIdNo,coalesce(CustomerName,'') as CustomerName,Locked,AccountRemarks,AccountComments,MemberType,Branch," +
                    " AccountRemarks,AccountComments,DateClosed,coalesce(MobileNo,'') as MobileNo,EmpNumber as EmploymentNo,DateOfBirth,JoiningDate,Coalesce(EmployerCode,'') as EmployerCode," +
                    " coalesce((select name from BranchSettings where branchcode=tbl_customer.Branch),'') as BranchName, " +
                    " coalesce((select MembershipName from tbl_MembershipTypes where MembershipCode=tbl_customer.MemberType),'') as MemberTypeName, " +
                    " coalesce((select DPTNAME from tbl_Departments where DptCode=tbl_customer.EmployerCode),'') as EmployerName " +
                    " from tbl_customer WHERE CUSTOMERNO ='" + customerNo.Format_Sql_String() + "'";

                DbDataReader reader = DbConnector.GetSqlReader(sqlCommand);

                custRecord = Functions.DataReaderMapToList<CustomerDetailsView>(reader);

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ref ex);
            }
            if (custRecord == null || custRecord.Count < 1)
            {
                custRecord = new List<CustomerDetailsView>
                {
                    new CustomerDetailsView()
                };
                custRecord[0].CustomerName = "Customer Record Not Found ";
            }
            return custRecord;
        }

        public PortalMembers GetPortalCustomer(string customerNo)
        {
            PortalMembers custRecord = null;

            if (ValueConverters.IsStringEmpty(customerNo))
            {
                return custRecord;
            }

            customerNo = ValueConverters.PADLeft(customerNumberMask.Length, customerNo.Trim(), '0');

            try
            {
                string sqlCommand = "SELECT tbl_CustomerId,CustomerNo,CustomerName,IdNo,MobileNo,Email,SecurityCode,Password,DateCreated " +
                                    "FROM tbl_PortalMembers WHERE CustomerNo  ='" + customerNo.Format_Sql_String() + "'";

                DbDataReader reader = DbConnector.GetSqlReader(sqlCommand);

                custRecord = Functions.DataReaderMapToList<PortalMembers>(reader).FirstOrDefault();

                if (custRecord == null)
                {
                    custRecord = new PortalMembers();
                }

            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ref ex);
            }

            return custRecord;
        }

    }

}
