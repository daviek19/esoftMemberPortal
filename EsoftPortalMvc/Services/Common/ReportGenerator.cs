using EsoftPortalMvc.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EstateManagementMvc;

namespace EsoftPortalMvc.Services.Common
{

    public interface IReportGenerator {

       ReportViewer  GenerateReport (ReportModel reportModel);


    }
    public class ReportGenerator : IReportGenerator
    {
        public EsoftPortalEntities mainDb;
        public ReportGenerator() {
            mainDb = new EsoftPortalEntities();
        }
        public  ReportViewer  GenerateReport (ReportModel reportModel)
        {
            ReportViewer viewer = new ReportViewer();

            List<Company> company = mainDb.Companies.ToList();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = reportModel.ReportPath;
            ReportParameter [] reportParaters = new List<ReportParameter>().ToArray();
            reportParaters[0]=new ReportParameter("paramUserName",EsoftPortalMvc.Services.Common.UserSession.Current.userDetails.CustomerNo);



            viewer.LocalReport.DataSources.Add(new ReportDataSource("DsCompany", company));
            Int16 count = 0;
            foreach (var reportparameter in reportModel.ReportParameters.Keys) {
                reportParaters[count++]=new ReportParameter(reportparameter, reportModel.ReportParameters[reportparameter]);
            }
            viewer.LocalReport.SetParameters( reportParaters);
            return viewer;


        }


    }


    public class ReportModel {
        public String ReportPath{ get; set; }
        public Dictionary<String, String>  ReportParameters { get; set; }
        public Dictionary<String ,List<object>> ReportDataSources { get; set; }
    }
}