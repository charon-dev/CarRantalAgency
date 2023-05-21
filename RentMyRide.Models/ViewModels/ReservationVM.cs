using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models.ViewModels
{
    public class ReservationVM
    {
        [ValidateNever]
        public Reservation reservation { get; set; }
        [ValidateNever]
        public IEnumerable<AdditionalService> AdditionalServices { get; set; }
        [ValidateNever]
        public IEnumerable<Reservation_Services> Reservation_Services { get; set; }
        [ValidateNever]
        public Reservation_Services Reservation_Service { get; set; }
        [ValidateNever]
        public Car currentCar { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Users { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
