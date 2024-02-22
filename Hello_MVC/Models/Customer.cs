using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hello_MVC.Models
{
    public class Customer
    {
        public string Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage ="Your name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Telephone number only have 10 digits")]
        public string Telephone { get; set; }
    }
}