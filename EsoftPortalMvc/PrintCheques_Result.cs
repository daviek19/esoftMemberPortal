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
    
    public partial class PrintCheques_Result
    {
        public long Recno { get; set; }
        public string PostingDocid { get; set; }
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string ChequeNo { get; set; }
        public string PayeeName { get; set; }
        public decimal ChequeAmount { get; set; }
        public string ReferenceNo { get; set; }
        public string PostBy { get; set; }
        public System.DateTime DatePosted { get; set; }
        public string SourceBranch { get; set; }
        public string ChequeType { get; set; }
        public Nullable<bool> Printed { get; set; }
        public string PrintedBy { get; set; }
        public Nullable<System.DateTime> DatePrinted { get; set; }
    }
}
