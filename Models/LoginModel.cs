using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ReliableCabs.Models
{
    public class LoginModel
    {
        public List<edmx.Login> login { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserName is required ")]
        public string Name { get; set; }
        

        [Required(ErrorMessage = "Password is required ")]
        public string Password { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required(ErrorMessage = "The Email is required")]       
        public string Email { get; set; }
       

        public string Mobile { get; set; }
        [Required(ErrorMessage = "Mobile is required ")]

        public string ErrorMsg { get; set; }
        
    }
}