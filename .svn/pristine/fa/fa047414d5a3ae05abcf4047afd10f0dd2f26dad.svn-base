using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstateManagementMvc.Services.Common
{
    public class DataTablesCustomModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                           ModelBindingContext bindingContext)
        {


            HttpRequestBase request = controllerContext.HttpContext.Request;
            string draw = string.Empty, sortColumn = string.Empty, sortColumnDir = string.Empty, searchValue = string.Empty, order = string.Empty, orderDir = string.Empty;
            Int32 startRec = 0, start = 0, skip = 0;
            int pageSize = 0, recordsTotal = 0;
            if (request.Form.GetValues("draw") != null && request.Form.GetValues("length") != null)
            {
                draw = request.Form.GetValues("draw").FirstOrDefault();
                startRec = Convert.ToInt32(request.Form.GetValues("start")[0]);
                start = startRec;
                var length = request.Form.GetValues("length").FirstOrDefault();
                sortColumn = request.Form.GetValues("columns[" + request.Form.GetValues("order[0][column]").FirstOrDefault()
                                        + "][name]").FirstOrDefault();
                sortColumnDir = request.Form.GetValues("order[0][dir]").FirstOrDefault();
                sortColumnDir = sortColumnDir ?? "ASC";
                searchValue = request.Form.GetValues("search[value]").FirstOrDefault();

                order = request.Form.GetValues("order[0][column]")[0];
                orderDir = request.Form.GetValues("order[0][dir]")[0];

                pageSize = length != null ? Convert.ToInt32(length) : 0;
                skip = start != null ? ValueConverters.StringtoInteger(start.ToString()) : 0;
            }
            return new JqueryDataTablesModel
            {
                startRec = skip,
                Start = start.ToString(),
                Skip = skip,
                Draw = draw,
                PageSize = pageSize,
                OrderColumn = sortColumn,
                OrderDirection = sortColumnDir,
                SearchValue = searchValue,
                TotalRecords = recordsTotal,
                Order = order,
                SortAscending = sortColumnDir.ToUpper() == "ASC" ? true : false,
                // BoundColumnName= boundColumns
            };
        }


    }

    public class JqueryDataTablesModel
    {
        public String Start { get; set; }
        public String Draw { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 Skip { get; set; }
        public String OrderColumn { get; set; }
        public String OrderDirection { get; set; }
        public string SearchValue { get; set; }
        public string Order { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int32 startRec { get; set; }
        public List<DataTableBoundColumns> BoundColumnName { get; set; }
        public bool SortAscending { get; set; }

    }

    public class DataTableBoundColumns
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
    }
}