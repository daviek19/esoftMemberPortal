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
    
    public partial class tbl_MembershipTypes
    {
        public long recno { get; set; }
        public string MembershipCode { get; set; }
        public string MembershipName { get; set; }
        public decimal MaxShareCapital { get; set; }
        public Nullable<decimal> EntranceFee { get; set; }
        public string EntranceFee_Gl { get; set; }
        public string ShareCapitalCode { get; set; }
        public Nullable<decimal> Minimum_Balance { get; set; }
        public System.Guid tbl_MembershipTypesID { get; set; }
    }
}
