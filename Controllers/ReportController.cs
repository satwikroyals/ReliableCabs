using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
//using ClosedXML.Excel;

namespace ReliableCabs.Controllers
{
    public class ReportController : Controller
    {
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult RptGetAllDrivers(int StatusId)
        {
            ReliableCabs.Models.ReportModel reports = new ReliableCabs.Models.ReportModel();
            var statusid = StatusId;
            reports.GetDriverDetails = Assets.RptListDriverDetails(statusid).ToList();

            reports.ListFundsDetails = Assets.GetCashFundDetails().ToList();
            if (reports.ListFundsDetails.Count > 0)
            {
                ViewBag.NetAmount = reports.ListFundsDetails.FirstOrDefault().NetAmount;
            }
            return View(reports);
        }

         public ActionResult RptGetDriverTrans()
        {
            ReliableCabs.Models.ReportModel SubTrans = new ReliableCabs.Models.ReportModel();

            SubTrans.ListAllSubTransDetails = Assets.RptAllSubTransDetails().ToList();
            foreach (var item in SubTrans.ListAllSubTransDetails)
            {
                SubTrans.SubTransId = item.SubTransId;
                SubTrans.ClientId = item.ClientId;
                SubTrans.Terminal = item.Terminal;
                SubTrans.TransDate = item.TransDate;
                SubTrans.EfposAmount = item.EfposAmount;
                SubTrans.TransDate = item.TransDate;
                SubTrans.EftposCommCharge = item.EftposCommCharge;
                SubTrans.ChitsCommCharge = item.ChitsCommCharge;
                SubTrans.ChitsAmount = item.ChitsAmount;
                SubTrans.RentAmount = item.RentAmount;
                SubTrans.CashAmount = item.CashAmount;
                SubTrans.ChequeAmount = item.ChequeAmount;
                SubTrans.OnlineAmount = item.OnlineAmount;
                SubTrans.NetReturnAmount = item.NetReturnAmount;
                SubTrans.EftposchargeAmount = item.EftposchargeAmount1;
                SubTrans.ChitsChargeAmount = item.ChitschargeAmount1;
            }
            SubTrans.ListFundsDetails = Assets.GetCashFundDetails().ToList();
            if (SubTrans.ListFundsDetails.Count > 0)
            {
                ViewBag.NetAmount = SubTrans.ListFundsDetails.FirstOrDefault().NetAmount;
            }
            else
            {
                ViewBag.NetAmount = 0.00;
            }
            return View(SubTrans);
           
        }


           public ActionResult RptCashCredited()
        {
                ReliableCabs.Models.CashCollectedModel cash = new ReliableCabs.Models.CashCollectedModel();
                cash.ListCashcollected = Assets.CashCollecteds.ToList();
                cash.ListCashReturnDetails = Assets.GetCashReturnDetails().ToList();
                cash.ListFundsDetails = Assets.GetCashFundDetails().ToList();
                if (cash.ListFundsDetails.Count > 0)
                {
                    ViewBag.NetAmount = cash.ListFundsDetails.FirstOrDefault().NetAmount;
                }
                else
                {
                    ViewBag.NetAmount = 0.00;
                }
                return View(cash);
                

            }
        

    }
}
