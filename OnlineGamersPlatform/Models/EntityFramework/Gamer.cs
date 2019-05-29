//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineGamersPlatform.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Gamer
    {
        public int id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter a name.")]
        public string name { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter a username.")]
        public string username { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please enter an e-mail.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string password { get; set; }
        [Display(Name = "Game Type")]
        [Required(ErrorMessage = "Please choose a game type.")]
        public string gameType { get; set; }

        public string description { get; set; }
        [Display(Name = "Birthday")]
        [Required(ErrorMessage = "Please choose a birthday.")]
        public Nullable<System.DateTime> birthday { get; set; }
        [Display(Name = "Are yo")]
        [Required(ErrorMessage = "Please choose an answer.")]
        public bool isProf { get; set; }
        public byte[] profilePhoto { get; set; }
    }
}