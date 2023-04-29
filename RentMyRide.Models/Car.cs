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
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        [DisplayName("License plate number")]
        public string LicensePlateNumber { get; set; }
        [DisplayName("Current mileage")]
        public string currentMileage { get; set; }
        [DisplayName("Price by day")]
        public double PriceByDay { get; set; }
        public int Places { get; set; }
        [ValidateNever]
        public string img { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }
    }
}
