using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models.ViewModels
{
    public class RentingVM
    {
        public Renting renting { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CarList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UsersList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StatusList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PaymentList { get; set; }
        [ValidateNever]
        public IEnumerable<AdditionalService> AdditionalServices { get; set; }
        [ValidateNever]
        public IEnumerable<Renting_Services> Renting_Services { get; set; }
        [ValidateNever]
        public Renting_Services Renting_Service { get; set; }


    }
}
