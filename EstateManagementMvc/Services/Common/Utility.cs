
using System;
using System.Text.RegularExpressions;

namespace EstateManagementMvc.Services.Common
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    /// 

    public class Utility
    {

        public Utility()
        {
            //      
        }
        public static void WriteErrorLog(string source, ref Exception e)
        {
            try
            {
                CheckLogDirectory(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\");
                System.IO.TextWriter ErrHan = new System.IO.StreamWriter(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log", true);
                ErrHan.WriteLine(source);
                ErrHan.WriteLine(DateTime.Now.ToString());
                ErrHan.WriteLine(e.Message);
                ErrHan.WriteLine(e.StackTrace);
                ErrHan.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                ErrHan.WriteLine("\n\n");
                ErrHan.Flush();
                ErrHan.Close();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
            }
        }

        public static void WriteErrorLog(ref Exception e)
        {
            CheckLogDirectory(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\");
            try
            {
                System.IO.TextWriter ErrHan = new System.IO.StreamWriter(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log", true);
                ErrHan.WriteLine(DateTime.Now.ToString());
                ErrHan.WriteLine(e.Message);
                ErrHan.WriteLine(e.StackTrace);
                ErrHan.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                ErrHan.WriteLine("\n\n");
                ErrHan.Flush();
                ErrHan.Close();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
            }
        }

        private static void CheckLogDirectory(string filePath)
        {

            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
        }

        public static void WriteErrorLog(String e)
        {
            try
            {
                CheckLogDirectory(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\");
                System.IO.TextWriter ErrHan = new System.IO.StreamWriter(SessionVariables.Log_Directory.Trim() + @"\ErrorLogs\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log", true);
                ErrHan.WriteLine(DateTime.Now.ToString());
                ErrHan.WriteLine(e);
                ErrHan.WriteLine("\r\n");
                ErrHan.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                ErrHan.WriteLine("\n\n");
                ErrHan.Flush();
                ErrHan.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
        }
        public static void WriteErrorLog(String e, string path)
        {
            CheckLogDirectory(path + @"\ErrorLogs\");
            System.IO.TextWriter ErrHan = new System.IO.StreamWriter(path + @"\ErrorLogs\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log", true);
            ErrHan.WriteLine(DateTime.Now.ToString());
            ErrHan.WriteLine(e);
            ErrHan.WriteLine("\r\n");
            ErrHan.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            ErrHan.WriteLine("\n\n");
            ErrHan.Flush();
            ErrHan.Close();
        }

        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static int CalculateAgeInYears(DateTime? birthDate)
        {
            DateTime now = DateTime.Now;
            DateTime bdate = ValueConverters.ConvertNullToDatetime(birthDate);
            int age = now.Year - bdate.Year;

            if (now.Month < bdate.Month || (now.Month == bdate.Month && now.Day < bdate.Day))
                age--;

            return age;
        }
    }
}