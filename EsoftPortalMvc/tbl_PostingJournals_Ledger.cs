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
    
    public partial class tbl_PostingJournals_Ledger
    {
        public long recno { get; set; }
        public string PostingDocid { get; set; }
        public string PostingReference { get; set; }
        public string AccountNo { get; set; }
        public string Narration { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public Nullable<decimal> balance { get; set; }
        public string BranchCode { get; set; }
        public System.Guid tbl_PostingJournals_LedgerID { get; set; }
    }
}