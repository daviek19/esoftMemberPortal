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
    
    public partial class tbl_SystemMessaging
    {
        public long recno { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public Nullable<System.DateTime> DateRaised { get; set; }
        public string MessageSend { get; set; }
        public Nullable<System.DateTime> DateRead { get; set; }
    }
}
