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
    
    public partial class Single_teller_cheques_Result
    {
        public long Recno { get; set; }
        public string GlAccountNo { get; set; }
        public string GlContra { get; set; }
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
        public System.Guid tbl_ledgerID { get; set; }
        public Nullable<decimal> balance { get; set; }
        public string Glname { get; set; }
        public string Membername { get; set; }
    }
}
