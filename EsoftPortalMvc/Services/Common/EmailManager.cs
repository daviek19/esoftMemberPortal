using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using EsoftPortalMvc.Services.Common;

namespace EstateManagementMvc.Services.Common
{

    public class EmailManager
    {
        private static String To;
        private static String Subject;
        private static String Message;
        private static bool AsHtmlMessage;
        private static string FilePath;

        //SEND MAIL CONFIGURATIONS
        private string From = System.Configuration.ConfigurationManager.AppSettings["EMAIL_FROM"].ToString();
        private const string Host = "smtp.gmail.com";
        private const int Port = 587;
        private string CredentialsAccountEmail = System.Configuration.ConfigurationManager.AppSettings["EMAIL_FROM"].ToString();
        private const string CredentialsAccountPassword = "12345@54321";
        private const bool EnableSsl = true;

        private static SmtpClient smtp;
        private static MailMessage mail;


        public EmailManager()
        {
            //Configure the SMTP Client
            smtp = new SmtpClient(Host, Port);
            smtp.Credentials = new NetworkCredential(CredentialsAccountEmail, CredentialsAccountPassword);
            smtp.EnableSsl = EnableSsl;
        }

        public void SendEmail(string to, string subject, string message, bool asHtmlMessage = false)
        {
            To = to;
            Subject = subject;
            Message = message;
            AsHtmlMessage = asHtmlMessage;

            Send();
        }

        public void SendEmail(string to, string subject, string message, string filePath, bool asHtmlMessage = false)
        {
            To = to;
            Subject = subject;
            Message = message;
            AsHtmlMessage = asHtmlMessage;
            FilePath = filePath;

            Send();
        }

        private void Send()
        {
            try
            {
                //Send the Actual Email
                mail = new MailMessage();
                mail.From = new MailAddress(From);
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Message;
                mail.IsBodyHtml = AsHtmlMessage;

                if (!string.IsNullOrWhiteSpace(FilePath))
                {
                    mail.Attachments.Add(new Attachment(FilePath));
                }
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("Failed to send email " + ex.Message, ref ex);
            }

        }

    }

}