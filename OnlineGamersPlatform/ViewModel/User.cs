using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGamersPlatform.ViewModel
{
    public class User
    {
        [Required(ErrorMessage = "Please enter your e-mail.")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Please enter your password.")]
        public string Password { get; set; }
    }
}