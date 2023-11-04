using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReliableCabs.edmx;
using ReliableCabs.Models;

namespace ReliableCabs.Controllers
{
    public class ClientController : Controller
    {
        //ReliableCabs.edmx.OnlineAssetMgmtEntities Assets = new ReliableCabs.edmx.OnlineAssetMgmtEntities();
        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();

        public ActionResult Index()
        {
            ReliableCabs.Models.ClientModel clientmodel = new ReliableCabs.Models.ClientModel();

            clientmodel.ListClients = Assets.Clients.ToList();

            return View(clientmodel);
            //return View(client);
        }

        public ActionResult Indexapi()
        {
            

            return View();
            //return View(client);
        }

        public JsonResult MobileCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            var searchdata = Assets.Clients.Where(x => x.Mobile == userdata).SingleOrDefault();
            if (searchdata != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }


        public ActionResult InsertClient()
        {
            ReliableCabs.Models.ClientModel clientmodel = new ReliableCabs.Models.ClientModel();

            clientmodel.ListClients = Assets.Clients.ToList();

            return View(clientmodel);
        }


        [HttpPost]
        public ActionResult InsertClient(Client customer)
        {
            
          //  Assets.AddClientDetails(customer.FirstName, customer.LastName, customer.Mobile, customer.Email, customer.DriverLicenceNo, customer.PhysicalAddress, customer.OtherInfo);


            return Json(true);
            
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Client customer)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                Client updatedClient = (from c in entities.Clients
                                            where c.ClientId == customer.ClientId
                                            select c).FirstOrDefault();
                updatedClient.FirstName = customer.FirstName;
                updatedClient.LastName = customer.LastName;
                updatedClient.Email = customer.Email;
                updatedClient.Mobile = customer.Mobile;
                updatedClient.DriverLicenceNo = customer.DriverLicenceNo;
                updatedClient.PhysicalAddress = customer.PhysicalAddress;
                
                entities.SaveChanges();
            }

            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            using (ReliablecabsEntities entities = new ReliablecabsEntities())
            {
                Client customer = (from c in entities.Clients
                                     where c.ClientId == customerId
                                     select c).FirstOrDefault();
                entities.Clients.Remove(customer);
                entities.SaveChanges();
            }

            //return new EmptyResult();
            return Json(true);
        }
    }
}
