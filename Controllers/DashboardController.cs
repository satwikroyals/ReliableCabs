using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReliableCabs.edmx;
using ReliableCabs.Models;

namespace ReliableCabs.Controllers
{
    public class DashboardController : Controller
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
                cash.totalDrivers = Assets.Clients.ToList().Count();
                cash.ListAmountDetails = Assets.RptdashboardTrans().ToList();
                cash.ListMonthlyAmounts = Assets.RptdashboardmonthTrans().ToList();

                                    
                return View(cash);

            }
            else
            {
                return RedirectToAction("Index", "Login");

            }
        }

       

    }
}
