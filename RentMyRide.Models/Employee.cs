using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RentMyRide.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        public string Position { get; set; }
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }
    }
}
