using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineGamersPlatform.ViewModel
{
    public class Profile
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter your username.")]
        public string Username { get; set; }
        public string Description { get; set; }
        [Display(Name = "Game Type")]
        [Required(ErrorMessage ="Please choose a game type.")]
        public string GameType { get; set; }
        [Display(Name = "Are you professional?")]
        [Required(ErrorMessage ="Please give your answer.")]
        public bool IsProf { get; set; }
    }
}