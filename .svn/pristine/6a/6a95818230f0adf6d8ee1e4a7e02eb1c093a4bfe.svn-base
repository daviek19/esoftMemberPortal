using EstateManagementMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace EstateManagementMvc.Services.Common
{
    public class Settings
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public static class OtherSystemSettings
    {
        public static List<Settings> settings { get; set; }

        static OtherSystemSettings()
        {
            DbDataReader reader = GetSettings();
            settings = new List<Settings>();

            List<Settings> set = new List<Settings>();

            while (reader.Read())
            {
                settings.Add(
                    new Settings
                    {
                        Key = reader["PurposeName"].ToString(),
                        Value = reader["KeyValue"].ToString()
                    });
            }
            reader.Close();

        }

        public static DbDataReader GetSettings()
        {
            DbDataReader reader = null;
            string sql = "SELECT PurposeName,KeyValue FROM tbl_LoanPurposeCodes where coalesce(PurposeCode,'')='XX' ORDER BY PurposeName ";
            reader = DbConnector.GetSqlReader(sql);
            return reader;

        }

        public static string GetOtherSystemSettings(string settingToCheck)
        {
            var found = settings.FirstOrDefault(x => x.Key == settingToCheck);
            if (found != null)
            {
                return found.Value.Substring(1, found.Value.Trim().Length - 1);
               
            }
            else
            {
                return string.Empty;
            }
        }
    }


}