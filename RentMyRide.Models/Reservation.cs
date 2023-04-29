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
    public class Reservation
    {
        public int Id { get; set; }
        [DisplayName("Pick up date")]
        public DateTime  PickUpDate{ get; set; }
        [DisplayName("Drop off date")]
        public DateTime DropOffDate { get; set; }

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
