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
    
    public partial class tbl_cheques
    {
        public long Recno { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public decimal ChequeAmount { get; set; }
        public System.DateTime ChequeDate { get; set; }
        public string DrawerName { get; set; }
        public string DrawerBank { get; set; }
        public string DrawerBankBranch { get; set; }
        public System.DateTime DateReceived { get; set; }
        public System.DateTime MaturityDate { get; set; }
        public string PostBy { get; set; }
        public string SourceBranch { get; set; }
        public decimal CommissionCharged { get; set; }
        public string ChequeType { get; set; }
        public string SourceReferenceNo { get; set; }
        public string GlDebit { get; set; }
        public string GlCredit { get; set; }
        public Nullable<System.DateTime> DateCleared { get; set; }
        public string ClearBy { get; set; }
        public string ClearReferenceNo { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public Nullable<bool> ChqDishonoured { get; set; }
        public System.Guid tbl_chequesID { get; set; }
    }
}
