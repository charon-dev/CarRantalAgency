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
    public class Renting
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }= DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        [DisplayName("Rental fees")]
        public double RentalFees { get; set; } = 0;
        [DisplayName("Extra charge")]
        public double ExtraCharge { get; set; } = 0;
        [DisplayName("Total charge")]
        public double TotalCharge { get; set; } = 0;
        [DisplayName("Payment method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Car")]
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public Car Car { get; set; }
        [Display(Name = "User")]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
