using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ReliableCabs.Models
{
    public class SubscriptionTrans
    {
        public List<edmx.GetTransDetails_Result> ListTransDetails { get; set; }

        public List<edmx.GetCommissionDetails_Result> ListCommDetails { get; set; }

        public List<edmx.GetRentPaymentHistory_Result> ListRentDetails { get; set; }

        public List<edmx.Subscription> ListSubscription { get; set; }

        public List<edmx.GetTotalTransPayment_Result> ListTotalTransdetails { get; set; }

        public List<edmx.GetTotalTransPayment1_Result> ListTotalTransdetails1 { get; set; }

        public List<edmx.GetLoanAdjustmentDetails_Result> LoanAdjustmentDetails { get; set; }

        public List<edmx.GetCashFundDetails_Result> ListFundsDetails { get; set; }

        public List<edmx.GetSubTransPaymentDetails_Result> ListSubTransPaymentDetails { get; set; }


        public int SubscriptionId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string Terminal { get; set; }
        public string Mobile { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> PendingLoanAmount { get; set; }
        public string DriverLicenceNo { get; set; }
        public Nullable<System.DateTime> SubscriptionStartDate { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
        public string RentFrequency { get; set; }
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<decimal> RentBalance { get; set; }
        public int SubTransId { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        //public Nullable<decimal> NetAmount { get; set; }
        public string Password1 { get; set; }
        public int statusId { get; set; }

    }
}

