//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EstateManagementMvc
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_LoanMasterTable
    {
        public long recno { get; set; }
        public string LoanCode { get; set; }
        public string customerNo { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Period { get; set; }
        public Nullable<System.DateTime> DateIssued { get; set; }
        public Nullable<decimal> AmountIssued { get; set; }
        public Nullable<decimal> MonthlyExpected { get; set; }
        public Nullable<decimal> InterestExpected { get; set; }
        public string PostingDocid { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<decimal> TotExpected { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string RecoveryCode { get; set; }
        public Nullable<decimal> OriginalRepayment { get; set; }
        public Nullable<decimal> ByProductAmount { get; set; }
        public string GradeCode { get; set; }
        public System.Guid tbl_LoanMasterTableID { get; set; }
    }
}
