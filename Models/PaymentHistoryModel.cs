using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliableCabs.Models
{
    public class PaymentHistoryModel
    {
        public List<edmx.GetRentPaymentHistory_Result> ListRentDetails { get; set; }

        public List<edmx.GetEftposPaymentHistory1_Result> ListEftposDetails { get; set; }

        public List<edmx.GetChitsPaymentHistory1_Result> ListChitsDetails { get; set; }

        public List<edmx.GetTotalTransPayment_Result> ListTotalTransdetails { get; set; }

        public List<edmx.GetLoanAdjustmentDetails_Result> LoanAdjustmentDetails { get; set; }

        public List<edmx.GetTotalTransPayment1_Result> ListTotalTransdetails1 { get; set; }

        public List<edmx.GetPaymentsHistory_Result> ListPaymentdetails { get; set; }
        


        public int SubscriptionId { get; set; }
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> PendingLoanAmount { get; set; }
        public Nullable<decimal> C_ChitsCharge { get; set; }       
        public Nullable<decimal> C_EftposCharge { get; set; }
        public int statusId { get; set; }
    }
}