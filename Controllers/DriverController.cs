using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReliableCabs.edmx;
using ReliableCabs.Models;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace ReliableCabs.Controllers
{
    public class DriverController : Controller
    {
        //ReliableCabs.edmx.OnlineAssetMgmtEntities Assets = new ReliableCabs.edmx.OnlineAssetMgmtEntities();
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                ReliableCabs.Models.ClientModel clientmodel = new ReliableCabs.Models.ClientModel();

              
                clientmodel.ListFundsDetails = Assets.GetCashFundDetails().ToList();
                if (clientmodel.ListFundsDetails.Count > 0)
                {
                    ViewBag.NetAmount = clientmodel.ListFundsDetails.FirstOrDefault().NetAmount;
                }
                else
                {
                    ViewBag.NetAmount = 0.00;
                }

                //clientmodel.ListClients = Assets.Clients.ToList();
                //clientmodel.ListSubscription = Assets.Subscriptions.ToList();
                //List<Status> Status = Assets.Status.ToList();
                //ViewBag.StatusList = new SelectList(Status, "StatusId", "Status1");


                List<Client> clients = Assets.Clients.ToList();
                List<Subscription> subscriptions = Assets.Subscriptions.ToList();
                List<Status> status = Assets.Status.ToList();

                var clientmodel1 = from e in clients
                                     join d in subscriptions on e.ClientId equals d.ClientId 
                                    join i in status on d.StatusId equals i.StatusId
                                   select new ViewModel
                                   {
                                       client = e,
                                       subscription = d,
                                       status = i
                                   };  




                return View(clientmodel1);
               
            }
            else
            {
                return RedirectToAction("Index", "Login");
          
            }
        }

      
        public ActionResult Index1(int id)
        {
              ReliableCabs.Models.ClientModel clientmodel = new ReliableCabs.Models.ClientModel();

                clientmodel.ListClients = Assets.Clients.Where(x=>x.ClientId == id).ToList();
                clientmodel.ListFundsDetails = Assets.GetCashFundDetails().ToList();
                return PartialView("PartialDriverIndex", clientmodel);
              
        }

        public JsonResult MobileCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            var searchdata = Assets.Clients.Where(x => x.Mobile == userdata).FirstOrDefault();
            if (searchdata != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult NameCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            var searchdata = Assets.Clients.Where(x => x.FirstName == userdata).FirstOrDefault();
            if (searchdata != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


        public JsonResult EmailCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {
                var searchdata = db.Clients.Where(x => x.Email == userdata).FirstOrDefault();
                if (searchdata != null)
                {
                     string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(userdata))
                    {
                        ModelState.AddModelError("Email", "Please Enter Correct Email Address");
                        return Json(2);
                    }
                    else
                    {
                        return Json(1);
                    }
                }
                else
                {
                    string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(userdata))
                    {
                        ModelState.AddModelError("Email", "Please Enter Correct Email Address");
                        return Json(2);
                    }
                    else
                    {

                        return Json(0);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult InsertClient(Client customer)
        {

          //  Assets.AddClientDetails(customer.FirstName, customer.LastName, customer.Mobile, customer.Email, customer.DriverLicenceNo, customer.PhysicalAddress, customer.OtherInfo);


            return Json(true);

        }


        public ActionResult InsertDriverDetails()
        {
            ReliableCabs.Models.ClientModel client = new ReliableCabs.Models.ClientModel();
            client.ListClients = Assets.Clients.ToList();

            return View();
        }

        public ActionResult InsertDriverDetail(ClientModel client)
        {
            ReliableCabs.Models.ClientModel clients = new ReliableCabs.Models.ClientModel();
            Assets.AddClientDetails(client.FirstName, client.LastName, client.Mobile, client.Email, client.DriverLicenceNo, client.BankDetails, client.SimNumber, client.NamePlateNumber, client.PhysicalAddress, client.OtherInfo);
            Assets.SaveChanges();

            var detials = Assets.Clients.Where(x => x.FirstName == client.FirstName && x.LastName == client.LastName && x.Mobile == client.Mobile).FirstOrDefault();

            client.ClientId = detials.ClientId;

            Assets.AddSubscription(client.ClientId, client.Terminal, client.StatusId, client.SubscriptionStartDate, client.RentAmount, client.PeriodTypeId, client.SubscriptionEndDate, client.Notes, client.EfposServiceCharge, client.ChitsCommission, client.LoanAmount, client.LoanDate);
            Assets.SaveChanges();

            return Json(true);

        }

        public ActionResult GetDriverDetails(int ClientId)
        {
            ReliableCabs.Models.ClientModel client = new ReliableCabs.Models.ClientModel();
            ViewBag.ClientId = ClientId;
            client.ClientId = ClientId;

            client.ListClients = Assets.Clients.Where(a => a.ClientId == ClientId).ToList();

            foreach (var item in client.ListClients)
            {
                client.ClientId = item.ClientId;
                client.FirstName = item.FirstName;
                client.LastName = item.LastName;
                client.Mobile = item.Mobile;
                client.Email = item.Email;
                client.DriverLicenceNo = item.DriverLicenceNo;
                client.BankDetails = item.BankDetails;
                client.SimNumber = item.SimNumber;
                client.NamePlateNumber = item.NamePlateNumber;
                client.PhysicalAddress = item.PhysicalAddress;
                client.OtherInfo = item.OtherInfo;
            }


            return View("GetDriverDetails", client);

        }

        [HttpPost]
        public ActionResult UpdateDriverDetails(Client client)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                Client updatedClient = (from c in entities.Clients
                                        where c.ClientId == client.ClientId
                                        select c).FirstOrDefault();
                updatedClient.FirstName = client.FirstName;
                updatedClient.LastName = client.LastName;
                updatedClient.Email = client.Email;
                updatedClient.Mobile = client.Mobile;
                updatedClient.DriverLicenceNo = client.DriverLicenceNo;
                updatedClient.BankDetails = client.BankDetails;
                 updatedClient.SimNumber = client.SimNumber;
                 updatedClient.NamePlateNumber = client.NamePlateNumber;
                 updatedClient.PhysicalAddress = client.PhysicalAddress;
                 updatedClient.OtherInfo = client.OtherInfo;

                entities.SaveChanges();
            }

            return Json(true);
        }

       
        [HttpPost]
        public ActionResult DeleteDriverDetails(int ClientId)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                Client client = (from c in entities.Clients
                                   where c.ClientId == ClientId
                                   select c).FirstOrDefault();
                entities.Clients.Remove(client);
                entities.SaveChanges();
            }

            //return new EmptyResult();
            return Json(true);
        }

       
    }
}