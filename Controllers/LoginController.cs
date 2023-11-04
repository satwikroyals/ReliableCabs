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
using System.Text.RegularExpressions;


namespace ReliableCabs.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (Session["ErrorMsg"] != null)
            {
                ViewBag.ErrorMsg = Session["ErrorMsg"];
            }
            return View();
        }
        //public ActionResult Autorize(string Name, string Password, string Email,string Mobile)

        public JsonResult NameCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {
                var searchdata = db.Logins.Where(x => x.Name == userdata).SingleOrDefault();
                if (searchdata != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
        }

        public JsonResult MobileCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {
                var searchdata = db.Logins.Where(x => x.Mobile == userdata).SingleOrDefault();
                if (searchdata != null)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
        }

        public JsonResult EmailCheckAvailability(string userdata)
        {
            System.Threading.Thread.Sleep(150);
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {
                var searchdata = db.Logins.Where(x => x.Email == userdata).SingleOrDefault();
                if (searchdata != null)
                {
                    return Json(1);
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
        public ActionResult Autorize(LoginModel logindetails)
        {
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {
                var userDetails = db.Logins.Where(x => x.Name == logindetails.Name && x.Password == logindetails.Password).FirstOrDefault();
                ReliableCabs.Models.LoginModel login = new ReliableCabs.Models.LoginModel();


                if (userDetails == null)
                {
                    ViewBag.ErrorMsg = "Wrong username or password.";
                    return View("Index");
                }
                else
                {
                    Session["UserId"] = userDetails.UserId;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

        }


        public ActionResult Register()
        {
            return View();
        }


        //public ActionResult Register1(string Name, string Password, string Email, string Mobile)
        [HttpPost]
        public ActionResult Register1(LoginModel details)
        {

            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {

                db.AddLoginDetails(details.Name, details.Password, details.Email, details.Mobile);
                db.SaveChanges();
                ViewBag.ErrorMsg = "Registered Successfully! Please login";
                Session["ErrorMsg"] = ViewBag.ErrorMsg;
                return RedirectToAction("Index", "Login");
            }

        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ForgotPasswords(string Email)
        {
            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {


                ReliableCabs.Models.LoginModel clientmodel = new ReliableCabs.Models.LoginModel();

                var client = db.Logins.Where(x => x.Email == Email).ToList().FirstOrDefault();

                if (client != null)
                {
                    WebClient webClient = new WebClient();
                    string path = HttpContext.Server.MapPath("~/Content/Email/ForgotPassword.html");
                    Stream stream = webClient.OpenRead(path);
                    StreamReader reader = new StreamReader(stream);
                    string readFile = reader.ReadToEnd();
                    string StrContent = "";
                    StrContent = readFile;
                    StrContent = StrContent.Replace("#UserId#", client.UserId.ToString());
                    StrContent = StrContent.Replace("#Name#", client.Name.ToString());
                    StrContent = StrContent.Replace("#Email#", client.Email.ToString());
                    StrContent = StrContent.Replace("#Password#", client.Password.ToString());


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
                    MailMsg.CC.Add("cnallimelli@gmail.com");
                    MailMsg.CC.Add("reliablecabs@yahoo.co.nz");
                    mailClient.Send(MailMsg);

                    ViewBag.ErrorMsg = "Password sent to your mail";
                    return View("ForgotPassword");
                }
                else
                {

                    ViewBag.ErrorMsg = "Email does not exist";
                    return View("ForgotPassword");
                }
            }
        }


        public ActionResult resetPassword()
        {
            return View();
        }

        public ActionResult resetPasswords(string Name, string Password, string Confirm)
        {
            ReliableCabs.Models.LoginModel clientmodel = new ReliableCabs.Models.LoginModel();

            using (ReliablecabsEntities db = new ReliablecabsEntities())
            {

                var client = db.Logins.Where(x => x.Name == Name).ToList().FirstOrDefault();
                if (client != null)
                {
                    if (Password != Confirm)
                    {
                        ViewBag.ErrorMsg = "Password does not match with Confirm Password";
                        return View("resetPassword");
                    }
                    else
                    {

                        client.Password = Password;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        ViewBag.ErrorMsg = "New password has been reset successfully";
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "Username does not exist";
                    return View("resetPassword");
                }



            }
        }
    }
}
