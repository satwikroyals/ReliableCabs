//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReliableCabs.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientReturnTran
    {
        public int ClientReturnTransId { get; set; }
        public int ClientId { get; set; }
        public Nullable<int> SubTransId { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public Nullable<decimal> CashAmount { get; set; }
        public Nullable<decimal> ChequeAmount { get; set; }
        public string ChequeNumber { get; set; }
        public Nullable<decimal> OnlineAmount { get; set; }
        public Nullable<decimal> AdvanceAdjustment { get; set; }
        public Nullable<decimal> LoanToDriver { get; set; }
        public string Remarks { get; set; }
    }
}
