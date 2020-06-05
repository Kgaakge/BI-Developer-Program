using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MadisengApp.Models
{
    public class ManageUserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Province { get; set; }
        public string Gender { get; set; }
    }
}