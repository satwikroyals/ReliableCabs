using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ReliableCabs.Models
{
    public class ViewModel
    {
        public edmx.Client client { get; set; }
        public edmx.Subscription subscription { get; set; }
        public edmx.Status status { get; set; }
        public edmx.PeriodType periodtype { get; set; }

        public List<edmx.GetDriverDetailsbyStatus_Result> ListDriverbystatus { get; set; }
        public int StatusId { get; set; }
    }
}