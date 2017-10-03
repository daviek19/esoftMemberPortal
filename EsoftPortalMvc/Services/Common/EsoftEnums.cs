using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Common
{
    /// <summary>
    /// Used By Customer Register 
    /// </summary>
    public class StaticEnumerations
    {
        public StaticEnumerations()
        {
            Salutations = new List<Salutations>{
                new Salutations{Name="Mr", Value="Mr."},
                new Salutations{Name="Ms", Value="Ms."},
                new Salutations{Name="Mrs", Value="Mrs."},
                new Salutations{Name="Miss", Value="Miss."},
                new Salutations{Name="Prof", Value="Prof."},
                new Salutations{Name="Dr", Value="Dr."},
                new Salutations{Name="Uknown", Value="U"}
            };
            Gender = new List<Gender>
            {
                new Gender{Name="Male", Value="M"},
                new Gender{Name="Female", Value="F"},
                new Gender{Name="Corporate", Value="C"},
                new Gender{Name="Group", Value="G"},
                new Gender{Name="Unknown", Value="U"}
            };
            MaritalStatus = new List<MaritalStatus> {
                new MaritalStatus{Name="Single",Value="S"},
                new MaritalStatus{Name="Married",Value="M"},
                new MaritalStatus{Name="Widowed",Value="W"},
                new MaritalStatus{Name="Divorced",Value="D"},
                new MaritalStatus{Name="Separated",Value="S"},
                new MaritalStatus{Name="Unknown",Value="U"}
            };
            Identification_Types = new List<Identification_Types>{
                new Identification_Types{Value="I",Name="Id Card Number",MaxLength= 10},
                new Identification_Types{Value="P",Name="Passport Number",MaxLength= 9},
                new Identification_Types{Value="C",Name="Certificate of Incorporation",MaxLength= 15},
                new Identification_Types{Value="G",Name="Group Reg Cert",MaxLength= 15},
                new Identification_Types{Value="U",Name="Unknown",MaxLength= 0}
            };

              
            Relations = new List<RelationShips>();
            GetOtherCodes();
        }

        public List<Salutations> Salutations { get; set; }
        public List<Gender> Gender { get; set; }
        public List<MaritalStatus> MaritalStatus { get; set; }
        public List<Identification_Types> Identification_Types { get; set; }
        public List<CountyCodesView> CountyCode { get; set; }
        public List<EmployerCodes> EmployerCodes { get; set; }
        public List<PostalCodes> PostalCodes { get; set; }
       
        public List<RelationShips> Relations { get; set; }

        void GetOtherCodes()
        {
            GetCountyCodes();
            GetEmployerCodes();
            GetPostalCodes();
           // Relations = SessionVariables.GetRelationShips();
        }

       


        private void GetCountyCodes()
        {
            //List<CountyCodesView> xx = new List<CountyCodesView>();
            //var codes = CountyCodesManager.GetCountyCodes();
            //foreach (var item in codes)
            //{
            //    xx.Add(
            //        new CountyCodesView
            //        {
            //            Value = item.CountyCode,
            //            Name = item.CountyCode.Trim() + ": " + item.CountyName.Trim()
            //        });
            //}
            //CountyCode = new List<CountyCodesView>();
            //CountyCode.AddRange(xx);

        }
        private void GetEmployerCodes()
        {
            //var codes = EmployerManager.GetEmployerCodes();
            //List<EmployerCodes> xx = new List<EmployerCodes>();
            //foreach (var item in codes)
            //{
            //    xx.Add(
            //        new EmployerCodes
            //        {
            //            Value = item.DptCode,
            //            Name = item.DptCode.Trim() + ": " + item.DptName.Trim()
            //        });
            //}
            //EmployerCodes = new List<EmployerCodes>();
            //EmployerCodes.AddRange(xx);
        }
        private void GetPostalCodes()
        {
            //var codes = PostalCodesManager.GetPostalCodes();
            //List<PostalCodes> xx = new List<PostalCodes>();
            //foreach (var item in codes)
            //{
            //    xx.Add(
            //        new PostalCodes
            //        {
            //            Value = item.code,
            //            Name = item.code.Trim() + ": " + item.name.Trim()
            //        });
            //}
            //PostalCodes = new List<PostalCodes>();
            //PostalCodes.AddRange(xx);
        }
    }
    public class RelationShips
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class Salutations
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class Gender
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class MaritalStatus
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class Identification_Types
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int MaxLength { get; set; }
    }
    public class CountyCodesView
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class EmployerCodes
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class PostalCodes
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}