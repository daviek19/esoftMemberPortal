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
    
    public partial class Bankerspaid_Result
    {
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmount { get; set; }
        public string postingreference { get; set; }
        public string PayeeName { get; set; }
        public System.DateTime ReferenceDate { get; set; }
        public string CreatedBy { get; set; }
        public string Branch { get; set; }
        public string GlDebit { get; set; }
        public string GlCredit { get; set; }
        public System.DateTime PostedDate { get; set; }
        public string postedBy { get; set; }
        public Nullable<bool> paid { get; set; }
        public string PostingDocid { get; set; }
        public string CustomerNo { get; set; }
        public string referencename { get; set; }
        public Nullable<decimal> SaccoCommission { get; set; }
        public Nullable<decimal> BnkCommission { get; set; }
    }
}
