//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EsoftPortalMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Buyers
    {
        public long Recno { get; set; }
        public System.Guid BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string MobileNo { get; set; }
        public string IdType { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}