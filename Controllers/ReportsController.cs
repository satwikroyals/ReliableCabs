using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReliableCabs.edmx;
using ReliableCabs.Models;

namespace ReliableCabs.Controllers
{
    public class ReportsController : Controller
    {
        //ReliableCabs.edmx.OnlineAssetMgmtEntities Assets = new ReliableCabs.edmx.OnlineAssetMgmtEntities();
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();


        public ActionResult Index()
        {

            if (Session["UserId"] != null)
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
            else
            {

                return RedirectToAction("Index", "Login");
            }
        }



        public ActionResult GetSubTransIdDetails(int SubTransId)
        {
            ViewBag.SubTransId = SubTransId;
            ReliableCabs.Models.ReportModel SubTrans = new ReliableCabs.Models.ReportModel();

            SubTrans.GetSubTransByID = Assets.GetSubTransById(SubTransId).ToList();

            foreach (var item in SubTrans.GetSubTransByID)
            {

                SubTrans.TransDate = item.TransDate;
                SubTrans.EfposAmount = item.EfposAmount;
                SubTrans.TransDate = item.TransDate;
                SubTrans.EftposCommCharge = item.EftposCommCharge;
                SubTrans.ChitsAmount = item.ChitsAmount;
                SubTrans.EftposchargeAmount = item.EfposAmount * (item.EftposCommCharge / 100);
                SubTrans.ChitsCommCharge = item.ChitsCommCharge;
                SubTrans.ChitsChargeAmount = item.ChitsAmount * (item.ChitsCommCharge / 100);
                SubTrans.RentAmount = item.RentAmount;
                SubTrans.CashAmount = item.CashAmount;
                SubTrans.ChequeAmount = item.ChequeAmount;
                SubTrans.ChequeNumber = item.ChequeNumber;
                SubTrans.OnlineAmount = item.OnlineAmount;
                SubTrans.AdvanceAdjustment = item.AdvanceAdjustment;
                SubTrans.LoanToDriver = item.LoanToDriver;
                SubTrans.Comments = item.Remarks;

            }

            return View(SubTrans);

        }


        [HttpPost]
        public ActionResult EditSubTransDetails(ReportModel model)
        {
            ReliableCabs.Models.ReportModel report = new ReliableCabs.Models.ReportModel();

            Assets.UpdateTransDetails(model.SubTransId, model.EfposAmount, model.ChitsAmount, model.CashAmount, model.ChequeAmount, model.ChequeNumber, model.RentAmount, model.OnlineAmount, model.AdvanceAdjustment, model.LoanToDriver, model.Comments);
            Assets.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDriverDetails()
        {
            List<Client> clients = Assets.Clients.ToList();
            List<Subscription> subscriptions = Assets.Subscriptions.ToList();
            List<Status> status = Assets.Status.ToList();
            ViewBag.StatusList = new SelectList(status, "StatusId", "Status1");

            //List<PeriodType> periodtype = Assets.PeriodTypes.ToList();

            //var clientmodel1 = from e in clients
            //                   join d in subscriptions on e.ClientId equals d.ClientId
            //                   join i in status on d.StatusId equals i.StatusId
            //                   join p in periodtype on d.PeriodTypeId equals p.PeriodTypeId
            //                   select new ViewModel
            //                   {
            //                       client = e,
            //                       subscription = d,
            //                       status = i,
            //                       periodtype = p

            //                   };
            int statusId = 0;

            ReliableCabs.Models.ViewModel clientmodel1 = new ReliableCabs.Models.ViewModel();
            clientmodel1.ListDriverbystatus = Assets.GetDriverDetailsbyStatus(statusId).ToList();


            ReliableCabs.Models.ReportModel SubTrans = new ReliableCabs.Models.ReportModel();
            SubTrans.ListFundsDetails = Assets.GetCashFundDetails().ToList();
            ViewBag.NetAmount = SubTrans.ListFundsDetails.FirstOrDefault().NetAmount;
           

            return View(clientmodel1);
            
        }

          [HttpPost]
        public ActionResult GetclientStatusDetails(int statusId)
        {
            ReliableCabs.Models.ViewModel clientmodel1 = new ReliableCabs.Models.ViewModel();
            clientmodel1.ListDriverbystatus = Assets.GetDriverDetailsbyStatus(statusId).ToList();

            ReliableCabs.Models.ReportModel model1 = new ReliableCabs.Models.ReportModel();
            model1.ListFundsDetails = Assets.GetCashFundDetails().ToList();
            ViewBag.NetAmount = model1.ListFundsDetails.FirstOrDefault().NetAmount;


            return View("GetDriverDetails", clientmodel1);

        }
    }
}
