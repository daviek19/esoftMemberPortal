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
    
    public partial class tbl_withdrawals_Funeral_Rider
    {
        public long recno { get; set; }
        public string CustomerNo { get; set; }
        public string DeceasedReferenceNumber { get; set; }
        public string PostingDocid { get; set; }
        public string PostingReferenceNo { get; set; }
        public string BurialPermitNumber { get; set; }
        public decimal AmountPaid { get; set; }
        public string GlAccountNo { get; set; }
        public string PaidTo_Name { get; set; }
        public string PaidTo_IdNo { get; set; }
        public string PaidTo_PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public string PostedBy { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<decimal> PostingLevel { get; set; }
    }
}
