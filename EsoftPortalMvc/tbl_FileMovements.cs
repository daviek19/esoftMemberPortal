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
    
    public partial class tbl_FileMovements
    {
        public long recno { get; set; }
        public string CustomerNo { get; set; }
        public string DispatchingOffice { get; set; }
        public string Reasons { get; set; }
        public Nullable<System.DateTime> DATE_Dispatched { get; set; }
        public string Issued_to { get; set; }
        public string DestinationOffice { get; set; }
        public string Received_By { get; set; }
        public Nullable<System.DateTime> Date_Received { get; set; }
        public string Receiving_Office { get; set; }
        public string Receiving_Remarks { get; set; }
        public Nullable<bool> status { get; set; }
        public System.Guid tbl_FileMovementsID { get; set; }
    }
}
