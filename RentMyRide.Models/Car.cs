using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string LicensePlateNumber { get; set; }
        public string currentMileage { get; set; }
        public double PriceByDay { get; set; }
        public int Places { get; set; }
        [ValidateNever]
        public string img { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }
    }
}
