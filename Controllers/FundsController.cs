using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReliableCabs.edmx;
using ReliableCabs.Models;

namespace ReliableCabs.Controllers
{
    public class FundsController : Controller
    {
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();

        public ActionResult Index()
        {

            if (Session["UserId"] != null)
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
                //return PartialView("PartialSubscription", Sub);

            }
            else
            {
                return RedirectToAction("Index", "Login");

            }

        }



        public ActionResult Indexpage()
        {

            if (Session["UserId"] != null)
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
                //return PartialView("PartialSubscription", Sub);

            }
            else
            {
                return RedirectToAction("IndexPage", "Login");

            }

        }



        public ActionResult PartialFunds()
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
            return View("PartialFunds",cash);

        }

        public ActionResult PartialFunds1()
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
            return View("PartialFunds1", cash);

        }

        [HttpPost]
        public JsonResult InsertCash(CashCollected cashdetails)
        {

            ReliableCabs.Models.CashCollectedModel cash = new ReliableCabs.Models.CashCollectedModel();
            cash.CashAmount = cashdetails.CashAmount;
            cash.RefNo = cashdetails.RefNo;
           Assets.AddCashCollectedDetails(cash.CashAmount,cash.RefNo);
           Assets.SaveChanges();
          
            return Json(cashdetails);
        }

        [HttpPost]
        public JsonResult DeleteCash(int CashAmountid)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                CashCollected cash = (from c in entities.CashCollecteds
                                           where c.CashAmountId == CashAmountid
                                 select c).FirstOrDefault();
                entities.CashCollecteds.Remove(cash);
                entities.SaveChanges();
            }
          return Json(true);
        }


    }
}
