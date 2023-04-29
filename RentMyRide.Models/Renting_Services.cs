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
    public class Renting_Services
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Additional service")]
        public int AdditionalServiceId { get; set; }
        [ForeignKey("AdditionalServiceId")]
        [ValidateNever]
        public AdditionalService AdditionalService { get; set; }

        [Display(Name = "Renting")]
        public int RentingId { get; set; }
        [ForeignKey("RentingId")]
        [ValidateNever]
        public Renting Renting { get; set; }
    }
}
