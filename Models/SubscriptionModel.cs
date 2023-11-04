using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReliableCabs.Models
{
    public class SubscriptionModel
    {
        public int SubscriptionId { get; set; }
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Terminal is required ")]
        public string Terminal { get; set; }

        [Required(ErrorMessage = "Status is required ")]
        public Nullable<int> StatusId { get; set; }

        [Required(ErrorMessage = "SubscriptionStartDate is required ")]
        //public DateTime? SubscriptionStartDate { get; set; }
        public Nullable<System.DateTime> SubscriptionStartDate { get; set; }
        public string ModifiedDateString { get { return SetDateFormat(this.SubscriptionStartDate); } }

        [Required(ErrorMessage = "RentAmount is required ")]
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<decimal> PresentRentAmount { get; set; }

        [Required(ErrorMessage = "PeriodTypeId is required ")]
        public Nullable<int> PeriodTypeId { get; set; }
        public Nullable<System.DateTime> SubscriptionEndDate { get; set; }
        public string ModifiedEndDateString { get { return SetDateFormat(this.SubscriptionEndDate); } }

        public string Notes { get; set; }
        public int Count { get; set; }
        public string password1 { get; set; }

        [Required(ErrorMessage = "EfposServiceCharge is required ")]
        public Nullable<decimal> EfposServiceCharge { get; set; }

        [Required(ErrorMessage = "ChitsCommission is required ")]
        public Nullable<decimal> ChitsCommission { get; set; }

        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
        
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public List<edmx.Subscription> ListSubscription { get; set; }
        public List<edmx.GetCashFundDetails_Result> ListFundsDetails { get; set; }

        /// <summary>
        /// To set dateformat.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Dateformat(mmm d, yyyy)</returns>
        public static string SetDateFormat(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d/MMM/yyyy}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }


    }


}

        