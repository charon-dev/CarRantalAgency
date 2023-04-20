using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime  PickUpDate{ get; set; }
        public DateTime DropOffDate { get; set; }

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
