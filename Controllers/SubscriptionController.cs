using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Web.UI.WebControls.Expressions;
using ReliableCabs.edmx;
using ReliableCabs.Models;


namespace ReliableCabs.Controllers
{
    public class SubscriptionController : Controller
    {
        //ReliableCabs.edmx.OnlineAssetMgmtEntities Assets = new ReliableCabs.edmx.OnlineAssetMgmtEntities();
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();



        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                ReliableCabs.Models.SubscriptionModel Subscription = new ReliableCabs.Models.SubscriptionModel();
                //List<Client> Client = Assets.Clients.ToList();
                //ViewBag.ClientList = new SelectList(Client, "ClientId", "FirstName");

                List<Client> ClientList = Assets.Clients.ToList();
                ViewBag.ClientList = new SelectList(ClientList, "ClientId", "FirstName");
                ViewBag.MobileList = new SelectList(ClientList, "ClientId", "Mobile");


                List<Status> Status = Assets.Status.ToList();
                ViewBag.StatusList = new SelectList(Status, "StatusId", "Status1");

                List<PeriodType> PeriodType = Assets.PeriodTypes.ToList();
                ViewBag.PeriodTypeList = new SelectList(PeriodType, "PeriodTypeId", "PeriodType1");

                Subscription.ListFundsDetails = Assets.GetCashFundDetails().ToList();
                if (Subscription.ListFundsDetails.Count > 0)
                {
                    ViewBag.NetAmount = Subscription.ListFundsDetails.FirstOrDefault().NetAmount;
                }
                else
                {
                    ViewBag.NetAmount = 0.00;
                }

                foreach (var item in Subscription.ListFundsDetails)
                {
                    var NetAmount = item.NetAmount;
                    ViewBag.Netamount = NetAmount;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpGet]
        public JsonResult GetClients(string term)
        {

            List<SubscriptionTrans> clients = Assets.Clients.Where(s => s.FirstName.Contains(term))
                .Select(x => new SubscriptionTrans
                {
                    ClientId = x.ClientId,
                    FirstName = x.FirstName,
                    Mobile = x.Mobile
                }).ToList();
            return new JsonResult { Data = clients, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetClientMobiles(string term)
        {

            List<SubscriptionTrans> clients = Assets.Clients.Where(s => s.Mobile.Contains(term))
                .Select(x => new SubscriptionTrans
                {
                    ClientId = x.ClientId,
                    Mobile = x.Mobile
                }).ToList();
            return new JsonResult { Data = clients, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpGet]
        public ActionResult GetSubscriptionDetails(int id)
        {
            ReliableCabs.Models.SubscriptionModel Sub = new ReliableCabs.Models.SubscriptionModel();
            ViewBag.ClientId = id;
            Sub.ClientId = id;

            List<Status> Status = Assets.Status.ToList();
            ViewBag.StatusList = new SelectList(Status, "StatusId", "Status1");

            List<PeriodType> PeriodType = Assets.PeriodTypes.ToList();
            ViewBag.PeriodTypeList = new SelectList(PeriodType, "PeriodTypeId", "PeriodType1");

            Sub.ListSubscription = Assets.Subscriptions.Where(a => a.ClientId == id).ToList() ;
       
            //var client = Assets.Logins.Where(x => x.Name == sub.uname).ToList().FirstOrDefault();
            //Sub.password1 = client.Password;

            if (Sub.ListSubscription.Count > 0)
            {

                foreach (var item in Sub.ListSubscription)
                {
                    Sub.Terminal = item.Terminal;
                    Sub.StatusId = item.StatusId;
                    Sub.SubscriptionStartDate = item.SubscriptionStartDate;
                    Sub.RentAmount = item.RentAmount;
                    Sub.PeriodTypeId = item.PeriodTypeId;
                    Sub.EfposServiceCharge = item.EfposServiceCharge;
                    Sub.ChitsCommission = item.ChitsCommission;
                    Sub.LoanAmount = item.LoanAmount;
                    Sub.LoanDate = item.LoanDate;
                    Sub.Notes = item.Notes;
                    Sub.Count = 1;
                    Sub.SubscriptionEndDate = item.SubscriptionEndDate;
                }
            }
            else
            {
                Sub.Count = 0;
            }

            return PartialView("PartialSubscription", Sub);

        }


   

        [HttpPost]
        public ActionResult AddSubscriptionDetails(SubscriptionModel model)
        {
            //if (ModelState.IsValid)
            //{

                ReliableCabs.Models.SubscriptionModel Subcription = new ReliableCabs.Models.SubscriptionModel();

                var sub = Assets.Subscriptions.Any(a => a.ClientId == model.ClientId);

                if (sub == true)
                {
                    Subcription.ListSubscription = Assets.Subscriptions.Where(a => a.ClientId == model.ClientId).ToList();

                    foreach (var item in Subcription.ListSubscription)
                    {
                        model.SubscriptionId = item.SubscriptionId;
                        model.PresentRentAmount = item.RentAmount;
                }

                    Assets.UpdateSubscription(model.SubscriptionId, model.ClientId, model.Terminal, model.StatusId,model.SubscriptionStartDate, model.RentAmount, model.PeriodTypeId, model.SubscriptionEndDate, model.Notes, model.EfposServiceCharge, model.ChitsCommission, model.LoanAmount, model.LoanDate);
                    Assets.SaveChanges();

                }
                else
                {

                Subcription.ListSubscription = Assets.Subscriptions.Where(a => a.ClientId == model.ClientId).ToList();

                foreach (var item in Subcription.ListSubscription)
                {
                    model.SubscriptionId = item.SubscriptionId;
                    
                }
            
                Assets.AddSubscription(model.ClientId, model.Terminal, model.StatusId, model.SubscriptionStartDate, model.RentAmount, model.PeriodTypeId, model.SubscriptionEndDate, model.Notes, model.EfposServiceCharge, model.ChitsCommission, model.LoanAmount, model.LoanDate);
                    Assets.SaveChanges();
                }

                //return Json(true);
               
            //}
            return Json(true);
        }
    }
}
