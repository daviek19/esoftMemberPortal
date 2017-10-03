using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EstateManagementMvc.Areas.Dashboard.Models
{
    public class DashboardViewModel
    {
        [Key]
        public string tType { get; set; }

        public string productName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]

        public decimal balance { get; set; }
    }
    public class DashboardViewModelGraph
    {


        public string label { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]

        public string value { get; set; }
    }
    public class CashBalancesViewModel
    {
        [Key]
        public string glAccountNo { get; set; }
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal balance { get; set; }
        public string tType { get; set; }
        public string typeName { get; set; }
    }
}