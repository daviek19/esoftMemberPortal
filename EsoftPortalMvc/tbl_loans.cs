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
    
    public partial class tbl_loans
    {
        public long Recno { get; set; }
        public string AccountNo { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Narration { get; set; }
        public string Docid { get; set; }
        public string ReferenceNo { get; set; }
        public string LoginCode { get; set; }
        public string Machine { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string BranchCode { get; set; }
        public System.DateTime AuditTime { get; set; }
        public string GlDebit { get; set; }
        public string GlCredit { get; set; }
        public string ProductCode { get; set; }
        public int Section { get; set; }
        public string GroupId { get; set; }
        public string LoanReferenceNo { get; set; }
    }
}
