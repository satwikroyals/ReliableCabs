using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReliableCabs.Models
{
    public class SubscriptionTransInsert
    {
        public int Clientid { get; set; }
        public int SubTransId { get; set; }
        public int SubscriptionId { get; set; }
        public Nullable<decimal> EfposAmount { get; set; }
        public Nullable<decimal> ChitsAmount { get; set; }
        public Nullable<decimal> EfposServiceCharge { get; set; }
        public Nullable<decimal> ChitsCommission { get; set; }
        public Nullable<decimal> chitsReturnAmount { get; set; }
        public Nullable<decimal> eftposReturnAmount { get; set; }
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<decimal> EftposchargeAmount { get; set; }
        public Nullable<decimal> ChitsChargeAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Comments { get; set; }

        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> ChequeAmount { get; set; }
        public string ChequeNumber { get; set; }
        public Nullable<decimal> OnlineAmount { get; set; }
        public Nullable<decimal> AdvanceAmount { get; set; }
        public Nullable<decimal> LoanCashAmount { get; set; }
        public Nullable<decimal> LoanChequeAmount { get; set; }
        public string LoanChequeNumber { get; set; }
        public Nullable<decimal> LoanOnlineAmount { get; set; }
        public Nullable<decimal> LoanToDriver { get; set; }
        public string RentRemarks { get; set; }
        public DateTime Transdate { get; set; }
    }
}