using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Common
{
    public class AuditTrail
    {
        private tbl_trail _tbl_trail = null;


        public void CreateTrailRecord(Esoft_EstateEntities db, string loginCode, string activity,
            decimal trAmount, string accountNo, string postingDocid, bool _saveChanges)
        {


            _tbl_trail = new tbl_trail();
            _tbl_trail.AccountNo = accountNo;
            _tbl_trail.Activity = activity;
            _tbl_trail.ActivityDate = DateTime.Now;
            _tbl_trail.LoginCode = loginCode;
            _tbl_trail.MachineDate = DateTime.Now; // How do we get from client
            _tbl_trail.MachineName = string.IsNullOrWhiteSpace(UserSession.Current.MachineName) ? "User Machine" : UserSession.Current.MachineName;
            _tbl_trail.PostingDocid = postingDocid;
            _tbl_trail.TrAmount = trAmount;
            _tbl_trail.tbl_trailID = GuidGenerator.NewComb();

            db.tbl_trail.Add(_tbl_trail);
            if (_saveChanges)
                db.SaveChanges();

        }
        public static void CreateTrailRecord(string loginCode, string activity,
           decimal trAmount, string accountNo, string postingDocid, bool _saveChanges)
        {
            Esoft_EstateEntities db = new Esoft_EstateEntities();
            tbl_trail _tbl_trailx = null;
            _tbl_trailx = new tbl_trail();
            _tbl_trailx.AccountNo = accountNo;
            _tbl_trailx.Activity = activity;
            _tbl_trailx.ActivityDate = DateTime.Now;
            _tbl_trailx.LoginCode = loginCode;
            _tbl_trailx.MachineDate = DateTime.Now; // How do we get from client
            _tbl_trailx.MachineName = string.IsNullOrWhiteSpace(UserSession.Current.MachineName) ? "User Machine" : UserSession.Current.MachineName;
            _tbl_trailx.PostingDocid = postingDocid;
            _tbl_trailx.TrAmount = trAmount;
            _tbl_trailx.tbl_trailID = GuidGenerator.NewComb();

            db.tbl_trail.Add(_tbl_trailx);
            if (_saveChanges)
                db.SaveChanges();

        }
        public void CreateTrailRecords(string loginCode, string activity,
          decimal trAmount, string accountNo, string postingDocid, bool _saveChanges)
        {
            Esoft_EstateEntities db = new Esoft_EstateEntities();
            tbl_trail _tbl_trailx = null;
            _tbl_trailx = new tbl_trail();
            _tbl_trailx.AccountNo = accountNo;
            _tbl_trailx.Activity = activity;
            _tbl_trailx.ActivityDate = DateTime.Now;
            _tbl_trailx.LoginCode = loginCode;
            _tbl_trailx.MachineDate = DateTime.Now; // How do we get from client
            _tbl_trailx.MachineName = string.IsNullOrWhiteSpace(UserSession.Current.MachineName) ? "User Machine" : UserSession.Current.MachineName;
            _tbl_trailx.PostingDocid = postingDocid;
            _tbl_trailx.TrAmount = trAmount;
            _tbl_trailx.tbl_trailID = GuidGenerator.NewComb();

            db.tbl_trail.Add(_tbl_trailx);
            if (_saveChanges)
                db.SaveChanges();

        }

        public static List<PostTransactionsViewModel> CreateTrailPostingRecord(List<PostTransactionsViewModel> transList, string m_current_posting_reference,
            string _docid, string _mactivity, double _mamount, string _maccount, string _usercode, bool postTransactions)
        {
            _mactivity = ValueConverters.format_sql_string(_mactivity);
            _maccount = ValueConverters.format_sql_string(_maccount);
            _docid = ValueConverters.format_sql_string(_docid);
            _usercode = ValueConverters.format_sql_string(_usercode);
            string _mamountx = ValueConverters.ConvertNullToDouble(_mamount).ToString();
            string _machinename = ValueConverters.ConvertNullToEmptyString(UserSession.Current.MachineName);

            string[] docids = { "9900", "9901", "9902" };

            string _trail_insert_string = "";
            if (ValueConverters.ContainsAny(_docid, docids) && !ValueConverters.IsStringEmpty(_usercode))
            {
                _trail_insert_string = "INSERT INTO TBL_TRAIL(PostingDocid,MachineDate,LOGINCODE,ACTIVITY,ACTIVITYDATE,MACHINENAME,TRAMOUNT,ACCOUNTNO)" +
                 " VALUES('" + _docid + "',getdate(),'" + _usercode + "','" + _mactivity + "',GETDATE(),'" + _machinename + "'," + _mamountx + ",'" + _maccount + "')";
            }
            else
            {
                _trail_insert_string = "INSERT INTO TBL_TRAIL(PostingDocid,MachineDate,LOGINCODE,ACTIVITY,ACTIVITYDATE,MACHINENAME,TRAMOUNT,ACCOUNTNO)" +
                 " VALUES('" + _docid + "',GETDATE(),'" + UserSession.Current.userDetails.LoginCode + "','" + _mactivity + "',GETDATE(),'" + _machinename + "'," + _mamountx + ",'" + _maccount + "')";

            }

            transList.Add(new PostTransactionsViewModel
            {
                SqlStatement = _trail_insert_string,
                TransactionId = m_current_posting_reference,
                TableName = "SQLSTATEMENT"
            });

            if (postTransactions)
            {
                PostTransactions transEnginex = new PostTransactions();
                transEnginex.Post_Transactions(transList, m_current_posting_reference, false, false);
            }
            return transList;
        }
    }
}