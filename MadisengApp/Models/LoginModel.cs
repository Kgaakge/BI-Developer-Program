using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MadisengApp.Models
{
    public class LoginModel
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
     
    }
}