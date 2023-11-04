using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReliableCabs.Models
{
    public class ClientModel
    {
        public List<edmx.Client> ListClients { get; set; }
        public List<edmx.GetCashFundDetails_Result> ListFundsDetails { get; set; }
        public List<edmx.Subscription> ListSubscription { get; set; }
        public List<edmx.getdriversdetails_Result> ListDriverDetails { get; set; }

        
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }

        public string DriverLicenceNo { get; set; }
        public string BankDetails { get; set; }
        public string SimNumber { get; set; }
        public string NamePlateNumber { get; set; }
        public string PhysicalAddress { get; set; }
        public string OtherInfo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

      
        public string Terminal { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<System.DateTime> SubscriptionStartDate { get; set; }
        public Nullable<decimal> RentAmount { get; set; }
        public Nullable<int> PeriodTypeId { get; set; }
        public Nullable<System.DateTime> SubscriptionEndDate { get; set; }
        public string Notes { get; set; }
        public Nullable<decimal> EfposServiceCharge { get; set; }
        public Nullable<decimal> ChitsCommission { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<System.DateTime> LoanDate { get; set; }
    }

 
}