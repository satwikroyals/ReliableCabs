using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReliableCabs.edmx;

namespace ReliableCabs.Services
{
    public class testController : ApiController
    {

        ReliableCabs.edmx.ReliablecabsEntities Assets = new ReliableCabs.edmx.ReliablecabsEntities();


        [HttpGet]
        //[ActionName("MobileCheckAvailability")]
        public List<Client> MobileCheckAvailability()
        {
            //System.Threading.Thread.Sleep(150);
            var searchdata = Assets.Clients.ToList();
            if (searchdata != null)
            {
                return searchdata;
            }
            else
            {
                return searchdata;
            }
        }

    }
}
