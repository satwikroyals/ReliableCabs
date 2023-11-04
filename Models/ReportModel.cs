using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliableCabs.Models
{
    public class ReportModel
    {
        public List<edmx.GetCashFundDetails_Result> ListFundsDetails { get; set; }
        public List<edmx.GetDriverDetailsbyStatus_Result> ListDriverbystatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Terminal { get; set; }
        public Nullable<decimal> EftposCommCharge { get; set; }
        public Nullable<decimal> ChitsCommCharge { get; set; }
        public Nullable<decimal> EftposchargeAmount { get; set; }
        public Nullable<decimal> ChitsChargeAmount { get; set; }
        public int SubTransId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public Nullable<decimal> EfposAmount { get; set; }
        public Nullable<decimal> ChitsAmount { get; set; }
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Comments { get; set; }
       public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> ChequeAmount { get; set; }
         public Nullable<decimal> OnlineAmount { get; set; }
         public Nullable<decimal> AdvanceAdjustment { get; set; }
         public Nullable<decimal> LoanToDriver { get; set; }
         public string ChequeNumber { get; set; }
         public Nullable<decimal> NetReturnAmount { get; set; }


         public List<edmx.RptAllSubTransDetails_Result> ListAllSubTransDetails { get; set; }
         public List<edmx.RptSubTransDetails_Result> ListSubTransDetails { get; set; }
         public List<edmx.GetSubTransById_Result> GetSubTransByID { get; set; }
         public List<edmx.RptListDriverDetails_Result> GetDriverDetails { get; set; }

    }
}