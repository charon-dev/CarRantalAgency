using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models
{
    public class Reservation_Services
    {
        [Key]
        public int id { get; set; }

        public int AdditionalServiceId { get; set; }
        [ForeignKey("AdditionalServiceId")]
        [ValidateNever]
        public AdditionalService AdditionalService { get; set; }

        public int ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        [ValidateNever]
        public Reservation Reservation { get; set; }
    }
}
