using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models
{
    public class Renting
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }= DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public double RentalFees { get; set; }
        public double ExtraCharge { get; set; } = 0;
        public double TotalCharge { get; set; } = 0;
        public string PaymentMethod { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public Car Car { get; set; }

        //public int AdditionalServiceId { get; set; }
        //[ForeignKey("AdditionalServiceId")]
        //[ValidateNever]
        //public AdditionalService AdditionalService { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
