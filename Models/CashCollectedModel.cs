using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliableCabs.Models
{
    public class CashCollectedModel
    {
        public List<edmx.CashCollected> ListCashcollected { get; set; }
        public List<edmx.GetCashReturnDetails_Result> ListCashReturnDetails { get; set; }
        public List<edmx.GetCashFundDetails_Result> ListFundsDetails { get; set; }
        public List<edmx.Client> ListDrivers { get; set; }
        public List<edmx.RptdashboardTrans_Result> ListAmountDetails { get; set; }
        public List<edmx.RptdashboardmonthTrans_Result> ListMonthlyAmounts { get; set; }

        public int CashAmountId { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public string RefNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int totalDrivers { get; set; }
       

    }    
}