//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EstateManagementMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_NavigationMenus
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public string ModuleId { get; set; }
        public bool Report { get; set; }
    }
}