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
    
    public partial class tbl_PostingDividends_Details_Flat
    {
        public long recno { get; set; }
        public string PostingDocid { get; set; }
        public string PostingReference { get; set; }
        public string CustomerNo { get; set; }
        public string AccountNo { get; set; }
        public string ProductCode { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public decimal Wtax { get; set; }
        public System.Guid tbl_PostingDividends_Details_FlatID { get; set; }
        public Nullable<decimal> QUALIFIED { get; set; }
    }
}
