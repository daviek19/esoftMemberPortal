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
    
    public partial class tbl_Sasra_Master
    {
        public long recno { get; set; }
        public string ReportCode { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Nullable<double> amount { get; set; }
        public string scoop { get; set; }
        public string OrderCode { get; set; }
        public Nullable<bool> SUBTOTAL { get; set; }
        public string subtotal_sign { get; set; }
        public string Excel_Row { get; set; }
        public string Subtotal_Order { get; set; }
        public System.Guid tbl_Sasra_MasterID { get; set; }
    }
}