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
using System.Net;
using System.Net.Mail;
using WebGrease.Css.Ast.Selectors;
using Microsoft.Ajax.Utilities;

namespace ReliableCabs.Controllers
{
    public class SubscriptionTransController : Controller
    {
        
        //ReliableCabs.edmx.OnlineAssetMgmtEntities Assets = new ReliableCabs.edmx.OnlineAssetMgmtEntities();
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();


        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        // For Subscription dropdown list 

        public ActionResult SubscriptionTrans()
        {
            if (Session["UserId"] != null)
            {
                ReliableCabs.Models.SubscriptionTrans SubscriptionTrans = new ReliableCabs.Models.SubscriptionTrans();
                List<Client> ClientList = Assets.Clients.ToList();
                ViewBag.ClientList = new SelectList(ClientList, "ClientId", "FirstName");
                ViewBag.MobileList = new SelectList(ClientList, "ClientId", "Mobile");

                List<Subscription> SubList = Assets.Subscriptions.ToList();
                ViewBag.TerminalList = new SelectList(SubList, "SubscriptionId", "Terminal");

                SubscriptionTrans.ListFundsDetails = Assets.GetCashFundDetails().ToList();
                if (SubscriptionTrans.ListFundsDetails.Count > 0)
                {
                    ViewBag.NetAmount = SubscriptionTrans.ListFundsDetails.FirstOrDefault().NetAmount;
                }
                else
                {
                    ViewBag.NetAmount = 0.00;
                }

                foreach (var item in SubscriptionTrans.ListFundsDetails)
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
                    Mobile =x.Mobile
                }).ToList();
            return new JsonResult { Data = clients, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpGet]
        public JsonResult GetTerminals(string term)
        {

            List<SubscriptionTrans> subscriptions = Assets.Subscriptions.Where(s =>s.Terminal.Contains(term) && s.StatusId == 1)
                .Select(x => new SubscriptionTrans
                {
                    ClientId = x.ClientId,
                    Terminal = x.Terminal
                }).ToList();
            return new JsonResult { Data = subscriptions, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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


        // For getting SubscriptionTransaction List
        public ActionResult GetTransactionDetails(int id)
        {
            ReliableCabs.Models.SubscriptionTrans SubTrans = new  ReliableCabs.Models.SubscriptionTrans();
            ViewBag.ClientId = id;
            SubTrans.ClientId = id;

            var sub = Assets.Subscriptions.Where(a => a.ClientId == id).FirstOrDefault();

            if (sub != null)
            {
                SubTrans.Terminal = sub.Terminal;
            }
            var client = Assets.Clients.Where(a => a.ClientId == id).First();
            SubTrans.Mobile = client.Mobile;
            SubTrans.ListTransDetails = Assets.GetTransDetails(id).ToList();
            SubTrans.ListCommDetails = Assets.GetCommissionDetails(id).ToList();
           SubTrans.ListRentDetails = Assets.GetRentPaymentHistory(id).ToList();
            SubTrans.LoanAdjustmentDetails = Assets.GetLoanAdjustmentDetails(id).ToList();

            if (SubTrans.LoanAdjustmentDetails.Count > 0)
            {

                foreach (var item in SubTrans.LoanAdjustmentDetails)
                {
                    SubTrans.PendingLoanAmount = Convert.ToDecimal(item.LoanPaid);
                }
            }
            if (SubTrans.ListTransDetails.Count > 0)
            {
                foreach (var item in SubTrans.ListTransDetails)
                {
                    SubTrans.DriverLicenceNo = item.DriverLicenceNo;
                    SubTrans.RentAmount = item.RentAmount;
                    SubTrans.RentBalance = item.RentBalance;
                    SubTrans.RentFrequency = item.RentFrequency;
                    SubTrans.SubscriptionStartDate = item.SubscriptionStartDate;
                    SubTrans.TransDate = item.TransDate;
                    SubTrans.LoanAmount = item.LoanAmount;
                    SubTrans.FirstName = item.Name;
                }
            }

            return PartialView("PartialSubscriptionTrans", SubTrans);

        }

        public ActionResult GetTransactionDetails1(int id)
        {
            ReliableCabs.Models.SubscriptionTrans SubTrans = new ReliableCabs.Models.SubscriptionTrans();
            ViewBag.ClientId = id;
            SubTrans.ClientId = id;
            SubTrans.ListTransDetails = Assets.GetTransDetails(id).ToList();
            SubTrans.ListCommDetails = Assets.GetCommissionDetails(id).ToList();
            SubTrans.ListRentDetails = Assets.GetRentPaymentHistory(id).ToList();
            

            //string uname = "Ranjit";
            //var client = Assets.Logins.Where(x => x.Name == uname).ToList().FirstOrDefault();
            //SubTrans.Password1 = client.Password;

            SubTrans.ListSubscription = Assets.Subscriptions.ToList();
            var SubId = SubTrans.ListSubscription.Where(a => a.ClientId == id).First();
            var Sid = SubId.SubscriptionId;

            var AdvanceAdjustent = Assets.ClientReturnTrans.Where(x => x.ClientId == id).FirstOrDefault();
            //SubTrans.ListSubTransPaymentDetails = Assets.GetSubTransPaymentDetails(SubTrans.SubTransId).ToList();

            SubTrans.ListTotalTransdetails = Assets.GetTotalTransPayment(Sid).ToList();
            SubTrans.SubscriptionId = Sid;
            ViewBag.SubscriptionId = Sid;
            SubTrans.ListTransDetails = Assets.GetTransDetails(id).ToList();
            if (SubTrans.ListTransDetails.Count > 0)
            {
                foreach (var item in SubTrans.ListTransDetails)
                {
                    SubTrans.DriverLicenceNo = item.DriverLicenceNo;
                    SubTrans.RentAmount = item.RentAmount;
                    SubTrans.RentBalance = item.RentBalance;
                    SubTrans.RentFrequency = item.RentFrequency;
                    SubTrans.SubscriptionStartDate = item.SubscriptionStartDate;
                    SubTrans.TransDate = item.TransDate;
                    SubTrans.LoanAmount = item.LoanAmount;
                    SubTrans.FirstName = item.Name;
                }
            }

          
            SubTrans.LoanAdjustmentDetails = Assets.GetLoanAdjustmentDetails(id).ToList();
            foreach (var item in SubTrans.LoanAdjustmentDetails)
            {
                SubTrans.PendingLoanAmount = Convert.ToDecimal(item.PendingLoanAmount);
            }
            return PartialView("PartialSubscriptionTrans1", SubTrans);

        }


        [HttpPost]
        public ActionResult SubTransInsert(SubscriptionTransInsert model)
        {
            var subscription = Assets.Subscriptions.Where(a => a.ClientId == model.Clientid).First();
            model.SubscriptionId = subscription.SubscriptionId;
            model.EfposServiceCharge = subscription.EfposServiceCharge;
            model.ChitsCommission = subscription.ChitsCommission;

            if (model.EfposAmount ==0 && model.ChitsAmount == 0)
            {
                model.RentAmount = 0;
            }
            // Assets.UpdateEftopsDetails(model.SubTransId, model.EfposAmount, model.ChitsAmount, model.Comments);
            Assets.AddSubscriptionTransDetails(model.SubscriptionId, model.EfposAmount, model.ChitsAmount, model.RentAmount, model.EfposServiceCharge, model.ChitsCommission, model.Comments);
            Assets.SaveChanges();

            int subid = model.SubscriptionId;

            var SubTrans = Assets.SubscriptionTrans.Where(a => a.SubscriptionId == subid).OrderByDescending(a => a.SubTransId).First();
            model.SubTransId = SubTrans.SubTransId;

            var client = Assets.Clients.Where(a => a.ClientId == model.Clientid).First();
            // var subscription = Assets.Subscriptions.Where(a => a.ClientId == model.Clientid).First();
            ViewBag.ClientId = model.Clientid;

            if ((model.EfposAmount == 0) && (model.ChitsAmount == 0) ) { model.RentAmount = 0; }

            if (model.ChitsAmount != 0)
            {
                WebClient webClient = new WebClient();
                string path = HttpContext.Server.MapPath("~/Content/Email/SubscriptionEmail.html");
                Stream stream = webClient.OpenRead(path);
                StreamReader reader = new StreamReader(stream);
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;
                StrContent = StrContent.Replace("#FirstName#", client.FirstName.ToString());
                StrContent = StrContent.Replace("#LastName#", client.LastName.ToString());
                //StrContent = StrContent.Replace("#Email#", client.Email.ToString()); 
                StrContent = StrContent.Replace("#Mobile#", client.Mobile.ToString());
                //StrContent = StrContent.Replace("#DriverLicenceNo#", client.DriverLicenceNo.ToString());
                StrContent = StrContent.Replace("#Terminal#", subscription.Terminal.ToString());
                StrContent = StrContent.Replace("#EfposServiceCharge#", subscription.EfposServiceCharge.ToString());
                StrContent = StrContent.Replace("#EfposAmount#", model.EfposAmount.ToString());
                StrContent = StrContent.Replace("#eftposReturnAmount#", model.eftposReturnAmount.ToString());
                StrContent = StrContent.Replace("#ChitsCommission#", subscription.ChitsCommission.ToString());
                StrContent = StrContent.Replace("#ChitsAmount#", model.ChitsAmount.ToString());
                StrContent = StrContent.Replace("#chitsReturnAmount#", model.chitsReturnAmount.ToString());
                StrContent = StrContent.Replace("#TotalAmount#", model.TotalAmount.ToString());
                StrContent = StrContent.Replace("#RentAmount#", model.RentAmount.ToString());
                StrContent = StrContent.Replace("#CashAmount#", model.CashAmount.ToString());
                StrContent = StrContent.Replace("#ChequeAmount#", model.ChequeAmount.ToString());
                StrContent = StrContent.Replace("#ChequeNumber#", model.ChequeNumber.ToString());
                StrContent = StrContent.Replace("#OnlineAmount#", model.OnlineAmount.ToString());
                StrContent = StrContent.Replace("#AdvanceAmount#", model.AdvanceAmount.ToString());
                StrContent = StrContent.Replace("#LoanToDriver#", model.LoanToDriver.ToString());

                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                MailMsg.From = new MailAddress("swarnathomas@gmail.com");
                MailMsg.Subject = "Subscription Transaction Details";
                MailMsg.Body = StrContent;
                MailMsg.IsBodyHtml = true;


                mailClient.Port = 587;
                mailClient.Credentials = new System.Net.NetworkCredential("swarnathomas@gmail.com", "xxxx");
                mailClient.EnableSsl = true;

               // MailMsg.CC.Add("swarnamathew@gmail.com");
                //MailMsg.CC.Add("cnallimelli@gmail.com");
                MailMsg.CC.Add("reliablecabs@yahoo.co.nz");
                mailClient.Send(MailMsg);

                Assets.AddClientReturnTransDetails(model.Clientid, model.SubTransId, model.CashAmount, model.ChequeAmount, model.ChequeNumber, model.OnlineAmount, model.AdvanceAmount, model.LoanToDriver, model.Comments);
                Assets.SaveChanges();

                Assets.AddLoanTransDetails(model.Clientid, model.SubTransId, model.LoanCashAmount, model.LoanChequeAmount, model.LoanChequeNumber, model.LoanOnlineAmount, model.LoanToDriver, model.RentRemarks);
                Assets.SaveChanges();


                return Json(true);
            }
            else
            {
                WebClient webClient = new WebClient();
                string path = HttpContext.Server.MapPath("~/Content/Email/SubscriptionEmail2.html");
                Stream stream = webClient.OpenRead(path);
                StreamReader reader = new StreamReader(stream);
                string readFile = reader.ReadToEnd();
                string StrContent = "";
                StrContent = readFile;
                StrContent = StrContent.Replace("#FirstName#", client.FirstName.ToString());
                StrContent = StrContent.Replace("#LastName#", client.LastName.ToString());
                //StrContent = StrContent.Replace("#Email#", client.Email.ToString()); ;
                StrContent = StrContent.Replace("#Mobile#", client.Mobile.ToString());
                //StrContent = StrContent.Replace("#DriverLicenceNo#", client.DriverLicenceNo.ToString());
                StrContent = StrContent.Replace("#Terminal#", subscription.Terminal.ToString());
                StrContent = StrContent.Replace("#EfposServiceCharge#", subscription.EfposServiceCharge.ToString());
                StrContent = StrContent.Replace("#EfposAmount#", model.EfposAmount.ToString());
                StrContent = StrContent.Replace("#eftposReturnAmount#", model.eftposReturnAmount.ToString());
                StrContent = StrContent.Replace("#ChitsCommission#", subscription.ChitsCommission.ToString());
                StrContent = StrContent.Replace("#ChitsAmount#", model.ChitsAmount.ToString());
                StrContent = StrContent.Replace("#chitsReturnAmount#", model.chitsReturnAmount.ToString());
                StrContent = StrContent.Replace("#TotalAmount#", model.TotalAmount.ToString());
                StrContent = StrContent.Replace("#RentAmount#", model.RentAmount.ToString());
                StrContent = StrContent.Replace("#CashAmount#", model.CashAmount.ToString());
                StrContent = StrContent.Replace("#ChequeAmount#", model.ChequeAmount.ToString());
                StrContent = StrContent.Replace("#ChequeNumber#", model.ChequeNumber.ToString());
                StrContent = StrContent.Replace("#OnlineAmount#", model.OnlineAmount.ToString());
                StrContent = StrContent.Replace("#AdvanceAmount#", model.AdvanceAmount.ToString());
                StrContent = StrContent.Replace("#LoanToDriver#", model.LoanToDriver.ToString());

                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                MailMsg.From = new MailAddress("swarnathomas@gmail.com");
                MailMsg.Subject = "Subscription Transaction Details";
                MailMsg.Body = StrContent;
                MailMsg.IsBodyHtml = true;


                mailClient.Port = 587;
                mailClient.Credentials = new System.Net.NetworkCredential("swarnathomas@gmail.com", "xxxx");
                mailClient.EnableSsl = true;

                MailMsg.CC.Add("swarnamathew@gmail.com");
                //MailMsg.CC.Add("cnallimelli@gmail.com");
                MailMsg.CC.Add("reliablecabs@yahoo.co.nz");
                mailClient.Send(MailMsg);

                Assets.AddClientReturnTransDetails(model.Clientid, model.SubTransId, model.CashAmount, model.ChequeAmount, model.ChequeNumber, model.OnlineAmount, model.AdvanceAmount, model.LoanToDriver, model.Comments);
                Assets.SaveChanges();

                Assets.AddLoanTransDetails(model.Clientid, model.SubTransId, model.LoanCashAmount, model.LoanChequeAmount, model.LoanChequeNumber, model.LoanOnlineAmount, model.LoanToDriver, model.RentRemarks);
                Assets.SaveChanges();

                return Json(true);
            }
        }

        [HttpGet]
        public ActionResult GetTransHistory(int id)
        {
            ReliableCabs.Models.PaymentHistoryModel SubTrans = new ReliableCabs.Models.PaymentHistoryModel();
            ReliableCabs.Models.SubscriptionTrans sub = new ReliableCabs.Models.SubscriptionTrans();

            sub.ListSubscription = Assets.Subscriptions.ToList();
                       
            var SubId = sub.ListSubscription.Where(a => a.ClientId == id).First();
            int Sid = SubId.SubscriptionId;

                 if (Sid != 0)
                 {

                     SubTrans.ListTotalTransdetails = Assets.GetTotalTransPayment(Sid).ToList();
                     SubTrans.ListTotalTransdetails1 = Assets.GetTotalTransPayment1(Sid).ToList();
                 }
         

            SubTrans.ListRentDetails = Assets.GetRentPaymentHistory(id).ToList();

            SubTrans.ListEftposDetails = Assets.GetEftposPaymentHistory1(id).ToList();           
       
            SubTrans.ListChitsDetails = Assets.GetChitsPaymentHistory1(id).ToList();

            SubTrans.ListPaymentdetails = Assets.GetPaymentsHistory(id).ToList();

            foreach (var item in SubTrans.ListPaymentdetails)
            {
                SubTrans.C_ChitsCharge = item.C_ChitsCharge;
                SubTrans.C_EftposCharge = item.C_EftposCharge;

            }



            return PartialView("PartialSubTransHistory1", SubTrans);

        }

          [HttpPost]
        public ActionResult GetPartialSubTransModal(int Clientid, int TotalAmount)
        {
            ReliableCabs.Models.PaymentHistoryModel SubTrans = new ReliableCabs.Models.PaymentHistoryModel();

            ReliableCabs.Models.SubscriptionTrans sub = new ReliableCabs.Models.SubscriptionTrans();

            sub.ListSubscription = Assets.Subscriptions.ToList();
            var SubId = sub.ListSubscription.Where(a => a.ClientId == Clientid).First();
            var Sid = SubId.SubscriptionId;

            SubTrans.ListTotalTransdetails = Assets.GetTotalTransPayment(Sid).ToList();
            SubTrans.ClientId = Clientid;
            //ViewBag.ClientId = Clientid;
            sub.SubscriptionId = Sid;
            ViewBag.SubscriptionId = Sid;

            SubTrans.TotalAmount = TotalAmount;
            ViewBag.TotalAmount = TotalAmount;

            sub.LoanAdjustmentDetails = Assets.GetLoanAdjustmentDetails(Clientid).ToList();
            foreach (var item in sub.LoanAdjustmentDetails)
            {
                SubTrans.PendingLoanAmount = Convert.ToDecimal(item.LoanPaid);
            }

            return PartialView("PartialSubTransModal", SubTrans);

        }



        [HttpPost]
        public ActionResult DeleteTransDetails(int SubTransId)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                SubscriptionTran subTrans = (from c in entities.SubscriptionTrans
                                 where c.SubTransId == SubTransId
                                 select c).FirstOrDefault();

                ClientReturnTran returnTrans = (from c in entities.ClientReturnTrans
                                             where c.SubTransId == SubTransId
                                             select c).FirstOrDefault();

                LoanTran loanTrans = (from c in entities.LoanTrans
                                       where c.SubTransId == SubTransId
                                                select c).FirstOrDefault();

                entities.SubscriptionTrans.Remove(subTrans);
                entities.ClientReturnTrans.Remove(returnTrans);
                entities.LoanTrans.Remove(loanTrans);
                entities.SaveChanges();
            }

            //return new EmptyResult();
            return Json(true);
        }


