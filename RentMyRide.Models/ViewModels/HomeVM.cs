using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMyRide.Models.ViewModels
{
    public class HomeVM
    {
        [ValidateNever]
        public int CarsCount { get; set; }
        [ValidateNever]
        public int UsersCount { get; set; }
        [ValidateNever]
        public IEnumerable<Location> LocationsList { get; set; }
        [ValidateNever]
        public int RentingCount { get; set; }
        [ValidateNever]
        public string OpenHours { get; set; }
        [ValidateNever]
        public IEnumerable<Car> Cars { get; set; }
        public Feedback Feedback { get; set; }
    }
}
