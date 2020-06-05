using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MadisengApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public bool Facilitator { get; set; }
        [Required]
        public string Password { get; set; }
    }
}