        public ActionResult GetSubTransPaymentDetails(int SubTransId)
        {
            ReliableCabs.Models.SubscriptionTrans SubTrans = new ReliableCabs.Models.SubscriptionTrans();
           
            var subtranlist = Assets.SubscriptionTrans.Where(x => x.SubTransId == SubTransId).ToList().FirstOrDefault();
            var sublist = Assets.Subscriptions.Where(x => x.SubscriptionId == subtranlist.SubscriptionId).ToList().FirstOrDefault();

            SubTrans.SubTransId = SubTransId;
            SubTrans.SubscriptionId = subtranlist.SubscriptionId;
            SubTrans.ClientId = sublist.ClientId;
            var id = SubTrans.ClientId;
            SubTrans.statusId = 1;

            SubTrans.ListSubTransPaymentDetails = Assets.GetSubTransPaymentDetails(SubTransId).ToList();
           
            string uname = "Ranjit";
            var client = Assets.Logins.Where(x => x.Name == uname).ToList().FirstOrDefault();
            SubTrans.Password1 = client.Password;



            SubTrans.LoanAdjustmentDetails = Assets.GetLoanAdjustmentDetails(id).ToList();
            foreach (var item in SubTrans.LoanAdjustmentDetails)
            {
                SubTrans.PendingLoanAmount = Convert.ToDecimal(item.PendingLoanAmount);
            }
            return PartialView("PartialSubscriptionTrans1", SubTrans);

        }



        [HttpPost]
        public ActionResult Deleteloanamt(int Clientid)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                entities.spdeleteloan(Clientid);
                entities.SaveChanges();
            }

            //return new EmptyResult();
            return Json(true);
        }


    }

}
  
 

